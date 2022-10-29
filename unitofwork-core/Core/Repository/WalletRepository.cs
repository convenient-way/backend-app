using unitofwork_core.Core.IRepository;
using unitofwork_core.Data;
using unitofwork_core.Entities;

namespace unitofwork_core.Core.Repository
{
    public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
