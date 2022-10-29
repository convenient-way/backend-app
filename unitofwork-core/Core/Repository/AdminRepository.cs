using unitofwork_core.Core.IRepository;
using unitofwork_core.Data;
using unitofwork_core.Entities;

namespace unitofwork_core.Core.Repository
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
