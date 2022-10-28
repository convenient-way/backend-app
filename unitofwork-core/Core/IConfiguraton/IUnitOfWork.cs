using unitofwork_core.Core.IRepository;

namespace unitofwork_core.Core.IConfiguraton
{
    public interface IUnitOfWork
    {
        IShopRepository Shops {get;}
        Task CompleteAsync();
        void Complete();
    }
}
