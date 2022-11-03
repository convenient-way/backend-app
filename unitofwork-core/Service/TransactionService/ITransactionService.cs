using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.CollectionModel;
using unitofwork_core.Model.TransactionModel;

namespace unitofwork_core.Service.TransactionService
{
    public interface ITransactionService
    {
        Task<ApiResponsePaginated<ResponseTransactionModel>> GetTransactionShipper(Guid shipperId, Guid shopId,
            DateOnly from, DateOnly to, int pageIndex, int pageSize);
    }
}
