using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Manager.Domain.Entities;

public sealed class Moto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public Guid Uuid { get; set; } = Guid.NewGuid();

    [Required]
    public int Ano { get; set; }

    [Required, MaxLength(100)]
    public string Modelo { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Placa { get; set; } = string.Empty;

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
