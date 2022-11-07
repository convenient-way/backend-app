using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.ShipperRouteModel;

namespace unitofwork_core.Service.ShipperRouteService
{
    public interface IShipperRouteService
    {
        Task<ApiResponse<List<ResponseShipperRouteModel>>> GetRouteShipper(Guid shipperId);
        Task<ApiResponse> SetActiveRoute(Guid routeId);
        Task<ApiResponse<ResponseShipperRouteModel>> RegisterRoute(RegisterShipperRouteModel model);
    }
}
