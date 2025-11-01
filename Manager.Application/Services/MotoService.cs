using Manager.Application.DTOs;
using Manager.Application.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Interfaces;
using System.Runtime.InteropServices;

namespace Manager.Application.Services;

public sealed class MotoService : IMotoService
{
    private readonly IRepository<Moto> _repository;
    private readonly IRabbitMqService _rabbitMqService;
    public MotoService(IRepository<Moto> repository,
                       IRabbitMqService rabbitMqService
        ) 
    {
        _repository = repository;
        _rabbitMqService = rabbitMqService;
    }

    public async Task AtualizaAsync(MotoDTO dto)
    {
        try
        {
            var moto = await _repository.GetByIdAsync(dto.Uuid);
            if (moto is null)
                throw new KeyNotFoundException("Moto não encontrada.");

            if (moto.Placa == dto.Placa)
                throw new InvalidOperationException("A placa informada já está em uso por outra moto.");

            await _repository.UpdateAsync(new Moto
            {
                Id = dto.Id,
                Uuid = dto.Uuid,
                Modelo = dto.Modelo,
                Ano = dto.Ano,
                Placa = dto.Placa,
                DataCriacao = dto.DataCadastro
            });

            await _rabbitMqService.Publish(new Domain.Events.MotoCadastradaEvent(moto.Uuid, moto.Modelo, moto.Ano, moto.Placa), "moto.atualizada");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    public async Task<MotoDTO?> BuscaPorIdAsync(Guid uuid)
    {
        var moto = await _repository.GetByIdAsync(uuid);
        if (moto is null)
            return null;
        return new MotoDTO(0,moto.Uuid,moto.Modelo,moto.Ano,moto.Placa,moto.DataCriacao);
    }

    public async Task<IEnumerable<MotoDTO>> BuscarTudoAsync()
    {
        var motos = await _repository.GetAllAsync();

        return motos
            .Select(m => new MotoDTO(
                id: 0,              
                uuid: m.Uuid,
                modelo: m.Modelo,
                ano: m.Ano,
                placa: m.Placa,
                dataCadastro: m.DataCriacao
            ))
            .ToList();
    }


    public async Task<MotoDTO> CriaAsync(MotoDTO dto)
    {
        try
        {
            var motoValidator = await _repository.GetByIdAsync(dto.Uuid);

            if (motoValidator?.Placa == dto.Placa)
                throw new InvalidOperationException("A placa informada já está em uso por outra moto.");

            var moto = new Moto
            {
                Uuid = Guid.NewGuid(),
                Modelo = dto.Modelo,
                Ano = dto.Ano,
                Placa = dto.Placa,
                DataCriacao = dto.DataCadastro
            };

            await _repository.AddAsync(moto);

            await _rabbitMqService.Publish(new Domain.Events.MotoCadastradaEvent(moto.Uuid, moto.Modelo, moto.Ano, moto.Placa), "moto.cadastrada");

            return dto;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    public async Task DeletaAsync(Guid uuid)
    {
        try
        {
            var moto = await _repository.GetByIdAsync(uuid);
            if (moto is null)
                throw new KeyNotFoundException("Moto não encontrada.");

            await _rabbitMqService.Publish(new Domain.Events.MotoCadastradaEvent(moto.Uuid, moto.Modelo, moto.Ano, moto.Placa), "moto.deletada");

            await _repository.DeleteAsync(moto);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
}
