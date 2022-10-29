using unitofwork_core.Constant.Order;
using unitofwork_core.Constant.OrderRouting;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;
using unitofwork_core.Model.Order;

namespace unitofwork_core.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepo;
        public OrderService(ILogger<OrderService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _orderRepo = unitOfWork.Orders;
        }

        public async Task<ResponseOrderModel> Create(CreateOrderModel model)
        {
            Order order = new Order();
            order.NumberPackage = model.NumberPackage;
            order.StartLongitude = model.StartLongitude;
            order.StartLatitude = model.StartLatitude;
            order.Distance = model.Distance;
            order.Volume = model.Volume;
            order.Weight = model.Weight;
            order.ShopId = model.ShopId;
            order.Status = OrderStatus.WAITING;

            int orderRoutingCount = model.OrderRoutings.Count;
            for (int i = 0; i < orderRoutingCount; i++)
            {
                OrderRouting orderRoute = model.OrderRoutings[i].ConvertToEntity();
                orderRoute.Status = OrderRoutingStatus.WAITING;
                order.OrderRoutings.Add(orderRoute);
            }
            await _orderRepo.InsertAsync(order);
            await _unitOfWork.CompleteAsync();
            return order.ToResponseModel();
        }
    }
}
