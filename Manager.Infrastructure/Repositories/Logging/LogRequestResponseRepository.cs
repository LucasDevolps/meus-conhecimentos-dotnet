using Manager.Domain.Logging;
using Manager.Infrastructure.Data;

namespace Manager.Infrastructure.Repositories.Logging;

public interface ILogRequestResponseRepository
{
    Task SaveAsync(LogRequestResponse log);
}

public sealed class LogRequestResponseRepository : ILogRequestResponseRepository
{
    private readonly AppDbContext _context;

    public LogRequestResponseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(LogRequestResponse log)
    {
        _context.LogRequestResponses.Add(log);
        await _context.SaveChangesAsync();
    }
}