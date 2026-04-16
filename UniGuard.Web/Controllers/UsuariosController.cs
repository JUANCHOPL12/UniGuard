using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniGuard.Web.Data; // Ajusta esto al nombre de tu proyecto
using UniGuard.Shared;

namespace UniGuard.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] LoginRequest request)
        {
            // Buscamos el usuario por nombre y clave, e incluimos su Rol
            var usuario = await _context.UsuariosSistema
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.NombreUsuario == request.Usuario
                                       && u.PasswordHash == request.Password);

            if (usuario == null)
            {
                return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });
            }

            // Devolvemos la info necesaria para el NavMenu
            return Ok(new
            {
                id = usuario.Id,
                nombre = usuario.NombreUsuario,
                rol = usuario.Rol?.Nombre ?? "Guardia"
            });
        }
    }

    // Clase pequeña para recibir los datos del Login.razor
    public class LoginRequest
    {
        public string Usuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}