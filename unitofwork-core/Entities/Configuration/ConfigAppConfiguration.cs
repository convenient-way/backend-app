using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace unitofwork_core.Entities.Configuration
{
    public class ConfigAppConfiguration : IEntityTypeConfiguration<ConfigApp>
    {
        public void Configure(EntityTypeBuilder<ConfigApp> builder)
        {
            builder.ToTable("Config");
            builder.Property(u => u.ModifiedAt)
                .HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
        }
    }
}
