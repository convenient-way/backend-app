using Microsoft.EntityFrameworkCore;
using unitofwork_core.Entities;
using System.Reflection;

namespace unitofwork_core.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        #region Dbset
        public virtual DbSet<Admin> Admins => Set<Admin>();
        public virtual DbSet<Shipper> Shipper => Set<Shipper>();
        public virtual DbSet<Shop> Shops => Set<Shop>();
        public virtual DbSet<Package> Packages => Set<Package>();
        public virtual DbSet<Product> Products => Set<Product>();
        public virtual DbSet<ConfigApp> ConfigApps => Set<ConfigApp>();
        public virtual DbSet<Transaction> Transactions => Set<Transaction>();
        public virtual DbSet<Wallet> Wallets => Set<Wallet>();
        public virtual DbSet<ShipperRoute> Routes => Set<ShipperRoute>();
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string connectionString = _configuration.GetConnectionString("DevConnection");
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                connectionString = _configuration.GetConnectionString("DevConnection");
            }
            else
            {
                connectionString = _configuration.GetConnectionString("AzureConnection");
            }
            if (!string.IsNullOrEmpty(connectionString)) optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
