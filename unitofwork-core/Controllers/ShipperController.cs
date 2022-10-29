using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using unitofwork_core.Model.ApiResponse;
using unitofwork_core.Model.Shipper;
using unitofwork_core.Service.ShipperService;

namespace unitofwork_core.Controllers
{
    public class ShipperController : BaseApiController
    {

        private readonly IShipperService _shipperService;
        private readonly ILogger<ShopController> _logger;

        public ShipperController(IShipperService shipperService, ILogger<ShopController> logger)
        {
            _shipperService = shipperService;
            _logger = logger;
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register shipper")]
        public async Task<ActionResult<ApiResponse<ResponseShipperModel>>> Register(RegisterShipperModel model)
        {
            try
            {
                ResponseShipperModel shopResponse = await _shipperService.Register(model);
                return Ok(new ApiResponse<ResponseShipperModel>
                {
                    Message = "Đăng kí thành công",
                    Data = shopResponse
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Register shipper has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
