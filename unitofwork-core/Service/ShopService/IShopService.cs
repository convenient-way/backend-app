using unitofwork_core.Model.ShopModel;

namespace unitofwork_core.Service.ShopService
{
    public interface IShopService
    {
        Task<ResponseShopModel> Register(RegisterShopModel model);
    }
}
