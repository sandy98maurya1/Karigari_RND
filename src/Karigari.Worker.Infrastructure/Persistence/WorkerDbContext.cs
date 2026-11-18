using Karigari.Worker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Karigari.Worker.Infrastructure.Persistence;

public sealed class WorkerDbContext(DbContextOptions<WorkerDbContext> options) : DbContext(options)
{
    public DbSet<WorkerProfile> WorkerProfiles => Set<WorkerProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
