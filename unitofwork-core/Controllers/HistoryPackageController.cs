using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.HistoryPackageModel;
using unitofwork_core.Service.HistoryPackageService;

namespace unitofwork_core.Controllers
{
    public class HistoryPackageController : BaseApiController
    {
        private readonly IHistoryPackageService _historyService;
        private readonly ILogger<HistoryPackageController> _logger;

        public HistoryPackageController(IHistoryPackageService historyService, ILogger<HistoryPackageController> logger)
        {
            _historyService = historyService;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get history package")]
        public async Task<ActionResult<ApiResponsePaginated<ResponseHistoryPackageModel>>> Create(
            Guid packageId, int pageIndex = 0, int pageSize = 20)
        {
            try
            {
                ApiResponsePaginated<ResponseHistoryPackageModel> response = 
                            await _historyService.GetHistoryPackage(packageId, pageIndex, pageSize);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get history exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
