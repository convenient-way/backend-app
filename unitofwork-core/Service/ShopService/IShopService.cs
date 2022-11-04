using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.ShopModel;

namespace unitofwork_core.Service.ShopService
{
    public interface IShopService
    {
        Task<ApiResponse<ResponseShopModel?>> GetById(Guid id);
        Task<ResponseShopModel> Register(RegisterShopModel model);
    }
}
