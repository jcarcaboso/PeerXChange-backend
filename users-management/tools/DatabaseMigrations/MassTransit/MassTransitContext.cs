using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMigrations.MassTransit;

public class MassTransitContext : DbContext
{
    public MassTransitContext()
    {
    }

    public MassTransitContext(DbContextOptions<MassTransitContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}