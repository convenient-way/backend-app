using unitofwork_core.Constant.Wallet;
using unitofwork_core.Model.ApiResponse;
using unitofwork_core.Model.Order;

namespace unitofwork_core.Service.OrderService
{
    public interface IOrderService
    {
        Task<ResponseOrderModel> Create(CreateOrderModel model);
        Task<bool> ApproveOrder(Guid id);
        Task<bool> RejectOrder(Guid id);
        Task<List<ResponseOrderModel>> SuggestPakage(Guid shiperId);
        Task<ApiResponse<bool>> PickUpPakage(Guid shipperId, Guid orderId, string walletType = WalletType.DEFAULT);
    }
}