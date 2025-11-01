using Manager.Application.DTOs;
using Manager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace Manager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MotoController : ControllerBase
{
    private readonly ILogger<MotoController> _logger;
    private readonly IMotoService _motoService;

    public MotoController(
        ILogger<MotoController> logger,
        IMotoService motoService
    )
    {
        _logger = logger;
        _motoService = motoService;
    }

    [HttpGet("/api/Moto")]
    public async Task<ActionResult<IEnumerable<MotoDTO>>> GetMotos()
    {
        IEnumerable<MotoDTO> motos = await _motoService.BuscarTudoAsync();
        return Ok(motos);
    }

    [HttpGet("/api/Moto/{uuid:guid}/id")]
    public async Task<ActionResult<MotoDTO?>> GetMotoById(Guid uuid)
    {
        MotoDTO? moto = await _motoService.BuscaPorIdAsync(uuid);
        return moto is null ? NotFound() : Ok(moto);
    }
    [HttpPost]
    public async Task<ActionResult<MotoDTO>> CreateMoto(MotoDTO dto)
    {
        try
        {
            MotoDTO createdMoto = await _motoService.CriaAsync(dto);
            return CreatedAtAction(nameof(GetMotoById), new { uuid = createdMoto.Uuid }, createdMoto);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
    [HttpPut("/api/Moto/{uuid:guid}")]
    public async Task<IActionResult> UpdateMoto(Guid uuid, MotoDTO dto)
    {
        try
        {
            if (uuid != dto.Uuid) return BadRequest("UUID mismatch.");
            await _motoService.AtualizaAsync(dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
    [HttpDelete("/api/Moto/{uuid:guid}")]
    public async Task<IActionResult> DeleteMoto(Guid uuid)
    {
        try
        {
            await _motoService.DeletaAsync(uuid);
            return NoContent();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
}
