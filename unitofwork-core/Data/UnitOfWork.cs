using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Core.Repository;
using unitofwork_core.Entities;

namespace unitofwork_core.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        public IShopRepository Shops { get; private set; }
        public IShipperRepository Shippers { get; private set; }
        public IAdminRepository Admins { get; private set; }
        public IWalletRepository Wallets { get; private set; }
        public IPackageRepository Packages { get; private set; }
        public IConfigRepostiory ConfigApps { get; private set; }
        public IHistoryPackageRepostiory HistoryPackages { get; private set; }
        public IProductRepository Products { get; private set; }
        public ITransactionRepository Transactions { get; private set; }
        public IShipperRouteRepository ShipperRoutes { get; private set; }
        public UnitOfWork(AppDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
            Shops = new ShopRepository(_context, _logger);
            Shippers = new ShipperRepository(_context, _logger);
            Admins = new AdminRepository(_context, _logger);
            Wallets = new WalletRepository(_context, _logger);
            Products = new ProductRepository(_context, _logger);
            Transactions = new TransactionRepository(_context, _logger);
            Packages = new PackageRepository(_context, _logger);
            HistoryPackages = new HistoryPackageRepostiory(_context, _logger);
            ConfigApps = new ConfigRepository(_context, _logger);
            ShipperRoutes = new ShipperRouteRepository(_context, logger);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
