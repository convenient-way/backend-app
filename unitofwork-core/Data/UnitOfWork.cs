using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Core.Repository;

namespace unitofwork_core.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;
   /*     public IUserRepository Users { get; private set; }
        public IRoleRepository Roles { get; private set; }*/
            public IShopRepository Shops { get; private set; }


        public UnitOfWork(AppDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
            Shops = new ShopRepository(_context, _logger);

            /*Users = new UserRepository(_context, _logger);
            Roles = new RoleRepository(_context, _logger);*/
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
