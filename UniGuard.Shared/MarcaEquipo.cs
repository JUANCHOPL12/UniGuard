using System.ComponentModel.DataAnnotations;

namespace UniGuard.Shared;

public class MarcaEquipo
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; } = string.Empty;
}