using System.ComponentModel.DataAnnotations;

namespace Manager.Domain.Entities;

public sealed class Usuario
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Identificador único universal do usuário.
    /// </summary>
    public Guid Uuid { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Nome completo do usuário.
    /// </summary>
    [Required, MaxLength(150)]
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// E-mail usado para login e comunicação.
    /// </summary>
    [Required, MaxLength(150)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Hash da senha gerada com BCrypt (nunca armazenar a senha pura).
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string SenhaHash { get; set; } = string.Empty;

    /// <summary>
    /// Perfil ou função do usuário (ex: Admin, User, etc.).
    /// </summary>
    [MaxLength(50)]
    public string Role { get; set; } = "User";

    /// <summary>
    /// Data e hora de criação do registro.
    /// </summary>
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Define se o usuário está ativo no sistema.
    /// </summary>
    public bool Ativo { get; set; } = true;
}
