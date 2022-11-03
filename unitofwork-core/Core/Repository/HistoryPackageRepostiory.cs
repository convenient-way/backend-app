using unitofwork_core.Core.IRepository;
using unitofwork_core.Data;
using unitofwork_core.Entities;

namespace unitofwork_core.Core.Repository
{
    public class HistoryPackageRepostiory : GenericRepository<HistoryPackage>, IHistoryPackageRepostiory
    {
        public HistoryPackageRepostiory(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
