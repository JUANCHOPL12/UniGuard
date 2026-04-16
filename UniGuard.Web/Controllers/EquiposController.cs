using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniGuard.Web.Data;
using UniGuard.Shared;

namespace UniGuard.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EquiposController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Equipos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetEquipos()
        {
            return await _context.Equipos
                .Include(e => e.Marca)
                .Include(e => e.EstadoActual)
                .AsNoTracking()
                .ToListAsync();
        }

        // PUT: api/Equipos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipo(int id, Equipo equipo)
        {
            if (id != equipo.Id) return BadRequest();

            _context.Entry(equipo).State = EntityState.Modified;

            if (equipo.Marca != null) _context.Entry(equipo.Marca).State = EntityState.Unchanged;
            if (equipo.EstadoActual != null) _context.Entry(equipo.EstadoActual).State = EntityState.Unchanged;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipoExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Equipos
        // MODIFICADO: Ahora valida que el serial sea único
        [HttpPost]
        public async Task<ActionResult<Equipo>> PostEquipo(Equipo equipo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(equipo.Serial))
                {
                    return BadRequest("El número de serial es obligatorio.");
                }

                string serialLimpio = equipo.Serial.Trim();

                // --- EL CANDADO CONTRA DUPLICADOS ---
                var existeSerial = await _context.Equipos
                    .AnyAsync(e => e.Serial.ToLower() == serialLimpio.ToLower());

                if (existeSerial)
                {
                    // Enviamos un error 400 con un mensaje personalizado
                    return BadRequest($"❌ El serial '{serialLimpio}' ya está registrado. Por favor verifique.");
                }
                // -------------------------------------

                equipo.Serial = serialLimpio;

                _context.Equipos.Add(equipo);
                await _context.SaveChangesAsync();

                var equipoCreado = await _context.Equipos
                    .Include(e => e.Marca)
                    .Include(e => e.EstadoActual)
                    .FirstOrDefaultAsync(e => e.Id == equipo.Id);

                return Ok(equipoCreado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al guardar: {ex.Message}");
            }
        }

        private bool EquipoExists(int id)
        {
            return _context.Equipos.Any(e => e.Id == id);
        }
    }
}