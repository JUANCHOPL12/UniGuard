using Microsoft.EntityFrameworkCore;
using UniGuard.Web.Components;
using UniGuard.Shared;
using System.Text.Json.Serialization;
using UniGuard.Web.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer; // NUEVO
using Microsoft.IdentityModel.Tokens; // NUEVO
using System.Text; // NUEVO

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de Base de Datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. CONFIGURACIÓN DE SEGURIDAD JWT (NUEVO)
// Esta clave debe ser la misma que uses en el AuthController para firmar el token
var jwtKey = "Tu_Clave_Secreta_Super_Larga_De_Mas_De_32_Caracteres_UniGuard_2024";
var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(config => {
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config => {
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero // Para que el token expire exacto cuando le digamos
    };
});

// 3. Configuración de API (Controladores)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// 4. Configuración de Blazor y HttpClient
builder.Services.AddHttpClient("UniGuard.API", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7151");
});

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7151")
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// 5. Middleware y Seguridad
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// 🔥 ORDEN CRÍTICO DE SEGURIDAD 🔥
app.UseRouting(); // Asegura las rutas
app.UseAuthentication(); // ¿Quién es el usuario? (Lee el Token JWT)
app.UseAuthorization();  // ¿Qué permisos tiene? (Valida el Rol)

app.UseAntiforgery();

// 6. MAPEO DE RUTAS
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(UniGuard.Web.Client._Imports).Assembly);

app.Run();