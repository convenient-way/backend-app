
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using unitofwork_core.Constant.Wallet;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.CollectionModel;
using unitofwork_core.Model.ShipperModel;

namespace unitofwork_core.Service.ShipperService
{
    public class ShipperService : IShipperService
    {
        private readonly ILogger<ShipperService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShipperRepository _shipperRepo;

        public ShipperService(ILogger<ShipperService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _shipperRepo = unitOfWork.Shippers;
        }


        public async Task<ResponseShipperModel> Register(RegisterShipperModel model)
        {
            Shipper shipper = new Shipper();
            shipper.UserName = model.UserName;
            shipper.Password = model.Password;
            shipper.Email = model.Email;
            shipper.DisplayName = model.DisplayName;
            shipper.PhoneNumber = model.PhoneNumber;
            shipper.PhotoUrl = model.PhotoUrl;
            shipper.Status = model.Status;
            shipper.Gender = model.Gender;
            shipper.Address = model.HomeAddress;
           
            ShipperRoute route = new ShipperRoute();
            route.FromLongitude = model.HomeLongitude;
            route.FromLatitude = model.HomeLatitude;
            route.FromAddress = model.HomeAddress;
            route.ToLongitude = model.DestinationLongitude;
            route.ToLatitude = model.DestinationLatitude;
            route.ToAddress = model.DestinationAddress;
            route.IsActive = true;
            shipper.Routes.Add(route);

            Wallet defaultWallet = new Wallet {
                WalletType = WalletType.DEFAULT,
                Status = WalletStatus.ACTIVE,
                ShipperId = shipper.Id,
                Balance = 1000000
            };
            Wallet promotionWallet = new Wallet
            {
                WalletType = WalletType.PROMOTION,
                Status = WalletStatus.ACTIVE,
                ShipperId = shipper.Id
            };
            List<Wallet> wallets = new List<Wallet> { 
                defaultWallet, promotionWallet
            };
            shipper.Wallets = wallets;

            await _shipperRepo.InsertAsync(shipper);
            await _unitOfWork.CompleteAsync();
            return shipper.ToResponseModel();
        }

        public async Task<ResponseShipperModel?> GetShipperId(Guid shipperId)
        {
            ResponseShipperModel? model = null;
            #region Includable shipper
            Func<IQueryable<Shipper>, IIncludableQueryable<Shipper, object?>> include = (shipper) => shipper.Include(sh => sh.Wallets);
            #endregion
            Shipper? shipper = await _shipperRepo.GetByIdAsync(id: shipperId, include: include);
            if(shipper != null) model = shipper.ToResponseModel();
            return model;
        }

        public async Task<ApiResponsePaginated<ResponseShipperModel>> GetShippers(int pageIndex, int pageSize) {
            ApiResponsePaginated<ResponseShipperModel> response = new ApiResponsePaginated<ResponseShipperModel>();
            #region Verify params
            if (pageIndex < 0 || pageSize < 1)
            {
                response.ToFailedResponse("Thông tin phân trang không hợp lệ");
                return response;
            }
            #endregion
            #region Includable
            Func<IQueryable<Shipper>, IIncludableQueryable<Shipper, object?>> include = (shipper) => shipper.Include(sh => sh.Wallets);
            #endregion
            #region Selector
            Expression<Func<Shipper, ResponseShipperModel>> selector = (source) => source.ToResponseModel();
            #endregion
            PaginatedList<ResponseShipperModel> shippers = await _shipperRepo.GetPagedListAsync<ResponseShipperModel>(include: include, 
                pageIndex: pageIndex, pageSize: pageSize, selector: selector, predicate: null);
            response.ToSuccessResponse(shippers, "Lấy thông tin thành công");
            return response;
        }
    }
}
