namespace Manager.Application.DTOs;

public sealed record MotoDTO(int Id,
    Guid Uuid,
    string Modelo,
    int Ano,
    string Placa,
    DateTime DataCadastro
);

