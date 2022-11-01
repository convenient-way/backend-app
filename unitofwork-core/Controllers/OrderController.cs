using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using unitofwork_core.Constant.Order;
using unitofwork_core.Model.ApiResponse;
using unitofwork_core.Model.Order;
using unitofwork_core.Service.OrderService;

namespace unitofwork_core.Controllers
{
    public class OrderController : BaseApiController
    {

        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderSerivce)
        {

            _logger = logger;
            _orderService = orderSerivce;
        }

        [HttpGet("suggest-pakage")]
        public async Task<ActionResult<ApiResponse<List<ResponseOrderModel>>>> SuggestPakage(Guid shipperId)
        {
            try {
                List<ResponseOrderModel> orders = await _orderService.SuggestPakage(shipperId);
                return Ok(new ApiResponse<List<ResponseOrderModel>> { 
                    Message = "Đơn hàng đề xuất cho shipepr",
                    Data = orders
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Suggest order has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }

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
        [HttpPost("status")]
        [SwaggerOperation(Summary = "Change status order")]
        public async Task<IActionResult> ChangeStatus(ChangeOrderStatusModel model)
        {
            try
            {
                if (model.Status == OrderStatus.DELIVERY)
                {
                    ApiResponse<bool> response = await _orderService.PickUpPakage(model.Shipper, model.OrderId);
                    return Ok(response);
                }
                return BadRequest(new ApiResponse { 
                    Success = false,
                    Message = "Lỗi chưa xác định"
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