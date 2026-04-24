using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UniGuard.Shared;

public class Equipo
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El serial es obligatorio")]
    public string Serial { get; set; } = string.Empty;

    [JsonPropertyName("Modelo")]
    public string Modelo { get; set; } = string.Empty;

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    // IDs de relación
    public int EstudianteId { get; set; }
    public int? MarcaId { get; set; }
    public int? EstadoActualId { get; set; }

    // Propiedades de Navegación (Los "Puentes")
    [ForeignKey("EstudianteId")]
    public virtual Estudiante? Estudiante { get; set; }

    [ForeignKey("MarcaId")]
    public virtual MarcaEquipo? Marca { get; set; }

    [ForeignKey("EstadoActualId")]
    public virtual Estado? EstadoActual { get; set; }
}