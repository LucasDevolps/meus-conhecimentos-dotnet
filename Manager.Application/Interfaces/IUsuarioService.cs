using Manager.Application.DTOs;

namespace Manager.Application.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDTO>> GetAllAsync();
    Task<UsuarioDTO?> GetByIdAsync(int id);
    Task<UsuarioDTO> CreateAsync(UsuarioDTO dto);
    Task UpdateAsync(UsuarioDTO dto);
    Task DeleteAsync(int id);
}