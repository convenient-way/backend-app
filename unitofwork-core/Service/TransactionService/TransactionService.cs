using System.Linq.Expressions;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.CollectionModel;
using unitofwork_core.Model.TransactionModel;

namespace unitofwork_core.Service.TransactionService
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TransactionService> _logger;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IWalletRepository _walletRepo;
        private readonly IShipperRepository _shipperRepo;
        private readonly IShopRepository _shopRepo;

        public TransactionService(IUnitOfWork unitOfWork, ILogger<TransactionService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _transactionRepo = unitOfWork.Transactions;
            _shipperRepo = unitOfWork.Shippers;
            _shopRepo = unitOfWork.Shops;
            _walletRepo = unitOfWork.Wallets;
        }

        public async Task<ApiResponsePaginated<ResponseTransactionModel>> GetTransactions(Guid shipperId, Guid shopId, 
            DateTime? from, DateTime? to, int pageIndex, int pageSize)
        {
            ApiResponsePaginated<ResponseTransactionModel> response = new ApiResponsePaginated<ResponseTransactionModel>();
            #region Verify params
            if (pageIndex < 0 || pageSize < 1)
            {
                response.ToFailedResponse("Thông tin phân trang không hợp lệ");
                return await Task.FromResult(response);
            }
            #endregion
            #region Predicates
            List<Expression<Func<Transaction, bool>>> predicates = new List<Expression<Func<Transaction, bool>>>();
            if (!shipperId.Equals(Guid.Empty)) {
                List<Wallet> shipperWallets = (await _walletRepo.GetAllAsync(predicate: (w) => w.ShipperId == shipperId)).ToList();
                Expression<Func<Transaction, bool>> predicateShipper = (transaction) => transaction.WalletId == shipperWallets[0].Id ||
                                                        transaction.WalletId == shipperWallets[1].Id;
                predicates.Add(predicateShipper);
            }
            if (!shopId.Equals(Guid.Empty))
            {
                List<Wallet> shopWallets = (await _walletRepo.GetAllAsync(predicate: (w) => w.ShopId == shopId)).ToList();
                Expression<Func<Transaction, bool>> predicateShop = (transaction) => transaction.WalletId == shopWallets[0].Id ||
                                                        transaction.WalletId == shopWallets[1].Id;
                predicates.Add(predicateShop);
            }
            if (from != null && to != null) {
                Expression<Func<Transaction, bool>> predicateDateTime = (transaction) => transaction.CreatedAt.CompareTo(from) >= 0
                            && transaction.CreatedAt.CompareTo(to) <= 0;
                predicates.Add(predicateDateTime);
            }
            #endregion
            #region Order
            Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> orderBy = (source) => source.OrderByDescending(tr => tr.CreatedAt);
            #endregion
            #region Selector
            Expression<Func<Transaction, ResponseTransactionModel>> selector = (tran) => tran.ToResponseModel();
            #endregion
            PaginatedList<ResponseTransactionModel> result = await _transactionRepo.GetPagedListAsync<ResponseTransactionModel>(
                predicates:predicates, orderBy: orderBy, selector: selector);
            response.SetData(result);
            response.ToSuccessResponse("Lấy thông tin thành công");
            return response;
        }
    }
}
