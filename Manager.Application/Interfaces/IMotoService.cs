using Manager.Application.DTOs;

namespace Manager.Application.Interfaces;

public interface IMotoService
{
    Task<IEnumerable<MotoDTO>> BuscarTudoAsync();
    Task<MotoDTO?> BuscaPorIdAsync(Guid uuid);
    Task<MotoDTO> CriaAsync(MotoDTO dto);
    Task AtualizaAsync(MotoDTO dto);
    Task DeletaAsync(Guid uuid);
}
