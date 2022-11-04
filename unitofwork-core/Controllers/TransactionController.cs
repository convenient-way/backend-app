using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Globalization;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.TransactionModel;
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
        [SwaggerOperation(Summary = "Get list transaction (Format datetime : yyyy-MM-dd)")]
        public async Task<ActionResult<ApiResponsePaginated<ResponseTransactionModel>>> GetList(Guid shipperId, Guid shopId, string? from, string? to, int pageIndex = 0, int pageSize = 20)
        {
            try
            {
                DateTime? fromDate = null;
                DateTime? toDate = null;
                if (from != null && to != null) {
                    fromDate = DateTime.ParseExact(from, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    toDate = DateTime.ParseExact(to, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                ApiResponsePaginated<ResponseTransactionModel> response = await _transactionService.GetTransactions(shipperId, shopId, fromDate, toDate, pageIndex, pageSize);
                if (response.Success == false) {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (FormatException ex)
            {
                _logger.LogError("Create admin has exception : " + ex.Message);
                return BadRequest("Định dạng dữ liệu chưa đúng");
            } 
            catch (Exception ex)
            {
                _logger.LogError("Create admin has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
