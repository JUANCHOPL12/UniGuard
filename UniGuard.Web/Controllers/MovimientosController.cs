using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniGuard.Shared;
using UniGuard.Web.Data;

namespace UniGuard.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovimientosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. HISTORIAL GLOBAL: Para la Bitácora del Administrador
        [HttpGet("detallado")]
        public async Task<ActionResult<IEnumerable<Movimiento>>> GetMovimientosDetallados()
        {
            try
            {
                return await _context.Movimientos
                    .Include(m => m.Equipo)
                        .ThenInclude(e => e.Estudiante)
                    .Include(m => m.UsuarioSistema)
                    .OrderByDescending(m => m.FechaHora)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener historial: {ex.Message}");
            }
        }

        // 2. HISTORIAL INDIVIDUAL: Para el Perfil del Estudiante
        [HttpGet("estudiante/{id}")]
        public async Task<ActionResult<IEnumerable<Movimiento>>> GetMovimientosPorEstudiante(int id)
        {
            try
            {
                return await _context.Movimientos
                    .Include(m => m.Equipo)
                    .Include(m => m.UsuarioSistema)
                    .Where(m => m.EstudianteId == id)
                    .OrderByDescending(m => m.FechaHora)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener movimientos: {ex.Message}");
            }
        }

        // 3. REGISTRAR MOVIMIENTO: Con Validación de Estado (Entrada/Salida)
        [HttpPost]
        public async Task<ActionResult<Movimiento>> PostMovimiento(Movimiento movimiento)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Establecer la hora del servidor
                movimiento.FechaHora = DateTime.Now;

                // Buscar el equipo y su estado actual en la BD
                var equipo = await _context.Equipos.FindAsync(movimiento.EquipoId);

                if (equipo == null)
                {
                    return NotFound("El equipo seleccionado no existe en el sistema.");
                }

                // VALIDACIÓN DE FLUJO LÓGICO
                // EstadoActualId: 1 = Dentro, 2 = Fuera (Asegúrate que coincidan con tus IDs en SQL)
                bool esIntentoEntrada = movimiento.TipoMovimiento.Equals("Entrada", StringComparison.OrdinalIgnoreCase);

                if (esIntentoEntrada && equipo.EstadoActualId == 1)
                {
                    return BadRequest("Error: El equipo ya se encuentra DENTRO. Registre una Salida primero.");
                }

                if (!esIntentoEntrada && equipo.EstadoActualId == 2)
                {
                    return BadRequest("Error: El equipo ya se encuentra FUERA. Registre una Entrada primero.");
                }

                // Si pasa la validación, asignamos el Estudiante dueño del equipo
                movimiento.EstudianteId = equipo.EstudianteId;

                // Actualizar la ubicación actual del equipo según el movimiento
                equipo.EstadoActualId = esIntentoEntrada ? 1 : 2;

                _context.Entry(equipo).State = EntityState.Modified;
                _context.Movimientos.Add(movimiento);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(movimiento);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest($"Error crítico: {ex.Message}");
            }
        }
    }
}