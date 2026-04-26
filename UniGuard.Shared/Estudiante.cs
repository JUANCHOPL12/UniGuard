namespace UniGuard.Shared
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Documento { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public string? FotoBase64 { get; set; }

        public string? Telefono { get; set; }
      
        public string? CorreoInstitucional { get; set; }

        public bool EstaActivo { get; set; } = true;

        // Propiedad de navegación para evitar errores en otras páginas
        public List<Equipo> Equipos { get; set; } = new List<Equipo>();
    }
}