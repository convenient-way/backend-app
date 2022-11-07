using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace unitofwork_core.Entities.Configuration
{
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.ToTable("Package");

            builder.Property(p => p.PriceShip).HasColumnType("decimal(10,0)");
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(u => u.ModifiedAt)
                .HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
            builder.HasMany(p => p.HistoryPackages)
                .WithOne(hp => hp.Package).HasForeignKey(hp => hp.PackageId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Transactions)
                .WithOne(tr => tr.Package).HasForeignKey(tr => tr.PackageId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Products)
                .WithOne(pr => pr.Package).HasForeignKey(pr => pr.PackageId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
