using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace unitofwork_core.Entities.Configuration
{
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {

        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.ToTable("Shop");
            builder.HasIndex(sh => sh.UserName).IsUnique();
            builder.HasIndex(sh => sh.Email).IsUnique();
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(u => u.ModifiedAt)
                    .HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
        }
    }
}
