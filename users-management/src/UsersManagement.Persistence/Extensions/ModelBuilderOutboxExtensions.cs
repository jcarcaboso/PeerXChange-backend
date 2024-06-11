using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace UsersManagement.Persistence.Extensions;

public static class ModelBuilderOutboxExtensions
{
    public static void AddOutboxPattern(this ModelBuilder modelBuilder)
    {
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}