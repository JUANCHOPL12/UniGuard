namespace UniGuard.Shared // Ajusta esto al nombre real de tu proyecto Shared
{
    public class LoginDto
    {
        public string Correo { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
    }

    public class SesionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}