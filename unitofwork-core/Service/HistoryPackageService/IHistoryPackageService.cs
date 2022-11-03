using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.HistoryPackageModel;

namespace unitofwork_core.Service.HistoryPackageService
{
    public interface IHistoryPackageService
    {
        Task<ApiResponsePaginated<ResponseHistoryPackageModel>> GetHistoryPackage(Guid packageId, int pageIndex, int pageSize);
    }
}
