using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.ShipperModel;
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

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get shipper with id")]
        public async Task<ActionResult<ApiResponse<ResponseShipperModel>>> GetShipperId(Guid id)
        {
            try
            {
                ResponseShipperModel? shopResponse = await _shipperService.GetShipperId(id);
                return Ok(new ApiResponse<ResponseShipperModel>
                {
                    Message = "Lấy thông tin thành công",
                    Data = shopResponse
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Register shipper has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
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
