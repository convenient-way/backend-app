using unitofwork_core.Core.IRepository;
using unitofwork_core.Core.Repository;
using unitofwork_core.Entities;

namespace unitofwork_core.Core.IConfiguraton
{
    public interface IUnitOfWork
    {
        IShopRepository Shops {get;}
        IShipperRepository Shippers {get;}
        IAdminRepository Admins {get;}
        IPackageRepository Packages {get;}
        IHistoryPackageRepostiory HistoryPackages {get;}
        IConfigRepostiory ConfigApps { get; }
        IWalletRepository Wallets {get;}
        IProductRepository Products {get;}
        ITransactionRepository Transactions {get;}
        IShipperRouteRepository ShipperRoutes { get; }

        Task<int> CompleteAsync();
        int Complete();
    }
}
