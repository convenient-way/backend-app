using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace unitofwork_core.Entities.Configuration
{
    public class HistoryOrderConfiguration : IEntityTypeConfiguration<HistoryOrder>
    {
        public void Configure(EntityTypeBuilder<HistoryOrder> builder)
        {
            builder.ToTable("HistoryOrder");
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        }
    }
}
