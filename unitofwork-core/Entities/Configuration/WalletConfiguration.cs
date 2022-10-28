using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace unitofwork_core.Entities.Configuration
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("Wallet");
            builder.Property(w => w.Balance).HasColumnType("decimal(10,0)");
            builder.HasMany(wl => wl.Transactions)
                .WithOne(tr => tr.Wallet).HasForeignKey(tr => tr.WalletId);
        }
    }
}
