using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.CollectionModel;
using unitofwork_core.Model.HistoryPackageModel;

namespace unitofwork_core.Service.HistoryPackageService
{
    public class HistoryPackageService : IHistoryPackageService
    {
        private readonly ILogger<HistoryPackageService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHistoryPackageRepostiory _historyRepo;
        private readonly IPackageRepository _packageRepo;

        public HistoryPackageService(ILogger<HistoryPackageService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _historyRepo = unitOfWork.HistoryPackages;
            _packageRepo = unitOfWork.Packages;
        }

        public async Task<ApiResponsePaginated<ResponseHistoryPackageModel>> GetHistoryPackage(Guid packageId, int pageIndex, int pageSize)
        {
            ApiResponsePaginated<ResponseHistoryPackageModel> response = new ApiResponsePaginated<ResponseHistoryPackageModel>();

            #region Verify params
            Package? package = await _packageRepo.GetByIdAsync(packageId);
            if (package == null) {
                response.ToFailedResponse("Gói hàng không tồn tại");
            }
            if (pageIndex < 0 || pageSize < 1)
            {
                response.ToFailedResponse("Thông tin phân trang không hợp lệ");
                return response;
            }
            #endregion

            #region Predicate
            Expression<Func<HistoryPackage, bool>> predicate = (source) => source.PackageId == packageId;
            #endregion
            #region Order
            Func<IQueryable<HistoryPackage>, IOrderedQueryable<HistoryPackage>> orderBy = (source) => source.OrderByDescending(p => p.CreatedAt);
            #endregion
            #region Selector
            Expression<Func<HistoryPackage, ResponseHistoryPackageModel>> selector = (source) => source.ToResponseModel();
            #endregion
            PaginatedList<ResponseHistoryPackageModel> items = await _historyRepo.GetPagedListAsync(predicate: predicate, orderBy: orderBy,
                selector: selector, pageIndex: pageIndex, pageSize: pageSize);
            if (items.Count > 0)
            {
                response.SetData(items, "Thông tin lịch sử của gói hàng");
            }
            else {
                response.Message = "Không có thông tin lịch sử của gói hàng";
            }
            return response;
        }
    }
}
