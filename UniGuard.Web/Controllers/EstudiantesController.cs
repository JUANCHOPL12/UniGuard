using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniGuard.Web.Data;
using UniGuard.Shared;

namespace UniGuard.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EstudiantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. OBTENER TODOS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantes()
        {
            try
            {
                var lista = await _context.Estudiantes
                    .Include(e => e.Equipos)
                    .AsNoTracking()
                    .ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en el servidor: {ex.Message}");
            }
        }

        // 2. BUSCAR POR ID (Corregido para evitar el 404)
        // Agregamos :int para que solo entre aquí si es un número de ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Estudiante>> GetEstudiantePorId(int id)
        {
            var estudiante = await _context.Estudiantes
                .Include(e => e.Equipos)
                    .ThenInclude(eq => eq.Marca)
                .Include(e => e.Equipos)
                    .ThenInclude(eq => eq.EstadoActual)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estudiante == null) return NotFound("Estudiante no encontrado por ID.");

            return Ok(estudiante);
        }

        // 3. BUSCAR POR DOCUMENTO (Ruta específica para evitar conflictos)
        [HttpGet("buscar-por-documento/{documento}")]
        public async Task<ActionResult<Estudiante>> GetEstudiantePorDocumento(string documento)
        {
            var estudiante = await _context.Estudiantes
                .Include(e => e.Equipos)
                    .ThenInclude(eq => eq.Marca)
                .Include(e => e.Equipos)
                    .ThenInclude(eq => eq.EstadoActual)
                .FirstOrDefaultAsync(e => e.Documento == documento.Trim());

            if (estudiante == null) return NotFound("Estudiante no encontrado por documento.");

            return Ok(estudiante);
        }

        // 4. ACTUALIZAR (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiante(int id, Estudiante estudiante)
        {
            if (id != estudiante.Id) return BadRequest();

            _context.Entry(estudiante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Estudiantes.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // 5. GUARDAR NUEVO (POST)
        [HttpPost]
        public async Task<ActionResult<Estudiante>> PostEstudiante(Estudiante estudiante)
        {
            try
            {
                _context.Estudiantes.Add(estudiante);
                await _context.SaveChangesAsync();

                // Redirigimos a la ruta de ID corregida
                return CreatedAtAction(nameof(GetEstudiantePorId), new { id = estudiante.Id }, estudiante);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al guardar: {ex.Message}");
            }
        }
    }
}