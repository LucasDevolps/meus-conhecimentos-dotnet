using Manager.Application.Interfaces;
using Manager.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMotoService, MotoService>();
        services.AddScoped<IUsuarioService, UsuarioService>();

        return services;
    }
}