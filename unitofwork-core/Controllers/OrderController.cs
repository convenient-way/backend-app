using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using unitofwork_core.Model.ApiResponse;
using unitofwork_core.Model.Order;
using unitofwork_core.Service.OrderService;

namespace unitofwork_core.Controllers
{
    public class OrderController : BaseApiController
    {

        private readonly ILogger<ShopController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<ShopController> logger, IOrderService orderSerivce)
        {

            _logger = logger;
            _orderService = orderSerivce;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create order")]
        public async Task<ActionResult<ApiResponse<ResponseOrderModel>>> Create(CreateOrderModel model)
        {
            try
            {
                ResponseOrderModel repsponseOrder = await _orderService.Create(model);
                return Ok(new ApiResponse<ResponseOrderModel> { 
                    Message = "Tạo đơn thành công",
                    Data = repsponseOrder
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