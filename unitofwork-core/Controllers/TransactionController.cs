using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using unitofwork_core.Service.TransactionService;

namespace unitofwork_core.Controllers
{
    public class TransactionController : BaseApiController
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get list transaction")]
        public async Task<IActionResult> GetList(Guid shipperId, Guid shopId, int pageIndex = 0, int pageSize = 20)
        {
            try
            {

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Create admin has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
