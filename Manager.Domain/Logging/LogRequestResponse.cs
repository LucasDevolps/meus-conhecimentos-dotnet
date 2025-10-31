namespace Manager.Domain.Logging;

public sealed class LogRequestResponse
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Method { get; set; }
    public string? Path { get; set; }
    public string? RequestBody { get; set; }
    public string? ResponseBody { get; set; }
    public int StatusCode { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public long DurationMs { get; set; }
}
