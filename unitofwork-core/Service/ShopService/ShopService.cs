using unitofwork_core.Constant.Shop;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;
using unitofwork_core.Entities.Extension;
using unitofwork_core.Model.Shop;

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
            
            await _shopRepo.InsertAsync(shop);
            await _unitOfWork.CompleteAsync();
            return shop.ToResponseModel();
    }
    }
}
