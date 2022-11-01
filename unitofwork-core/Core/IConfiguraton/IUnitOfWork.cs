using unitofwork_core.Core.IRepository;

namespace unitofwork_core.Core.IConfiguraton
{
    public interface IUnitOfWork
    {
        IShopRepository Shops {get;}
        IShipperRepository Shippers {get;}
        IAdminRepository Admins {get;}
        IOrderRepository Orders {get;}
        IWalletRepository Wallets {get;}
        IOrderRoutingRepository OrderRoutings {get;}
        IProductRepository Products {get;}
        ITransactionRepository Transactions {get;}

        Task CompleteAsync();
        void Complete();
    }
}
