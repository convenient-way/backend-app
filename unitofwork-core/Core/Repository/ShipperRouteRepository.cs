using unitofwork_core.Core.IRepository;
using unitofwork_core.Data;
using unitofwork_core.Entities;

namespace unitofwork_core.Core.Repository
{
    public class ShipperRouteRepository : GenericRepository<ShipperRoute>, IShipperRouteRepository
    {
        public ShipperRouteRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
