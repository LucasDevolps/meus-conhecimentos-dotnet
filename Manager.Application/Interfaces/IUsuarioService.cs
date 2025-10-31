using Manager.Application.DTOs;

namespace Manager.Application.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDTO>> GetAllAsync();
    Task<UsuarioDTO?> GetByIdAsync(Guid id);
    Task<UsuarioDTO> CreateAsync(UsuarioDTO dto);
    Task UpdateAsync(UsuarioDTO dto);
    Task DeleteAsync(Guid id);
}