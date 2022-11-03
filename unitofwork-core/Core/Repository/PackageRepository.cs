using unitofwork_core.Core.IRepository;
using unitofwork_core.Data;
using unitofwork_core.Entities;

namespace unitofwork_core.Core.Repository
{
    public class PackageRepository : GenericRepository<Package>, IPackageRepository
    {
        public PackageRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
