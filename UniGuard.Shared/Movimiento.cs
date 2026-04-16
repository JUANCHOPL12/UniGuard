using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniGuard.Shared
{
    public class Movimiento
    {
        public int Id { get; set; }

        // Relación con el Equipo
        public int EquipoId { get; set; }
        public Equipo? Equipo { get; set; }

        // Relación directa con el Estudiante (Para reportes rápidos)
        public int EstudianteId { get; set; }
        public Estudiante? Estudiante { get; set; }

        // Datos del movimiento
        public string TipoMovimiento { get; set; } = "Entrada"; // "Entrada" o "Salida"
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public string? Observaciones { get; set; }

        // Relación con el Guardia/Admin que registra
        public int? UsuarioSistemaId { get; set; }
        public UsuarioSistema? UsuarioSistema { get; set; }
    }
}