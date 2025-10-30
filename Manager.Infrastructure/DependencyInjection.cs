using Manager.Application.Interfaces;
using Manager.Application.Services;
using Manager.Domain.Entities;
using Manager.Domain.Interfaces;
using Manager.Infrastructure.Data;
using Manager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IRepository<Usuario>, UsuarioRepository>();
        services.AddScoped<IUsuarioService, UsuarioService>();

        return services;
    }
}
