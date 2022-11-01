using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Annotations;
using unitofwork_core.Model.ApiResponse;
using unitofwork_core.Service.Goong;

namespace unitofwork_core.Controllers
{
    public class GoongController : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IGoongService _goongService;
        private readonly ILogger<MapboxController> _logger;

        public GoongController(IConfiguration configuration, IGoongService goongService, ILogger<MapboxController> logger)
        {
            _configuration = configuration;
            _goongService = goongService;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(summary:"Default search only location in HCM city")]
        public async Task<IActionResult> SearchApi(string search, double longitude = 106.64849987615482, double latitude = 10.807953964793464)
        {
            try
            {
                JObject searchObject = await _goongService.SearchApi(search, longitude, latitude);
                return Ok(new ApiResponse<JObject>
                {
                    Message = "Tìm kiếm",
                    Data = searchObject,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception api mapbox : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
