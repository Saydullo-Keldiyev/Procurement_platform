using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AgriProcurement.Procurement.Domain.Aggregates;

namespace AgriProcurement.Procurement.Infrastructure.Persistence.Configurations;

public sealed class ProcurementOrderConfiguration
    : IEntityTypeConfiguration<ProcurementOrder>
{
    public void Configure(EntityTypeBuilder<ProcurementOrder> builder)
    {
        builder.ToTable("procurement_orders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.OwnsMany(x => x.OrderLines, lines =>
        {
            lines.ToTable("procurement_order_lines");
            lines.WithOwner().HasForeignKey("order_id");

            lines.Property<Guid>("id");
            lines.HasKey("id");

            // 🔥 MONEY VALUE OBJECT MAPPING
            lines.OwnsOne(x => x.Price, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("price_amount")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("price_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });
        });
    }
}
