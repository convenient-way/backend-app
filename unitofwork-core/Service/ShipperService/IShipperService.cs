using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.CollectionModel;
using unitofwork_core.Model.ShipperModel;

namespace unitofwork_core.Service.ShipperService
{
    public interface IShipperService
    {
        Task<ResponseShipperModel> Register(RegisterShipperModel model);
        Task<ResponseShipperModel?> GetShipperId(Guid id);
        Task<ApiResponsePaginated<ResponseShipperModel>> GetShippers(int pageIndex, int pageSize);
    }
}
