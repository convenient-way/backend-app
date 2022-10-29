using unitofwork_core.Model.Order;

namespace unitofwork_core.Service.OrderService
{
    public interface IOrderService
    {
        Task<ResponseOrderModel> Create(CreateOrderModel model);
    }
}