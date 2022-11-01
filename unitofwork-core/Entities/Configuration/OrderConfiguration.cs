using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace unitofwork_core.Entities.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.Property(w => w.Price).HasColumnType("decimal(10,0)");
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(u => u.ModifiedAt)
                .HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
            builder.HasMany(or => or.HistoryOrders)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(or => or.OrderRoutings)
                .WithOne(o => o.Order).HasForeignKey(or => or.OrderId);
            builder.HasMany(or => or.Transactions)
                .WithOne(tr => tr.Order).HasForeignKey(or => or.OrderId);
        }
    }
}
