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

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get shop id")]
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
                _logger.LogError("Get shop id has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get shops")]
        public async Task<ActionResult<ApiResponsePaginated<ResponseShopModel>>> GetList(int pageIndex = 0, int pageSize = 20)
        {
            try
            {
                ApiResponsePaginated<ResponseShopModel> response = await _shopService.GetList(pageIndex, pageSize);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get shops id has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register shop")]
        public async Task<ActionResult<ApiResponse<ResponseShopModel>>> Register(RegisterShopModel model)
        {
            try
            {
                ResponseShopModel shopResponse = await _shopService.Register(model);
                return Ok(new ApiResponse<ResponseShopModel>
                {
                    Message = "Đăng kí thành công",
                    Data = shopResponse
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Register shop has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
