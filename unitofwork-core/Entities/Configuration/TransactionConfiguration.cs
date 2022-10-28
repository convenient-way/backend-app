using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace unitofwork_core.Entities.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");
            builder.Property(t => t.CoinExchange).HasColumnType("decimal(10,0)");
            builder.Property(t => t.BalanceWallet).HasColumnType("decimal(10,0)");
        }
    }
}
