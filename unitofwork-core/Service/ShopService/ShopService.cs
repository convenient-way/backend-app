using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using unitofwork_core.Constant.Shop;
using unitofwork_core.Constant.Wallet;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.CollectionModel;
using unitofwork_core.Model.ShopModel;

namespace unitofwork_core.Service.ShopService
{
    public class ShopService : IShopService
    {
        private readonly ILogger<ShopService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShopRepository _shopRepo;
        public ShopService(ILogger<ShopService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _shopRepo = unitOfWork.Shops;
        }

        public async Task<ApiResponse<ResponseShopModel?>> GetById(Guid id)
        {
            ApiResponse<ResponseShopModel?> response = new ApiResponse<ResponseShopModel?>();
            #region Includable
            Func<IQueryable<Shop>, IIncludableQueryable<Shop, object>> include = (shop) => shop.Include(s => s.Wallets);
            #endregion
            Shop? shop = await _shopRepo.GetByIdAsync(id, include: include);
            if (shop == null) {
                response.ToFailedResponse("Shop không tồn tại");
                return response;
            }
            response.ToSuccessResponse(shop!.ToResponseModel(), "Lấy thông tin thành công");
            return response;
        }

        public async Task<ApiResponsePaginated<ResponseShopModel>> GetList(int pageIndex, int pageSize)
        {
            ApiResponsePaginated<ResponseShopModel> response = new();
            #region Verify params
            if (pageIndex < 0 || pageSize < 1)
            {
                response.ToFailedResponse("Thông tin phân trang không hợp lệ");
                return response;
            }
            #endregion
            #region Includable
            Func<IQueryable<Shop>, IIncludableQueryable<Shop, object>> include = (shop) => shop.Include(s => s.Wallets);
            #endregion
            #region Selector
            Expression<Func<Shop, ResponseShopModel>> selector = (source) => source.ToResponseModel();
            #endregion
            PaginatedList<ResponseShopModel> shops = await _shopRepo.GetPagedListAsync(predicate: null, include: include,
                selector: selector, pageIndex: pageIndex, pageSize: pageSize);
            response.ToSuccessResponse(shops, "Lấy thông tin thành công");
            return response;
        }

        public async Task<ApiResponse<ResponseShopModel>> Register(RegisterShopModel model)
        {
            ApiResponse<ResponseShopModel> response = new ApiResponse<ResponseShopModel>();
            #region verify params
            Shop? _checkEmail = await _shopRepo.GetSingleOrDefaultAsync(predicate: (sh => sh.Email == model.Email));
            if (_checkEmail != null) {
                response.ToFailedResponse("Email đã tồn tại, không thể đăng kí");
                return response;
            }
            Shop? _checkUserName = await _shopRepo.GetSingleOrDefaultAsync(predicate: (sh => sh.UserName == model.UserName));
            if (_checkEmail != null)
            {
                response.ToFailedResponse("User name đã tồn tại, không thể đăng kí");
                return response;
            }
            #endregion


            Shop shop = new Shop();

            shop.UserName = model.UserName;
            shop.Password = model.Password;
            shop.Email = model.Email;
            shop.DisplayName = model.DisplayName;
            shop.PhoneNumber = model.PhoneNumber;
            shop.PhotoUrl = model.PhotoUrl;
            shop.Status = ShopStatus.WAITING;
            shop.Address = model.Address;
            shop.Longitude = model.Longitude;
            shop.Latitude = model.Latitude;

            Wallet defaultWallet = new Wallet
            {
                WalletType = WalletType.DEFAULT,
                Status = WalletStatus.ACTIVE,
                ShopId = shop.Id
            };
            Wallet promotionWallet = new Wallet
            {
                WalletType = WalletType.PROMOTION,
                Status = WalletStatus.ACTIVE,
                ShopId = shop.Id
            };
            List<Wallet> wallets = new List<Wallet> {
                defaultWallet, promotionWallet
            };
            shop.Wallets = wallets;
            await _shopRepo.InsertAsync(shop);
            int result = await _unitOfWork.CompleteAsync();
            if (result > 0) {
                response.ToSuccessResponse(shop.ToResponseModel(), "Đăng kí shop thành công");
            }
            else
            {
                response.ToFailedResponse("Đăng kí không thành công, lỗi không xác định");
            }
            return response;
        }

        
    }
}
