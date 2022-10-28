using unitofwork_core.Model.Shop;

namespace unitofwork_core.Service.ShopService
{
    public interface IShopService
    {
        Task<ResponseShopModel> Register(RegisterShopModel model);
    }
}
