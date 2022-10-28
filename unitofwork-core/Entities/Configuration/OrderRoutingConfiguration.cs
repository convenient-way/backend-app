using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace unitofwork_core.Entities.Configuration
{
    public class OrderRoutingConfiguration : IEntityTypeConfiguration<OrderRouting>
    {
        public void Configure(EntityTypeBuilder<OrderRouting> builder)
        {
            builder.ToTable("OrderRouting");
            builder.Property(u => u.ModifiedAt)
               .HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
            builder.HasMany(or => or.Products)
                .WithOne(pr => pr.OrderRouting).HasForeignKey(pr => pr.OrderRoutingId);
        }
    }
}
