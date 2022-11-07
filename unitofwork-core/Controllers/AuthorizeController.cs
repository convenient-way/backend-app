using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.AuthorizeModel;
using unitofwork_core.Model.ShipperModel;
using unitofwork_core.Service.AuthorizeService;
using Vonage;
using Vonage.Request;
using Vonage.Verify;

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

        [HttpPost("sent-otp")]
        [SwaggerOperation(Summary = "Sent otp phone number")]
        public async Task<ActionResult<ApiResponse<string>>> SentOtp(string phone)
        {
            try
            {
                ApiResponse<string> response = await _authorizeService.SentOtp(phone);
                if (response.Success == false) {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Login exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("verify-otp")]
        [SwaggerOperation(Summary = "Verify otp is valid")]
        public async Task<ActionResult<ApiResponse>> VerifyOtp(string requestId, string otpCode)
        {
            try
            {
                ApiResponse response = await _authorizeService.VerifyOtp(requestId, otpCode);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Verify otp exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
