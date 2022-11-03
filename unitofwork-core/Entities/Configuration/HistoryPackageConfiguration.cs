using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace unitofwork_core.Entities.Configuration
{
    public class HistoryPackageConfiguration : IEntityTypeConfiguration<HistoryPackage>
    {
        public void Configure(EntityTypeBuilder<HistoryPackage> builder)
        {
            builder.ToTable("HistoryPackage");
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        }
    }
}
