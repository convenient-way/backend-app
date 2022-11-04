
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using unitofwork_core.Constant.Wallet;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;
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
            shipper.Address = model.Address;
            shipper.HomeLongitude = model.HomeLongitude;
            shipper.HomeLatitude = model.HomeLatitude;
            shipper.DestinationLongitude = model.DestinationLongitude;
            shipper.DestinationLatitude = model.DestinationLatitude;

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
    }
}
