using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.ShipperRoute;

namespace unitofwork_core.Service.ShipperRouteService
{
    public class ShipperRouteService : IShipperRouteService
    {
        private readonly ILogger<ShipperRouteService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShipperRouteRepository _shipperRouteRepo;
        private readonly IShipperRepository _shipperRepo;

        public ShipperRouteService(ILogger<ShipperRouteService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _shipperRouteRepo = unitOfWork.ShipperRoutes;
            _shipperRepo = unitOfWork.Shippers;
        }

        public async Task<ApiResponse<List<ResponseShipperRouteModel>>> GetRouteShipper(Guid shipperId)
        {
            ApiResponse<List<ResponseShipperRouteModel>> response = new ApiResponse<List<ResponseShipperRouteModel>>();
            List<ResponseShipperRouteModel> routes = _shipperRouteRepo.GetAll(predicate: (route) => route.ShipperId == shipperId, selector: route => route.ToResponseModel()).ToList();
            if (routes.Count > 0) {
                response.ToSuccessResponse(routes, "Lấy thông tin thành công");
            }
            else{
                response.ToFailedResponse("Shipper không tồn tại");
            }
            return await Task.FromResult(response);

        }

        public async Task<ApiResponse> SetActiveRoute(Guid routeId)
        {

            ApiResponse response = new ApiResponse();
            ShipperRoute? route = await _shipperRouteRepo.GetByIdAsync(routeId);
            if (route != null) { 
                List<ShipperRoute> routes = (await _shipperRouteRepo.GetAllAsync(predicate:
                    route => route.ShipperId == route.ShipperId, disableTracking: false)).ToList();
                for (int i = 0; i < routes.Count; i++)
                {
                    if (routes[i].Id == route.Id)
                    {
                        routes[i].IsActive = true;
                    }
                    else { 
                        routes[i].IsActive = true;
                    }
                }
                response.ToSuccessResponse("Yêu cầu thành công");
                return response;
            }
            response.ToSuccessResponse("Yêu cầu không thành công");
            return response;
        }
    }
}
