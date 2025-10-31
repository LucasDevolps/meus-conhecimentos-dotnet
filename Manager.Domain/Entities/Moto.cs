using System.Text.Json.Serialization;

namespace Manager.Domain.Entities;

public sealed class Moto
{
    public Guid Uuid { get; set; }
    [JsonIgnore]
    public int Id { get; set; }
    public required int Ano { get; set; }
    public required int Modelo { get; set; }
    public required string Placa { get; set; }
}
