using unitofwork_core.Model.Authorize;

namespace unitofwork_core.Service.AuthorizeService
{
    public interface IAuthorizeService
    {
        Task<ResponseLoginModel> Login(LoginModel model,bool isShop,bool isShipper,bool isAdmin); 
    }
}
