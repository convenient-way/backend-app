/*using Microsoft.EntityFrameworkCore;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Data;
using unitofwork_core.Entities;

namespace unitofwork_core.Core.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {

        }
        public override User? GetById(Guid id)
        {
            return _dbSet.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
        }
        public override async Task<User?> GetByIdAsync(Guid id)
        {
            return  await _dbSet.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
*/