using Manager.Application.DTOs;
using Manager.Application.Interfaces;
using Manager.Domain.Interfaces;

namespace Manager.Application.Services;

public sealed class MotoService : IMotoService
{
    private readonly IRepository<MotoDTO> _repository;
    public MotoService(IRepository<MotoDTO> repository) => _repository = repository;

    public async Task AtualizaAsync(MotoDTO dto)
    {
        var moto = await _repository.GetByIdAsync(dto.Uuid);
        if (moto is null)
            throw new KeyNotFoundException("Moto não encontrada.");

        if(moto.Placa == dto.Placa)
            throw new InvalidOperationException("A placa informada já está em uso por outra moto.");

        await _repository.UpdateAsync(dto);
    }

    public async Task<MotoDTO?> BuscaPorIdAsync(Guid uuid)
    {
        return await _repository.GetByIdAsync(uuid);
    }

    public async Task<IEnumerable<MotoDTO>> BuscasTudoAsync()
    {
        var motos = await _repository.GetAllAsync();
        return motos.Select(m => new MotoDTO
        (   
            m.Id,
            m.Uuid,
            m.Marca,
            m.Modelo,
            m.Ano,
            m.Cor,
            m.Placa,
            m.DataCadastro
        ));
    }

    public async Task<MotoDTO> CriaAsync(MotoDTO dto)
    {

        var motoValidator = await _repository.GetByIdAsync(dto.Uuid);

        if (motoValidator?.Placa == dto.Placa)
            throw new InvalidOperationException("A placa informada já está em uso por outra moto.");

        var moto = new MotoDTO
        (
            Id: dto.Id,
            Uuid: dto.Uuid,
            Marca: dto.Marca,
            Modelo: dto.Modelo,
            Ano: dto.Ano,
            Cor: dto.Cor,
            Placa: dto.Placa,
            DataCadastro: dto.DataCadastro
        );

        await _repository.AddAsync( dto );
        return moto;
    }

    public async Task DeletaAsync(Guid uuid)
    {
        var moto = await _repository.GetByIdAsync(uuid);
        if (moto is null)
            throw new KeyNotFoundException("Moto não encontrada.");

        await _repository.DeleteAsync(moto);
    }
}
