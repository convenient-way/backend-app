using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace unitofwork_core.Entities.Configuration
{
    public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> builder)
        {
            builder.ToTable("Shipper");
            builder.HasIndex(sh => sh.UserName).IsUnique();
            builder.HasIndex(sh => sh.Email).IsUnique();
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(u => u.ModifiedAt)
                .HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
            builder.HasMany(sh => sh.Packages)
                .WithOne(or => or.Shipper).HasForeignKey(or => or.ShipperId);
            builder.HasMany(sh => sh.Wallets)
                 .WithOne(wl => wl.Shipper).HasForeignKey(wl => wl.ShipperId);
            builder.HasMany(sh => sh.Routes)
                .WithOne(r => r.Shipper).HasForeignKey(r => r.ShipperId);
        }
    }
}
