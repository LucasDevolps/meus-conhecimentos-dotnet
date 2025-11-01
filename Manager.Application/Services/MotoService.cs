using Manager.Application.DTOs;
using Manager.Application.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Interfaces;

namespace Manager.Application.Services;

public sealed class MotoService : IMotoService
{
    private readonly IRepository<Moto> _repository;
    public MotoService(IRepository<Moto> repository) => _repository = repository;

    public async Task AtualizaAsync(MotoDTO dto)
    {
        var moto = await _repository.GetByIdAsync(dto.Uuid);
        if (moto is null)
            throw new KeyNotFoundException("Moto não encontrada.");

        if(moto.Placa == dto.Placa)
            throw new InvalidOperationException("A placa informada já está em uso por outra moto.");

        await _repository.UpdateAsync(new Moto { 
                                                    Id = dto.Id,
                                                    Uuid = dto.Uuid,
                                                    Modelo = dto.Modelo,
                                                    Ano = dto.Ano,
                                                    Placa = dto.Placa,
                                                    DataCriacao = dto.DataCadastro
        });
    }

    public async Task<MotoDTO?> BuscaPorIdAsync(Guid uuid)
    {
        var moto = await _repository.GetByIdAsync(uuid);
        if (moto is null)
            return null;
        return new MotoDTO(moto.Id,moto.Uuid,moto.Modelo,moto.Ano,moto.Placa,moto.DataCriacao);
    }

    public async Task<IEnumerable<MotoDTO>> BuscasTudoAsync()
    {
        var motos = await _repository.GetAllAsync();
        return motos.Select(m => new MotoDTO(m.Id,m.Uuid,m.Modelo,m.Ano,m.Placa,m.DataCriacao));
    }

    public async Task<MotoDTO> CriaAsync(MotoDTO dto)
    {

        var motoValidator = await _repository.GetByIdAsync(dto.Uuid);

        if (motoValidator?.Placa == dto.Placa)
            throw new InvalidOperationException("A placa informada já está em uso por outra moto.");

        var moto = new Moto
        {
            Uuid = dto.Uuid,
            Modelo = dto.Modelo,
            Ano = dto.Ano,
            Placa = dto.Placa,
            DataCriacao = dto.DataCadastro
        };

        await _repository.AddAsync( moto );
        return dto;
    }

    public async Task DeletaAsync(Guid uuid)
    {
        var moto = await _repository.GetByIdAsync(uuid);
        if (moto is null)
            throw new KeyNotFoundException("Moto não encontrada.");

        await _repository.DeleteAsync(moto);
    }
}
