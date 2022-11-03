using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using unitofwork_core.Model.AdminModel;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Service.AdminService;

namespace unitofwork_core.Controllers
{

    public class AdminController : BaseApiController
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService shipperService, ILogger<AdminController> logger)
        {
            _adminService = shipperService;
            _logger = logger;
        }

        [HttpPost()]
        [SwaggerOperation(Summary = "Create admin")]
        public async Task<ActionResult<ApiResponse<ResponseAdminModel>>> Register(CreateAdminModel model)
        {
            try
            {
                ResponseAdminModel adminResponse = await _adminService.Create(model);
                return Ok(new ApiResponse<ResponseAdminModel>
                {
                    Message = "Tạo thành công admin",
                    Data = adminResponse
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Create admin has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
