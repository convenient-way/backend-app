using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.ShopModel;
using unitofwork_core.Service.ShopService;

namespace unitofwork_core.Controllers
{
    public class ShopController : BaseApiController
    {
        private readonly IShopService _shopService;
        private readonly ILogger<ShopController> _logger;

        public ShopController(IShopService shopService, ILogger<ShopController> logger)
        {
            _shopService = shopService;
            _logger = logger;
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary ="Register shop")]
        public async Task<ActionResult<ApiResponse<ResponseShopModel>>> Register(RegisterShopModel model)
        {
            try
            {
                ResponseShopModel shopResponse = await _shopService.Register(model);
                return Ok(new ApiResponse<ResponseShopModel> { 
                    Message = "Đăng kí thành công",
                    Data = shopResponse
                });
            }
            catch (Exception ex) {
                _logger.LogError("Register shop has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Register shop")]
        public async Task<ActionResult<ApiResponse<ResponseShopModel?>>> GetId(Guid id)
        {
            try
            {
                ApiResponse<ResponseShopModel?> response = await _shopService.GetById(id);
                if (response.Success == false) {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Register shop has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
