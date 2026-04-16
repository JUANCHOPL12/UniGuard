public class Estado
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty; // Ejemplo: Activo, Inactivo, Robado
    public string TipoEntidad { get; set; } = string.Empty; // Para saber si es estado de "Estudiante" o "Equipo"
}