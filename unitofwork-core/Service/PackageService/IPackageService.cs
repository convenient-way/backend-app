using unitofwork_core.Constant.Wallet;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.PackageModel;

namespace unitofwork_core.Service.PackageService
{
    public interface IPackageService
    {
        Task<ApiResponse<ResponsePackageModel>> Create(CreatePackageModel model);
        Task<ApiResponse<ResponsePackageModel>> GetById(Guid id);
        Task<ApiResponsePaginated<ResponsePackageModel>> GetFilter(Guid? shipperId, Guid? shopId, string? status, int? pageIndex, int? pageSize);
        Task<ApiResponse<List<ResponsePackageModel>>> GetAll(Guid shipperId, Guid shopId, string? status);
        Task<ApiResponse> ApprovedPackage(Guid id);
        Task<ApiResponse> RejectPackage(Guid id);
        Task<ApiResponse> ShipperPickupPackages(Guid shipperId, List<Guid> packageIds, string walletType = WalletType.DEFAULT);
        Task<ApiResponse> ShopCancelPackage(Guid packageId);
        Task<ApiResponse> ShipperCancelPackage(Guid packageId);
        Task<ApiResponseListError> ShipperConfirmPackages(List<Guid> packageIds, Guid shipperId, string walletType = WalletType.DEFAULT);
        Task<ApiResponse> ShipperDeliverySuccess(Guid packageId);
        Task<ApiResponse> DeliveryFailed(Guid packageId);
        Task<ApiResponse> ShopConfirmDeliverySuccess(Guid packageId);
        Task<ApiResponse> RefundSuccess(Guid packageId);
        Task<ApiResponse> RefundFailed(Guid packageId);
        Task<ApiResponsePaginated<ResponseComboPackageModel>> SuggestCombo(Guid shipperId, int pageIndex , int pageSize);
    }
}
