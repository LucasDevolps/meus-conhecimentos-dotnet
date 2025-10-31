using Manager.Domain.Entities;
using Manager.Domain.Logging;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Moto> Motos { get; set; }
    public DbSet<LogRequestResponse> LogRequestResponses => Set<LogRequestResponse>();
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LogRequestResponse>(entity =>
        {
            entity.ToTable("LogRequestResponse");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.UserName).HasMaxLength(100);
            entity.Property(x => x.Method).HasMaxLength(10);
            entity.Property(x => x.Path).HasMaxLength(200);
        });
    }
}
