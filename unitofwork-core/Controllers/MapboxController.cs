using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using unitofwork_core.Model.ApiResponse;
using unitofwork_core.Model.Mapbox;
using unitofwork_core.Service.MapboxService;

namespace unitofwork_core.Controllers
{
    public class MapboxController : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapboxService _mapboxService;
        private readonly ILogger<MapboxController> _logger; 

        public MapboxController(IConfiguration configuration, IMapboxService mapboxService, ILogger<MapboxController> logger )
        {
            _configuration = configuration;
            _mapboxService = mapboxService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> SearchApi(string search)
        {
            try
            {
                JObject directionApi = await _mapboxService.SearchApi(search);
                return Ok(new ApiResponse<JObject>
                {
                    Message = "Tìm kiếm",
                    Data = directionApi,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception api mapbox : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<JObject>>> DirectionApi(DirectionApiModel model)
        {
            try
            {
                JObject directionApi = await _mapboxService.DirectionApi(model);
                return Ok(new ApiResponse<JObject> {
                    Success = directionApi == null ? false : true,
                    Message = directionApi == null ? "Thông tin tọa độ bị sai" : "Lấy thông tin thành công từ Mapbox",
                    Data = directionApi,
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
