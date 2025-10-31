using Manager.Application.DTOs;
using Manager.Application.Interfaces;
using Manager.Domain.Entities;
using Manager.Domain.Interfaces;

namespace Manager.Application.Services;

public sealed class UsuarioService : IUsuarioService
{
    private readonly IRepository<Usuario> _repository;

    public UsuarioService(IRepository<Usuario> repository)
        => _repository = repository;

    public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
    {
        var usuarios = await _repository.GetAllAsync();
        return usuarios.Select(u => new UsuarioDTO(u.Id, u.Nome, u.Email, u.SenhaHash.GetHashCode().ToString(), u.DataCadastro));
    }

    public async Task<UsuarioDTO?> GetByIdAsync(Guid id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        return usuario is null ? null : new UsuarioDTO(usuario.Id, usuario.Nome, usuario.Email, usuario.SenhaHash.GetHashCode().ToString(), usuario.DataCadastro);
    }

    public async Task<UsuarioDTO> CreateAsync(UsuarioDTO dto)
    {
        var usuario = new Usuario { Nome = dto.Nome, Email = dto.Email, SenhaHash = dto.Senha.GetHashCode().ToString() };
        await _repository.AddAsync(usuario);
        return new UsuarioDTO(usuario.Id, usuario.Nome, usuario.Email, usuario.SenhaHash, usuario.DataCadastro);
    }

    public async Task UpdateAsync(UsuarioDTO dto)
    {
        var usuario = new Usuario { Id = dto.Id, Nome = dto.Nome, Email = dto.Email };
        await _repository.UpdateAsync(usuario);
    }

    public async Task DeleteAsync(Guid id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario != null)
            await _repository.DeleteAsync(usuario);
    }
}
