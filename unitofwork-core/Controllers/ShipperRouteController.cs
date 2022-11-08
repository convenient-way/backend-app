using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.ShipperModel;
using unitofwork_core.Model.ShipperRouteModel;
using unitofwork_core.Service.ShipperRouteService;

namespace unitofwork_core.Controllers
{ 
    public class ShipperRouteController : BaseApiController
    {
        private readonly IShipperRouteService _routeService;
        private readonly ILogger<ShipperRouteController> _logger;

        public ShipperRouteController(IShipperRouteService routeService, ILogger<ShipperRouteController> logger)
        {
            _routeService = routeService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get shipper with id")]
        public async Task<ActionResult<ApiResponse<List<ResponseShipperRouteModel>>>> GetShipperRoute(Guid id)
        {
            try
            {
                ApiResponse<List<ResponseShipperRouteModel>>? response = await _routeService.GetRouteShipper(id);
                if (response.Success == false) {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get shipper route : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register route for shipper")]
        public async Task<ActionResult<ApiResponse<ResponseShipperModel>>> RegisterRoute(RegisterShipperRouteModel model)
        {
            try
            {
                ApiResponse<ResponseShipperModel> response = await _routeService.RegisterRoute(model);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Register route exception : " + ex.Message.Substring(0, 100));
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("active-route")]
        [SwaggerOperation(Summary = "Active route")]
        public async Task<ActionResult<ApiResponse<List<ResponseShipperRouteModel>>>> ActiveRoute(Guid id)
        {
            try
            {
                ApiResponse response = await _routeService.SetActiveRoute(id);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Active route : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
