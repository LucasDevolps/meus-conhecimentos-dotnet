using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Manager.Domain.Entities;

public sealed class Moto
{
    [Key]
    public Guid Uuid { get; set; } = Guid.NewGuid();
    public int Id { get; set; }
    public required int Ano { get; set; }
    [MaxLength(100)]
    public required string Modelo { get; set; } = string.Empty;
    [MaxLength(100)]
    public required string Placa { get; set; } = string.Empty;  
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
