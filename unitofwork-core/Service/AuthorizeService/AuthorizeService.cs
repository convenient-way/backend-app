using System.Linq.Expressions;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;
using unitofwork_core.Helper;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.AuthorizeModel;

namespace unitofwork_core.Service.AuthorizeService
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly ILogger<AuthorizeService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShipperRepository _shipperRepo;
        private readonly IShopRepository _shopRepo;
        private readonly IAdminRepository _adminRepo;
        private readonly IJWTHelper _jwtHelper;

        public AuthorizeService(ILogger<AuthorizeService> logger, IUnitOfWork unitOfWork, IJWTHelper jwtHelper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _shipperRepo = unitOfWork.Shippers;
            _shopRepo = unitOfWork.Shops;
            _adminRepo = unitOfWork.Admins;
            _jwtHelper = jwtHelper;
        }

        public async Task<ApiResponse<ResponseLoginModel>> Login(LoginModel model,bool isShop,bool isShipper,bool isAdmin)
        {
            Actor? userExist = null;
            if (isShipper) {
                Expression<Func<Shipper,bool>> predicate = (ship) => ship.UserName.Equals(model.UserName) && ship.Password.Equals(model.Password);
                Shipper? shipper = await _shipperRepo.GetSingleOrDefaultAsync(predicate);
                userExist = shipper;
            }
            if (isShop) {
                Expression<Func<Shop, bool>> predicate = (shop) => shop.UserName.Equals(model.UserName) && shop.Password.Equals(model.Password);
                Shop? shop = await _shopRepo.GetSingleOrDefaultAsync(predicate);
                userExist = shop;
            }
            if (isAdmin)
            {
                Expression<Func<Admin, bool>> predicate = (admin) => admin.UserName.Equals(model.UserName) && admin.Password.Equals(model.Password);
                Admin? admin = await _adminRepo.GetSingleOrDefaultAsync(predicate);
                userExist = admin;
            }
            ApiResponse<ResponseLoginModel> response = new ApiResponse<ResponseLoginModel> ();
            ResponseLoginModel responseLoginModel = new ResponseLoginModel();
            if (userExist != null) {
                responseLoginModel.Token = _jwtHelper.generateJwtToken(userExist);
                response.Message = "Đăng nhập thành công";
                response.Data = responseLoginModel;
            }
            else
            {
                response.Success = false;
                response.Message = "Sai tên tài khoản hoặc mật khẩu";
            }

            return response;
        }
    }
}
