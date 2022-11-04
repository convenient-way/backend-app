using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using unitofwork_core.Constant.Wallet;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.CollectionModel;
using unitofwork_core.Model.PackageModel;
using unitofwork_core.Service.PackageService;

namespace unitofwork_core.Controllers
{
    public class PackageController : BaseApiController
    {
        private readonly ILogger<PackageController> _logger;
        private readonly IPackageService _packageService;
        public PackageController(ILogger<PackageController> logger, IPackageService packageService)
        {
            _logger = logger;
            _packageService = packageService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get list package")]
        public async Task<ActionResult<ApiResponsePaginated<ResponsePackageModel>>> Create(Guid shipperId, Guid shopId, string? status, int pageIndex = 0, int pageSize = 20)
        {
            try
            {
                ApiResponsePaginated<ResponsePackageModel> response = await _packageService.GetFilter(shipperId, shopId, status, pageIndex, pageSize);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get list package exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActionResult<ApiResponse<ResponsePackageModel>>>> GetId(Guid id)
        {
            try
            {
                ApiResponse<ResponsePackageModel> response = await _packageService.GetById(id);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get list package exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create pakage")]
        public async Task<ActionResult<ApiResponse<ResponsePackageModel>>> Create(CreatePackageModel model)
        {
            try
            {
                ApiResponse<ResponsePackageModel> response = await _packageService.Create(model);
                if (response.Success == false) {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Create package has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("combos")]
        [SwaggerOperation(Summary = "Get suggest combos")]
        public async Task<ActionResult<ApiResponsePaginated<ResponseComboPackageModel>>> SuggestCombo(Guid shipperId , int pageIndex = 0, int pageSize = 20)
        {
            try
            {
                ApiResponsePaginated<ResponseComboPackageModel> response = await _packageService.SuggestCombo(shipperId, pageIndex, pageSize);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Api mapbox has exception : " + ex.Message);
                return StatusCode(500, "Api mapbox has exception : " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get suggest combo has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("approve")]
        [SwaggerOperation(Summary = "Approved pakage from status waiting")]
        public async Task<ActionResult<ApiResponse>> ApprovedPackage(Guid packageId)
        {
            try
            {
                ApiResponse response = await _packageService.ApprovedPackage(packageId);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Approve package has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("reject")]
        [SwaggerOperation(Summary = "Reject pakage from status waiting")]
        public async Task<ActionResult<ApiResponse>> RejectPackage(Guid packageId)
        {
            try
            {
                ApiResponse response = await _packageService.RejectPackage(packageId);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Reject package has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("shipper-pickup")]
        [SwaggerOperation(Summary = "Shipper pickup a package")]
        public async Task<ActionResult<ApiResponse>> PickupPackage(ShipperPickUpModel model)
        {
            try
            {
                ApiResponse response = await _packageService.ShipperPickupPackage(model.shipperId,
                    model.packageId, model.walletType);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Shipper pickup package has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("shop-cancel")]
        [SwaggerOperation(Summary = "Shop cancel package")]
        public async Task<ActionResult<ApiResponse>> ShopCancelPackage(Guid packageId)
        {
            try
            {
                ApiResponse response = await _packageService.ShopCancelPackage(packageId);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Shop cancel package exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("shipper-confirm-packages")]
        [SwaggerOperation(Summary = "Shipper confirms packages and then delivery them")]
        public async Task<ActionResult<ApiResponseListError>> ShipperConfirmPackage(ShipperConfirmPackagesModel model)
        {
            try
            {
                ApiResponseListError response = await _packageService.ShipperConfirmPackages(
                    model.packageIds, model.shipperId, model.walletType);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Shipper confrims packages has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("delivery-failed")]
        [SwaggerOperation(Summary = "Shipper delivery failed package")]
        public async Task<ActionResult<ApiResponse>> ShipperDeliveryFailedPackage(Guid packageId)
        {
            try
            {
                ApiResponse response = await _packageService.DeliveryFailed(packageId);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Shipper delivery failed package has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("delivery-success")]
        [SwaggerOperation(Summary = "Shipper delivery success pakage")]
        public async Task<ActionResult<ApiResponse>> ShipperDeliverySuccessPackage(Guid packageId)
        {
            try
            {
                ApiResponse response = await _packageService.ShipperDeliverySuccess(packageId);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Shipper delivery success pakage has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("shop-confirm-delivery-success")]
        [SwaggerOperation(Summary = "Shop confirm delivery success")]
        public async Task<ActionResult<ApiResponse>> ShopConfirmDeliverySuccessPackage(Guid packageId)
        {
            try
            {
                ApiResponse response = await _packageService.ShopConfirmDeliverySuccess(packageId);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Shop confirm delivery success has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("refund-success")]
        [SwaggerOperation(Summary = "Shipper refund package for shop success")]
        public async Task<ActionResult<ApiResponse>> RefundSuccessPackage(Guid packageId)
        {
            try
            {
                ApiResponse response = await _packageService.RefundSuccess(packageId);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Shipper refund package for shop success has exception : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("refund-failed")]
        [SwaggerOperation(Summary = "Shipper refund package for shop failed")]
        public async Task<ActionResult<ApiResponse>> RefundFailedPackage(Guid packageId)
        {
            try
            {
                ApiResponse response = await _packageService.RefundFailed(packageId);
                if (response.Success == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Shipper refund package for shop failed : " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
