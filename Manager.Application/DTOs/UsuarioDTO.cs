namespace Manager.Application.DTOs;

public sealed record UsuarioDTO(int Id, string Nome, string Email, string Senha, DateTime DataCadastro);
