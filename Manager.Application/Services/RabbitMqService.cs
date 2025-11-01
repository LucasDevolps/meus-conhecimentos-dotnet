using Manager.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Manager.Application.Services;

public sealed class RabbitMqService : IRabbitMqService, IAsyncDisposable
{
    private readonly ConnectionFactory _factory;
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly string _exchange;

    public RabbitMqService(IConfiguration configuration)
    {
        _factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMq:HostName"] ?? "localhost",
            UserName = configuration["RabbitMq:UserName"] ?? "guest",
            Password = configuration["RabbitMq:Password"] ?? "guest",
            Port = int.Parse(configuration["RabbitMq:Port"] ?? "5672")
        };

        _exchange = configuration["RabbitMq:Exchange"] ?? "manager.exchange";
    }

    public async Task InitializeAsync()
    {
        try
        {
            _connection = await _factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(
                exchange: _exchange,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false,
                arguments: null
            );

            await _channel.QueueDeclareAsync(
                queue: _exchange,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to initialize RabbitMQ connection and channel.", ex);
        }
    }

    public async Task Publish<T>(T message, string routingKey)
    {
        try
        {
            if (_channel is null)
                throw new InvalidOperationException("RabbitMQ channel not initialized. Call InitializeAsync() first.");

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            await _channel.BasicPublishAsync(
                _exchange,
                routingKey,
                body: body
            );

            Console.WriteLine($"[x] Mensagem publicada em {_exchange} -> {routingKey}");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to publish message to RabbitMQ.", ex);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel is not null)
            await _channel.CloseAsync();

        if (_connection is not null)
            await _connection.CloseAsync();

        _channel?.Dispose();
        _connection?.Dispose();

        GC.SuppressFinalize(this);
    }
}