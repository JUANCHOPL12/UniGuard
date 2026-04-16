using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniGuard.Shared;
using UniGuard.Web.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UniGuard.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto login)
        {
            // 1. Buscamos el usuario
            var usuario = await _context.UsuariosSistema
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.NombreUsuario == login.Correo
                                       && u.PasswordHash == login.Clave);

            if (usuario == null)
            {
                return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });
            }

            // 2. GENERAR EL TOKEN JWT
            var token = GenerarJwt(usuario);

            // 3. Devolvemos el Token y los datos básicos
            return Ok(new
            {
                token = token, // Este es el carnet de acceso real
                id = usuario.Id,
                nombre = usuario.NombreUsuario,
                rolId = usuario.RolId,
                rol = usuario.Rol?.Nombre ?? "Sin Rol"
            });
        }

        private string GenerarJwt(UsuarioSistema usuario)
        {
            // USAMOS LA MISMA CLAVE QUE PUSIMOS EN EL PROGRAM.CS
            var jwtKey = "Tu_Clave_Secreta_Super_Larga_De_Mas_De_32_Caracteres_UniGuard_2024";
            var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Name, usuario.NombreUsuario));
            claims.AddClaim(new Claim(ClaimTypes.Role, usuario.Rol.Nombre)); // Aquí va el ROL

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(8), // El token dura una jornada laboral
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(tokenConfig);
        }
    }
}