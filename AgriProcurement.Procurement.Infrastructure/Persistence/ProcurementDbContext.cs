using AgriProcurement.Procurement.Domain.Aggregates;
using AgriProcurement.Procurement.Infrastructure.Idempotency;
using AgriProcurement.Procurement.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AgriProcurement.Procurement.Infrastructure.Persistence;

public sealed class ProcurementDbContext : DbContext
{
    public DbSet<ProcurementOrder> Orders => Set<ProcurementOrder>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    internal DbSet<IdempotentRequest> IdempotentRequests => Set<IdempotentRequest>();

    public ProcurementDbContext(DbContextOptions<ProcurementDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ProcurementDbContext).Assembly);


    }
}
