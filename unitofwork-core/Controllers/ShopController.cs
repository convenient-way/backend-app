using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using unitofwork_core.Model.ApiResponse;
using unitofwork_core.Model.Shop;
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
    }
}
