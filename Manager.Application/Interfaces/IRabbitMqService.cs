namespace Manager.Application.Interfaces;

public interface IRabbitMqService
{
    Task InitializeAsync();
    Task Publish<T>(T message, string routingKey);
    ValueTask DisposeAsync();
}
