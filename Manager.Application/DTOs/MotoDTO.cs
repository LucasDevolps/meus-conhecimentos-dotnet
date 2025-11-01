using System.Text.Json.Serialization;

namespace Manager.Application.DTOs;

public sealed class MotoDTO
{
    [JsonIgnore]
    public int Id { get; set; }
    public Guid Uuid { get; set; }
    public string Modelo { get; set; }
    public int Ano { get; set; }
    public string Placa { get; set; }
    public DateTime DataCadastro { get; set; }

    public MotoDTO(int id, Guid uuid, string modelo, int ano, string placa, DateTime dataCadastro)
    {
        Id = id;
        Uuid = uuid;
        Modelo = modelo;
        Ano = ano;
        Placa = placa;
        DataCadastro = dataCadastro;
    }
}

