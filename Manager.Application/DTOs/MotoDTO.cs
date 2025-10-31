namespace Manager.Application.DTOs;

public sealed record MotoDTO(int Id,
    Guid Uuid,
    string Marca,
    string Modelo,
    int Ano,
    string Cor,
    string Placa,
    DateTime DataCadastro
);

