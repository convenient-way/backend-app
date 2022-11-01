using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using unitofwork_core.Constant.Order;
using unitofwork_core.Constant.OrderRouting;
using unitofwork_core.Constant.Transaction;
using unitofwork_core.Constant.Wallet;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;
using unitofwork_core.Model.ApiResponse;
using unitofwork_core.Model.Mapbox;
using unitofwork_core.Model.Order;
using unitofwork_core.Service.MapboxService;

namespace unitofwork_core.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepo;
        private readonly IShipperRepository _shipperRepo;
        private readonly IWalletRepository _walletRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IMapboxService _mapboxService;
        public OrderService(ILogger<OrderService> logger, IUnitOfWork unitOfWork, IMapboxService mapboxService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _orderRepo = unitOfWork.Orders;
            _shipperRepo = unitOfWork.Shippers;
            _walletRepo = unitOfWork.Wallets;
            _transactionRepo = unitOfWork.Transactions;
            _mapboxService = mapboxService;
        }

        public async Task<bool> ApproveOrder(Guid id)
        {
            Order? order = await _orderRepo.GetByIdAsync(id, disableTranking: false);
            if (order != null) {
                if (order.Status != OrderStatus.WAITING) return false;
                order.Status = OrderStatus.APPROVED;
                return true;
            }
            return false;
        }

        public async Task<bool> RejectOrder(Guid id)
        {
            Order? order = await _orderRepo.GetByIdAsync(id, disableTranking: false);
            if (order != null)
            {
                if (order.Status != OrderStatus.WAITING) return false;
                order.Status = OrderStatus.REJECT;
                return true;
            }
            return false;
        }

        public async Task<ApiResponse<bool>> PickUpPakage(Guid shipperId, Guid orderId, string walletType = WalletType.DEFAULT) {
            Shipper? shipper = await _shipperRepo.GetByIdAsync(shipperId, disableTranking: false);
            Order? order = await _orderRepo.GetByIdAsync(orderId, disableTranking: false);

            #region Filter wallet shipper
            Expression<Func<Wallet, bool>> predicateShipperWallet = (wallet) => wallet.WalletType == walletType && wallet.ShipperId == shipperId;
            #endregion
            #region Filter wallet system
            Expression<Func<Wallet, bool>> predicateSystemWallet = (wallet) => wallet.WalletType == WalletType.SYSTEM;
            #endregion
            Wallet? shipperWallet = await _walletRepo.GetSingleOrDefaultAsync(predicate: predicateShipperWallet, disableTracking: false);
            Wallet? systemWallet = await _walletRepo.GetSingleOrDefaultAsync(predicate: predicateSystemWallet, disableTracking: false);

            #region Check null and check balance wallet
            ApiResponse<bool> apiResponseFailure = new ApiResponse<bool>
            {
                Success = false,
                Message = "Thông tin shipper hoặc order bị sai",
                Data = false
            };
            if (shipper == null || order == null || shipperWallet == null || systemWallet == null) {
                return apiResponseFailure;

            }
            if (order.Status != OrderStatus.APPROVED) {
                apiResponseFailure.Message = "Đơn hàng đang ở trạng khác APPROVED không thể pickup";
                return apiResponseFailure;
            }
            decimal remainingCoin = shipperWallet.Balance - order.Price;
            if (remainingCoin < 0) {
                apiResponseFailure.Message = "Số dư ví không đủ";
                return apiResponseFailure;
            }
            #endregion

            #region Create transations and update wallet shipper and system
            Transaction systemTrans = new Transaction();
            systemTrans.Description = shipper.Email + " đã nhận đơn hàng với id: " + order.Id;
            systemTrans.Status = TransactionStatus.ACCOMPLISHED;
            systemTrans.TransactionType = TransactionType.PICKUP;
            systemTrans.CoinExchange = order.Price;
            systemTrans.BalanceWallet = systemWallet.Balance + order.Price;
            systemTrans.OrderId = order.Id;
            systemTrans.WalletId = systemWallet.Id;

            Transaction shipperTrans = new Transaction();
            shipperTrans.Description = "Đã nhận đơn hàng id : " + order.Id;
            shipperTrans.Status = TransactionStatus.ACCOMPLISHED;
            shipperTrans.TransactionType = TransactionType.PICKUP;
            shipperTrans.CoinExchange = order.Price;
            shipperTrans.BalanceWallet = shipperWallet.Balance - order.Price;
            shipperTrans.OrderId = order.Id;
            shipperTrans.WalletId= shipperWallet.Id;

            systemWallet.Balance = systemWallet.Balance + order.Price;
            shipperWallet.Balance = shipperWallet.Balance - order.Price;

            order.Status = OrderStatus.DELIVERY;

            List<Transaction> transactions = new List<Transaction> {
                systemTrans, shipperTrans
            };

            #endregion

            await _transactionRepo.InsertAsync(transactions);
            await _unitOfWork.CompleteAsync();

            ApiResponse<bool> reponseSuccess = new ApiResponse<bool> { 
                Message = "Nhận đơn hàng thành công",
                Data = true
            };
            return reponseSuccess;
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

        public async Task<List<ResponseOrderModel>> SuggestPakage(Guid shiperId)
        {
            List<ResponseOrderModel> result = new List<ResponseOrderModel>();
            Shipper? shipper = await _shipperRepo.GetByIdAsync(shiperId);
            if (shipper != null) { 
                GeoCoordinate homeCoordinate = new GeoCoordinate(shipper.HomeLatitude, shipper.HomeLongitude);
                GeoCoordinate destinationCoordinate = new GeoCoordinate(shipper.DestinationLatitude, shipper.DestinationLongitude);

                PolyLineModel polyLineShipper = await _mapboxService.GetPolyLine(homeCoordinate, destinationCoordinate);

                #region Includale order
                Func<IQueryable<Order>, IIncludableQueryable<Order, object?>> include =
                    (source) => source.Include(user => user.OrderRoutings).ThenInclude(orRoute => orRoute.Products);
                #endregion
                List<Order> orders = (await _orderRepo.GetAllAsync(include: include)).ToList();
                int orderCount = orders.Count;
                for (int i = 0; i < orderCount; i++)
                {
                    bool isValidOrder = ValidDestinationBetweenShipperLineAndPakage(polyLineShipper, orders[i]);
                    if (isValidOrder) result.Add(orders[i].ToResponseModel());
                }
            }
            return result;
        }

        public bool ValidDestinationBetweenShipperLineAndPakage(PolyLineModel polyLine, Order order, double spacingValid = 2000) {
            bool result = false;
            List<GeoCoordinate>? geoCoordinateList = polyLine.PolyPoints;
            if (geoCoordinateList != null) {
                int countLine = geoCoordinateList.Count;
                List<OrderRouting> orderRoutings = order.OrderRoutings.ToList();
                int countOrderRouting = orderRoutings.Count;
                for (int i = 0; i < countLine; i++)
                {
                    int validNumberCoor = 0;

                    GeoCoordinate shopCoordinate = new GeoCoordinate(order.StartLatitude, order.StartLongitude);
                    double distanceShop = geoCoordinateList[i].GetDistanceTo(shopCoordinate);
                    if(distanceShop < spacingValid)  validNumberCoor++; 

                    for (int j = 0; j < countOrderRouting; j++)
                    {
                        GeoCoordinate orderRoutingPoint = new GeoCoordinate(orderRoutings[j].ToLatitude, orderRoutings[j].ToLongitude);
                        double distance = geoCoordinateList[i].GetDistanceTo(orderRoutingPoint);
                        if (distance < spacingValid) {
                            validNumberCoor++;
                        }
                    }
                    if (validNumberCoor == countOrderRouting + 1) {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
