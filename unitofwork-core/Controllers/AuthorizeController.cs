using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.AuthorizeModel;
using unitofwork_core.Model.ShipperModel;
using unitofwork_core.Service.AuthorizeService;

namespace unitofwork_core.Controllers
{
    public class AuthorizeController : BaseApiController
    {
        private readonly IAuthorizeService _authorizeService;
        private readonly ILogger<AuthorizeService> _logger;

        public AuthorizeController(IAuthorizeService authorizeService, ILogger<AuthorizeService> logger)
        {
            _authorizeService = authorizeService;
            _logger = logger;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Login with username and password")]
        public async Task<ActionResult<ApiResponse<ResponseLoginModel>>> Login(LoginModel model, bool isShop = false, bool isShipper = false, bool isAdmin = false)
        {
            try
            {
                ApiResponse<ResponseLoginModel> loginResponse = await _authorizeService.Login(model, isShop, isShipper, isAdmin);
                if (loginResponse.Success == false) {
                    return BadRequest(loginResponse);
                }
                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("Login exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
