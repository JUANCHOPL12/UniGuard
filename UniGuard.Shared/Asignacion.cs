using System;

namespace UniGuard.Shared
{
    public class Asignacion
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public Estudiante? Estudiante { get; set; }
        public int EquipoId { get; set; }
        public Equipo? Equipo { get; set; }
        public DateTime FechaAsignacion { get; set; } = DateTime.Now;
        public bool EsVigente { get; set; } = true;
    }
}