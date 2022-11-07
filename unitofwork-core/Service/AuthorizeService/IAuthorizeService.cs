using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.AuthorizeModel;

namespace unitofwork_core.Service.AuthorizeService
{
    public interface IAuthorizeService
    {
        Task<ApiResponse<ResponseLoginModel>> Login(LoginModel model,bool isShop,bool isShipper,bool isAdmin);
        Task<ApiResponse<string>> SentOtp(string phone);
        Task<ApiResponse> VerifyOtp(string requestId, string otp);
    }
}
