using Manager.Domain.Entities;
using Manager.Domain.Interfaces;
using Manager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infrastructure.Repositories;

public sealed class MotoRepository : IRepository<Moto>
{
    private readonly AppDbContext _context;
    public MotoRepository(AppDbContext context) => _context = context;

    public Task AddAsync(Moto entity)
    {
        _context.Motos.Add(entity);
        return _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Moto entity)
    {
        _context.Motos.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Moto>> GetAllAsync()
    {
        return await _context.Motos.ToListAsync();
    }

    public async Task<Moto?> GetByIdAsync(Guid id) => await _context.Motos.FindAsync(id);

    public async Task UpdateAsync(Moto entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
