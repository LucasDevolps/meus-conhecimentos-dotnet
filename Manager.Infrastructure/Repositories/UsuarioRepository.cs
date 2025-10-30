using Manager.Domain.Entities;
using Manager.Domain.Interfaces;
using Manager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infrastructure.Repositories;

public class UsuarioRepository : IRepository<Usuario>
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Usuario>> GetAllAsync() =>
        await _context.Usuarios.ToListAsync();

    public async Task<Usuario?> GetByIdAsync(int id) =>
        await _context.Usuarios.FindAsync(id);

    public async Task AddAsync(Usuario entity)
    {
        _context.Usuarios.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Usuario entity)
    {
        _context.Usuarios.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
