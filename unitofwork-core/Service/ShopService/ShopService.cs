using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using unitofwork_core.Constant.Shop;
using unitofwork_core.Constant.Wallet;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;
using unitofwork_core.Model.ApiResponseModel;
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

        public async Task<ResponseShopModel> Register(RegisterShopModel model)
        {
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
            await _unitOfWork.CompleteAsync();
            return shop.ToResponseModel();
        }

        
    }
}
