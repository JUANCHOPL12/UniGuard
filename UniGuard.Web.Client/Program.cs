using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Text.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// 1. Configuración del HttpClient (Asegúrate que el puerto 7151 sea el de tu API)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7151/")
});

// 2. 🔥 LA FORMA CORRECTA DE CONFIGURAR JSON EN EL CLIENTE 🔥
// Esto le dice al sistema: "Cuando recibas datos, ignora si son Mayúsculas o Minúsculas"
builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.PropertyNameCaseInsensitive = true;
    options.PropertyNamingPolicy = null; // Para que respete nombres como 'FotoBase64' o 'Documento'
});

await builder.Build().RunAsync();