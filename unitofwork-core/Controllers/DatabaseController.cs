using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using unitofwork_core.Service.DatabaseService;

namespace unitofwork_core.Controllers
{
    public class DatabaseController : BaseApiController
    {
        private readonly IDatabaseService _databaseService;
        private readonly ILogger<DatabaseController> _logger;
        public DatabaseController(IDatabaseService databaseService, ILogger<DatabaseController> logger)
        {
            _databaseService = databaseService;

            _logger = logger;
        }
        [HttpGet("generate-data")]
        public IActionResult GenerateData()
        {
            try
            {
                _databaseService.GenerateData();
                _logger.LogInformation("Generate data success.");
                return Ok("Generate data success");
            }
            catch (Exception ex)
            {
                _logger.LogError("Remove data has failed : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("remove-data")]
        public IActionResult RemoveData()
        {
            try
            {
                _databaseService.RemoveData();
                return Ok("Remove data success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
