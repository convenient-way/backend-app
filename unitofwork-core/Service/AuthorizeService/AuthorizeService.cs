using System.Linq.Expressions;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;
using unitofwork_core.Helper;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.AuthorizeModel;
using Vonage;
using Vonage.Request;
using Vonage.Verify;

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
            ApiResponse<ResponseLoginModel> response = new ApiResponse<ResponseLoginModel>();
            response.Data = new ResponseLoginModel();
            Actor? userExist = null;
            string roleName = "";
            if (isShipper) {
                Expression<Func<Shipper,bool>> predicate = (ship) => ship.UserName.Equals(model.UserName) && ship.Password.Equals(model.Password);
                Shipper? shipper = await _shipperRepo.GetSingleOrDefaultAsync(predicate);
                if (shipper != null) {
                    response.Data.Shipper = shipper.ToResponseModel();
                    roleName = "SHIPPER";
                }
                userExist = shipper;
            }
            if (isShop) {
                Expression<Func<Shop, bool>> predicate = (shop) => shop.UserName.Equals(model.UserName) && shop.Password.Equals(model.Password);
                Shop? shop = await _shopRepo.GetSingleOrDefaultAsync(predicate);
                if (shop != null)
                {
                    response.Data.Shop = shop.ToResponseModel();
                    roleName = "SHOP";
                }
                userExist = shop;
            }
            if (isAdmin)
            {
                Expression<Func<Admin, bool>> predicate = (admin) => admin.UserName.Equals(model.UserName) && admin.Password.Equals(model.Password);
                Admin? admin = await _adminRepo.GetSingleOrDefaultAsync(predicate);
                if (admin != null) { 
                    response.Data.Admin = admin.ToResponseModel();
                    roleName = "ADMIN";
                }
                userExist = admin;
            }
            if (userExist != null) {
                response.Data.Token = _jwtHelper.generateJwtToken(userExist, roleName);
                response.Message = "Đăng nhập thành công";
            }
            else
            {
                response.Success = false;
                response.Message = "Sai tên tài khoản hoặc mật khẩu";
            }

            return response;
        }

        public async Task<ApiResponse<string>> SentOtp(string phone)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            var credentials = Credentials.FromApiKeyAndSecret("bbe37b49", "9pVLaDGyKKkE4VFD");
            var client = new VonageClient(credentials);
            var request = new VerifyRequest()
            {
                Brand = "T3T",
                Number = phone,
            };
            var responseVonage = await client.VerifyClient.VerifyRequestAsync(request);
            Console.WriteLine($"Verify Request Complete\nStatus:{responseVonage.Status}\nRequest ID:{responseVonage.RequestId} {request}");
            if (responseVonage.Status == "0")
            {
                response.ToSuccessResponse(responseVonage.RequestId, "Xác thực OTP với request id này");
                return response;
            }
            else {
                response.ToSuccessResponse(responseVonage.ErrorText, "Số điện thoại không hợp lệ");
                return response;
            }
        }

        public async Task<ApiResponse> VerifyOtp(string requestId, string otpCode)
        {
            ApiResponse response = new ApiResponse();
            var credentials = Credentials.FromApiKeyAndSecret("bbe37b49", "9pVLaDGyKKkE4VFD");
            var client = new VonageClient(credentials);
            var request = new VerifyCheckRequest() { Code = otpCode, RequestId = requestId };
            var responseVonage = await client.VerifyClient.VerifyCheckAsync(request);
            int statusCode = int.Parse(responseVonage.Status);
            if (statusCode == 0) {
                response.ToSuccessResponse("Xác thực thành công");
            }
            else
            {
                response.ToFailedResponse("Xác thực thất bại");
            }
            return response;
        }
    }
}
