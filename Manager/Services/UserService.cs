using Manager.Domain.Entities;
using Manager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Manager.WebApi.Services;

public sealed class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context) => _context = context;

    public async Task<Usuario?> GetByUsernameAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
