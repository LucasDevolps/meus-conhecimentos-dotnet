
using Manager.Domain.Logging;
using Manager.Infrastructure.Repositories.Logging;

namespace Manager.Infrastructure.Services;

public interface ILogRequestResponseService
{
    Task SaveAsync(string? userName, string method, string path, string? request, string? response, int statusCode, long duration);
}

public sealed class LogRequestResponseService : ILogRequestResponseService
{
    private readonly ILogRequestResponseRepository _repository;

    public LogRequestResponseService(ILogRequestResponseRepository repository)
    {
        _repository = repository;
    }

    public async Task SaveAsync(string? userName, string method, string path, string? request, string? response, int statusCode, long duration)
    {
        var log = new LogRequestResponse
        {
            UserName = userName ?? "Anonymous",
            Method = method,
            Path = path,
            RequestBody = request,
            ResponseBody = response,
            StatusCode = statusCode,
            DurationMs = duration
        };

        await _repository.SaveAsync(log);
    }
}
