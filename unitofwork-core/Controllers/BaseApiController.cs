using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using unitofwork_core.Model.ApiResponseModel;

namespace unitofwork_core.Controllers
{
    [Route("api/v1.0/[controller]s")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected IActionResult SendResponse(ApiResponse response)
        {
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
