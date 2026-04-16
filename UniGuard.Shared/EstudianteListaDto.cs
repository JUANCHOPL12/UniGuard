namespace UniGuard.Shared
{
    public class EstudianteListaDto
    {
        public int Id { get; set; }
        public string Documento { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string NombreEstado { get; set; } = string.Empty;
    }
}