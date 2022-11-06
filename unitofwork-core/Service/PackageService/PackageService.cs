using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using unitofwork_core.Constant.ConfigConstant;
using unitofwork_core.Constant.DateTimeFormat;
using unitofwork_core.Constant.Package;
using unitofwork_core.Constant.Transaction;
using unitofwork_core.Constant.Wallet;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;
using unitofwork_core.Helper;
using unitofwork_core.Model.ApiResponseModel;
using unitofwork_core.Model.CollectionModel;
using unitofwork_core.Model.MapboxModel;
using unitofwork_core.Model.PackageModel;
using unitofwork_core.Model.ProductModel;
using unitofwork_core.Service.MapboxService;
using unitofwork_core.Service.PackageService;


namespace unitofwork_core.Service.PackageService
{
    public class PackageService : IPackageService
    {
        private readonly ILogger<PackageService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPackageRepository _packageRepo;
        private readonly IShipperRepository _shipperRepo;
        private readonly IShopRepository _shopRepo;
        private readonly IWalletRepository _walletRepo;
        private readonly IHistoryPackageRepostiory _historyPackageRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IConfigRepostiory _configRepo;
        private readonly IMapboxService _mapboxService;
        public PackageService(ILogger<PackageService> logger, IUnitOfWork unitOfWork, IMapboxService mapboxService)
        {
            _logger = logger;

            _mapboxService = mapboxService;

            _unitOfWork = unitOfWork;
            _packageRepo = unitOfWork.Packages;
            _shipperRepo = unitOfWork.Shippers;
            _shopRepo = unitOfWork.Shops;
            _historyPackageRepo = unitOfWork.HistoryPackages;
            _walletRepo = unitOfWork.Wallets;
            _transactionRepo = unitOfWork.Transactions;
            _configRepo = unitOfWork.ConfigApps;
        }

        public async Task<ApiResponse<ResponsePackageModel>> Create(CreatePackageModel model)
        {
            ApiResponse<ResponsePackageModel> response = new ApiResponse<ResponsePackageModel>();
            #region Verify params
            Shop? shop = await _shopRepo.GetByIdAsync(model.ShopId);
            if (shop == null)
            {
                response.ToFailedResponse("Cửa hàng không tồn tại");
                return response;
            }
            #endregion

            Package package = model.ConverToEntity();
            await _packageRepo.InsertAsync(package);

            #region Create history
            HistoryPackage history = new HistoryPackage();
            history.FromStatus = PackageStatus.NOT_EXIST;
            history.ToStatus = PackageStatus.WAITING;
            history.Description = "Đơn hàng được tạo vào lúc: " + DateTime.UtcNow.ToString(DateTimeFormatConstant.DEFAULT);
            history.PackageId = package.Id;
            await _historyPackageRepo.InsertAsync(history);
            #endregion

            int result = await _unitOfWork.CompleteAsync();

            #region Response result
            response.Success = result > 0 ? true : false;
            response.Message = result > 0 ? "Tạo đơn hàng thành công" : "Tạo đơn thất bại";
            response.Data = result > 0 ? package.ToResponseModel() : null;
            #endregion

            return response;
        }

        public async Task<ApiResponse<ResponsePackageModel>> GetById(Guid id)
        {
            #region Includable
            Func<IQueryable<Package>, IIncludableQueryable<Package, object>> include = (p) => p.Include(p => p.Products);
            #endregion

            Package? package = await _packageRepo.GetByIdAsync(id, include: include);

            #region Response result
            ApiResponse<ResponsePackageModel> response = new ApiResponse<ResponsePackageModel>();
            if (package != null)
            {
                response.Message
                    = "Lấy thông tin đơn hàng thành công";
                response.Data = package.ToResponseModel();
            }
            else
            {
                response.Success = false;
                response.Message = "Id đơn hàng không tồn tại";
            }
            #endregion

            return response;
        }

        public async Task<ApiResponsePaginated<ResponsePackageModel>> GetFilter(Guid shipperId, Guid shopId, string? status, int pageIndex, int pageSize)
        {
            ApiResponsePaginated<ResponsePackageModel> response = new ApiResponsePaginated<ResponsePackageModel>();
            #region Verify params
            if (pageIndex < 0 || pageSize < 1)
            {
                response.ToFailedResponse("Thông tin phân trang không hợp lệ");
                return response;
            }
            #endregion

            #region Includable
            Func<IQueryable<Package>, IIncludableQueryable<Package, object>> include = (source) => source.Include(p => p.Products);
            #endregion

            #region Predicates
            List<Expression<Func<Package, bool>>> predicates = new List<Expression<Func<Package, bool>>>();
            if (shipperId != Guid.Empty)
            {
                Expression<Func<Package, bool>> filterShipper = (p) => p.ShipperId == shipperId;
                predicates.Add(filterShipper);
            }
            if (shopId != Guid.Empty)
            {
                Expression<Func<Package, bool>> filterShop = (p) => p.ShopId == shopId;
                predicates.Add(filterShop);
            }
            if (status != null)
            {
                Expression<Func<Package, bool>> filterStatus = (p) => p.Status == status.ToUpper();
                predicates.Add(filterStatus);
            }
            #endregion

            #region Order
            Func<IQueryable<Package>, IOrderedQueryable<Package>> orderBy = (source) => source.OrderByDescending(p => p.ModifiedAt);
            #endregion

            Expression<Func<Package, ResponsePackageModel>> selector = (package) => package.ToResponseModel();
            PaginatedList<ResponsePackageModel> items = await _packageRepo.GetPagedListAsync(
                selector: selector, include: include, predicates: predicates,
                orderBy: orderBy, pageIndex: pageIndex, pageSize: pageSize);
            _logger.LogInformation("Total count: " + items.TotalCount);
            #region Response result
            response.SetData(items);
            int countPackage = items.Count;
            if (countPackage > 0)
            {
                response.Message = "Không tìm không đơn hàng";
            }
            else
            {
                response.Message = "Lấy thông tin đơn hàng thành công";
            }
            #endregion
            return response;
        }

        public async Task<ApiResponsePaginated<ResponseComboPackageModel>> SuggestCombo(Guid shipperId, int pageIndex, int pageSize)
        {
            ApiResponsePaginated<ResponseComboPackageModel> response = new ApiResponsePaginated<ResponseComboPackageModel>();
            #region Verify params
            Shipper? _shipper = await _shipperRepo.GetByIdAsync(shipperId);
            if (_shipper == null)
            {
                response.ToFailedResponse("Shipperkhông tồn tại");
                return response;
            }
            if (pageIndex < 0 || pageSize < 1)
            {
                response.ToFailedResponse("Thông tin phân trang không hợp lệ");
                return response;
            }
            #endregion

            GeoCoordinate homeCoordinate = new GeoCoordinate(_shipper.HomeLatitude, _shipper.HomeLongitude);
            GeoCoordinate destinationCoordinate = new GeoCoordinate(_shipper.DestinationLatitude, _shipper.DestinationLongitude);

            PolyLineModel polyLineShipper = await _mapboxService.GetPolyLineModel(homeCoordinate, destinationCoordinate);

            #region Includale package
            Func<IQueryable<Package>, IIncludableQueryable<Package, object>> include = (source) => source.Include(p => p.Products);
            #endregion
            #region Predicate package
            Expression<Func<Package, bool>> predicate = (source) => source.Status == PackageStatus.APPROVED;
            #endregion

            #region Find packages valid spacing
            List<ResponsePackageModel> packagesValid = new List<ResponsePackageModel>();
            List<Package> packages = (await _packageRepo.GetAllAsync(include: include, predicate: predicate)).ToList();
            int packageCount = packages.Count;
            for (int i = 0; i < packageCount; i++)
            {
                bool isValidOrder = MapHelper.ValidDestinationBetweenShipperAndPackage(polyLineShipper, packages[i]);
                _logger.LogInformation($"Package valid destination: {packages[i].Id}");
                if (isValidOrder) packagesValid.Add(packages[i].ToResponseModel());
            }
            #endregion

            #region Group with shop
            List<Guid> shopIds = new List<Guid>();
            foreach (ResponsePackageModel p in packagesValid)
            {
                if (!shopIds.Contains(p.ShopId))
                {
                    shopIds.Add(p.ShopId);
                    _logger.LogInformation($"Shop have combos suggest: {p.ShopId}");
                }
            }
            List<ResponseComboPackageModel> combos = new List<ResponseComboPackageModel>();
            foreach (Guid shopId in shopIds)
            {
                ResponseComboPackageModel combo = new ResponseComboPackageModel();
                combo.Shop = (await _shopRepo.GetByIdAsync(shopId))?.ToResponseModel();
                combo.Packages = packagesValid.Where(p => p.ShopId == shopId).ToList();

                decimal comboPrice = 0;
                foreach (ResponsePackageModel pac in combo.Packages)
                {
                    foreach (ResponseProductModel pr in pac.Products)
                    {
                        comboPrice += pr.Price;
                    }
                }
                combo.ComboPrice = comboPrice;
                _logger.LogInformation($"Combo[Shop: {combo.Shop?.Id},Price: {combo.ComboPrice},Package: {combo.Packages.Count}]");
                combos.Add(combo);
            }
            PaginatedList<ResponseComboPackageModel> responseList = await combos.ToPaginatedListAsync(pageIndex, pageSize);
            response.SetData(responseList);
            response.ToSuccessResponse("Lấy những đề xuất combo");
            #endregion

            return response;
        }

        public async Task<ApiResponse> ApprovedPackage(Guid id)
        {
            ApiResponse response = new ApiResponse();
            Package? package = await _packageRepo.GetByIdAsync(id, disableTracking: false);

            #region Verify params
            if (package == null)
            {
                response.ToFailedResponse("Gói hàng không tồn tại");
                return response;
            }
            if (package.Status != PackageStatus.WAITING)
            {
                response.ToFailedResponse("Gói hàng không tồn tại không ở trạng thái chờ để duyệt");
                return response;
            }
            #endregion

            #region Create history
            HistoryPackage history = new HistoryPackage();
            history.FromStatus = package.Status;
            history.ToStatus = PackageStatus.APPROVED;
            history.Description = "Đơn hàng được duyệt vào lúc: " + DateTime.UtcNow.ToString(DateTimeFormatConstant.DEFAULT);
            history.PackageId = package.Id;
            await _historyPackageRepo.InsertAsync(history);
            #endregion
            package.Status = PackageStatus.APPROVED;

            int result = await _unitOfWork.CompleteAsync();

            #region Response result
            response.Success = result > 0 ? true : false;
            response.Message = result > 0 ? "Duyệt đơn thành công" : "Duyệt đơn thất bại";
            #endregion

            return response;
        }

        public async Task<ApiResponse> RejectPackage(Guid id)
        {
            ApiResponse response = new ApiResponse();
            Package? package = await _packageRepo.GetByIdAsync(id, disableTracking: false);

            #region Verify params
            if (package == null)
            {
                response.ToFailedResponse("Gói hàng không tồn tại");
                return response;
            }
            if (package.Status != PackageStatus.WAITING)
            {
                response.ToFailedResponse("Gói hàng không tồn tại không ở trạng thái chờ để hủy");
                return response;
            }
            #endregion

            #region Create history
            HistoryPackage history = new HistoryPackage();
            history.FromStatus = package.Status;
            history.ToStatus = PackageStatus.REJECT;
            history.Description = "Đơn hàng bị từ chối vào lúc: " + DateTime.UtcNow.ToString(DateTimeFormatConstant.DEFAULT);
            history.PackageId = package.Id;
            await _historyPackageRepo.InsertAsync(history);
            #endregion
            package.Status = PackageStatus.REJECT;

            int result = await _unitOfWork.CompleteAsync();

            #region Response result
            response.Success = result > 0 ? true : false;
            response.Message = result > 0 ? "Từ chối gói hàng thành công" : "Từ chối gói hàng thất bại thất bại";
            #endregion

            return response;
        }

        public async Task<ApiResponse> ShipperPickupPackages(Guid shipperId, List<Guid> packageIds, string walletType = WalletType.DEFAULT)
        {
            ApiResponse response = new ApiResponse();

            #region Includable shipper, pakage
            Func<IQueryable<Shipper>, IIncludableQueryable<Shipper, object>> includeShipper = (source) => source.Include(sh => sh.Wallets);
            Func<IQueryable<Package>, IIncludableQueryable<Package, object>> includePackage = (source) => source.Include(sh => sh.Products);
            #endregion
            #region Predicate
            Expression<Func<Wallet, bool>> predicateSystemWallet = (wallet) => wallet.WalletType == WalletType.SYSTEM;
            #endregion

            Shipper? shipper = await _shipperRepo.GetByIdAsync(shipperId, include: includeShipper, disableTracking: false);
            Wallet? shipperWallet = shipper?.Wallets.SingleOrDefault(w => w.WalletType == walletType);

            List<Package> packages = new List<Package>();
            for (int i = 0; i < packageIds.Count; i++)
            {
                Package? package = await _packageRepo.GetByIdAsync(packageIds[i], disableTracking: false, include: includePackage);
                if (package == null) {
                    response.ToFailedResponse("Có gói hàng không tồn tại");
                    return response;
                }
                if (package.Status != PackageStatus.APPROVED) {
                    response.ToFailedResponse("Có gói hàng không tồn tại không ở trạng thái chờ để duyệt");
                    return response;
                }
                packages.Add(package);
            }

            #region Verify params
            decimal totalPrice = 0;
            for (int i = 0; i < packages.Count; i++)
            {
                Package package = packages[i];
                package.Products.ToList().ForEach(pr =>
                {
                    totalPrice += pr.Price;
                });
            }
            if (shipperWallet == null || shipperWallet.Balance < totalPrice)
            {
                response.ToFailedResponse("Số dư ví không đủ để thực hiện nhận gói hàng");
                return response;
            }
            #endregion
            #region Create history
            for (int i = 0; i < packages.Count; i++)
            {
                Package package = packages[i];
                package.ShipperId = shipperId;
                HistoryPackage history = new HistoryPackage();
                history.FromStatus = package.Status;
                history.ToStatus = PackageStatus.SHIPPER_PICKUP;
                history.Description = "Đơn hàng được nhận vào lúc: " + DateTime.UtcNow.ToString(DateTimeFormatConstant.DEFAULT);
                history.PackageId = package.Id;
                await _historyPackageRepo.InsertAsync(history);
                package.Status = PackageStatus.SHIPPER_PICKUP;
            }
            #endregion
            int result = await _unitOfWork.CompleteAsync();

            #region Response result
            response.Success = result > 0 ? true : false;
            response.Message = result > 0 ? "Chọn đơn thành công" : "Chọn đơn thất bại";
            #endregion

            return response;
        }

        public async Task<ApiResponse> ShopCancelPackage(Guid packageId)
        {
            ApiResponse response = new ApiResponse();
            Package? package = await _packageRepo.GetByIdAsync(packageId, disableTracking: false);

            #region Verify params
            if (package == null)
            {
                response.ToFailedResponse("Gói hàng không tồn tại");
                return response;
            }
            if (package.Status != PackageStatus.APPROVED && package.Status != PackageStatus.WAITING && package.Status != PackageStatus.SHIPPER_PICKUP)
            {
                response.ToFailedResponse("Gói hàng đang ở trạng thái không thể hủy");
                return response;
            }
            #endregion
            #region Create history
            HistoryPackage history = new HistoryPackage();
            history.FromStatus = package.Status;
            history.ToStatus = PackageStatus.SHOP_CANCEL;
            history.Description = "Đơn hàng đã bị shop hủy vào lúc: " + DateTime.UtcNow.ToString(DateTimeFormatConstant.DEFAULT);
            history.PackageId = package.Id;
            await _historyPackageRepo.InsertAsync(history);
            #endregion
            package.Status = PackageStatus.SHOP_CANCEL;

            int result = await _unitOfWork.CompleteAsync();

            #region Response result
            response.Success = result > 0 ? true : false;
            response.Message = result > 0 ? "Hủy đơn thành công" : "Hủy đơn = thất bại";
            #endregion

            return response;
        }

        public async Task<ApiResponse> ShipperCancelPackage(Guid packageId)
        {
            ApiResponse response = new ApiResponse();
            Package? package = await _packageRepo.GetByIdAsync(packageId, disableTracking: false);

            #region Verify params
            if (package == null)
            {
                response.ToFailedResponse("Gói hàng không tồn tại");
                return response;
            }
            if (package.Status != PackageStatus.APPROVED && package.Status != PackageStatus.WAITING && package.Status != PackageStatus.SHIPPER_PICKUP)
            {
                response.ToFailedResponse("Gói hàng đang ở trạng thái không thể hủy");
                return response;
            }
            #endregion
            #region Create history
            HistoryPackage history = new HistoryPackage();
            history.FromStatus = package.Status;
            history.ToStatus = PackageStatus.SHIPPER_CANCEL;
            history.Description = "Đơn hàng đã bị shop hủy vào lúc: " + DateTime.UtcNow.ToString(DateTimeFormatConstant.DEFAULT);
            history.PackageId = package.Id;
            await _historyPackageRepo.InsertAsync(history);
            #endregion
            package.Status = PackageStatus.SHIPPER_CANCEL;

            int result = await _unitOfWork.CompleteAsync();

            #region Response result
            response.Success = result > 0 ? true : false;
            response.Message = result > 0 ? "Hủy đơn thành công" : "Hủy đơn thất bại";
            #endregion

            return response;
        }

        public async Task<ApiResponseListError> ShipperConfirmPackages(List<Guid> packageIds, Guid shipperId, string walletType = WalletType.DEFAULT)
        {
            ApiResponseListError response = new ApiResponseListError();

            #region Includable shipper, pakage
            Func<IQueryable<Shipper>, IIncludableQueryable<Shipper, object>> includeShipper = (source) => source.Include(sh => sh.Wallets);
            Func<IQueryable<Package>, IIncludableQueryable<Package, object>> includePackage = (source) => source.Include(pk => pk.Products);
            #endregion
            #region Predicate
            Expression<Func<Wallet, bool>> predicateSystemWallet = (wallet) => wallet.WalletType == WalletType.SYSTEM;
            #endregion

            Shipper? shipper = await _shipperRepo.GetByIdAsync(shipperId, include: includeShipper, disableTracking: false);
            Wallet? shipperWallet = shipper?.Wallets.SingleOrDefault(w => w.WalletType == walletType);
            Wallet? systemWallet = await _walletRepo.GetSingleOrDefaultAsync(predicate: predicateSystemWallet, disableTracking: false);

            #region Verify params
            List<Package> packages = new List<Package>();
            List<string> errors = new List<string>();

            if (shipper == null || shipperWallet == null || systemWallet == null)
            {
                errors.Add("Shipper không tồn tại, không tìm thấy ví hệ thống");
            }
            #region Checking packages valid
            foreach (Guid id in packageIds)
            {
                Package? package = await _packageRepo.GetByIdAsync(id,include: includePackage, disableTracking: false);
                if (package == null)
                {
                    // response.ToFailedResponse($"Có gói hàng không tồn tại id: {id}");
                    string error = $"Có gói hàng không tồn tại id: {id}";
                    errors.Add(error);
                }
                else
                {
                    if (package.Status != PackageStatus.SHIPPER_PICKUP)
                    {
                        string error = $"Có gói hàng không ở trạng thái đã chọn id: {id}-{package.Status}";
                        errors.Add(error);
                    }
                    else
                    {
                        packages.Add(package);
                    }
                }
            }
            #endregion
            #region Checking balance
            decimal totalPriceCombo = 0;
            packages.ForEach(p =>
            {
                p.Products.ToList().ForEach(pr =>
                {
                    totalPriceCombo += pr.Price;
                });
            });
            if (shipperWallet == null || shipperWallet.Balance < totalPriceCombo)
            {
                errors.Add("Số dư ví không đủ để thực hiện nhận gói hàng");
            }

            #endregion

            if (errors.Count > 0)
            {
                response.ToFailedResponse(errors);
                return response;
            }
            #endregion

            #region Create transations, history and update wallet shipper and system
            int pakageCount = packages.Count();
            for (int i = 0; i < pakageCount; i++)
            {
                Package package = packages[i];
                decimal packagePrice = 0;
                package.Products.ToList().ForEach(pr =>
                {
                    totalPriceCombo += pr.Price;
                    packagePrice += pr.Price;
                });
                _logger.LogInformation("Total price package: " + packagePrice);
                #region Create transactions
                Transaction systemTrans = new Transaction();
                systemTrans.Description = $"Shipper({shipper!.Id})" + "đã nghận gói hàng với id: " + package.Id;
                systemTrans.Status = TransactionStatus.ACCOMPLISHED;
                systemTrans.TransactionType = TransactionType.PICKUP;
                systemTrans.CoinExchange = totalPriceCombo;
                systemTrans.BalanceWallet = systemWallet!.Balance + packagePrice;
                systemTrans.PackageId = package.Id;
                systemTrans.WalletId = systemWallet.Id;

                Transaction shipperTrans = new Transaction();
                shipperTrans.Description = "Đã nhận đơn hàng id : " + package.Id;
                shipperTrans.Status = TransactionStatus.ACCOMPLISHED;
                shipperTrans.TransactionType = TransactionType.PICKUP;
                shipperTrans.CoinExchange = - packagePrice;
                shipperTrans.BalanceWallet = shipperWallet!.Balance - packagePrice;
                shipperTrans.PackageId = package.Id;
                shipperTrans.WalletId = shipperWallet.Id;

                systemWallet.Balance = systemWallet.Balance + packagePrice;
                shipperWallet.Balance = shipperWallet.Balance - packagePrice;

                package.ShipperId = shipper.Id;

                List<Transaction> transactions = new List<Transaction> {
                    systemTrans, shipperTrans
                };
                await _transactionRepo.InsertAsync(transactions);
                #endregion

                #region Create history
                HistoryPackage history = new HistoryPackage();
                history.FromStatus = package.Status;
                history.ToStatus = PackageStatus.DELIVERY;
                history.Description = $"Shipper({package.ShipperId}) đang giao đơn hàng vào lúc: " + DateTime.UtcNow.ToString(DateTimeFormatConstant.DEFAULT);
                history.PackageId = package.Id;
                package.Status = PackageStatus.DELIVERY;
                await _historyPackageRepo.InsertAsync(history);
                #endregion
            };
            #endregion
            int result = await _unitOfWork.CompleteAsync();
            #region Response result
            response.Success = result > 0 ? true : false;
            response.Note = result > 0 ? "Nhận hàng để giao thành công" : "Nhận hàng để giao thất bại";
            #endregion

            return response;
        }

        public async Task<ApiResponse> ShipperDeliverySuccess(Guid packageId)
        {
            ApiResponse response = new ApiResponse();
            Package? package = await _packageRepo.GetByIdAsync(packageId, disableTracking: false);

            #region Verify params
            if (package == null)
            {
                response.ToFailedResponse("Gói hàng không tồn tại");
                return response;
            }
            if (package.Status != PackageStatus.DELIVERY)
            {
                response.ToFailedResponse("Gói hàng không ở trạng thái đang giao để chuyển sang giao thành công");
                return response;
            }
            #endregion

            #region Create history
            HistoryPackage history = new HistoryPackage();
            history.FromStatus = package.Status;
            history.ToStatus = PackageStatus.DELIVERED;
            history.Description = $"Shipper({package.ShipperId}) giao hàng thành công vào lúc: " + DateTime.UtcNow.ToString(DateTimeFormatConstant.DEFAULT);
            history.PackageId = package.Id;
            await _historyPackageRepo.InsertAsync(history);
            package.Status = PackageStatus.DELIVERED;
            #endregion

            int result = await _unitOfWork.CompleteAsync();

            #region Response result
            response.Success = result > 0 ? true : false;
            response.Message = result > 0 ? "Yêu cầu thành công" : "Yêu cầu thất bại";
            #endregion

            return response;
        }

        public async Task<ApiResponse> DeliveryFailed(Guid packageId)
        {
            ApiResponse response = new ApiResponse();
            Package? package = await _packageRepo.GetByIdAsync(packageId, disableTracking: false);

            #region Verify params
            if (package == null)
            {
                response.ToFailedResponse("Gói hàng không tồn tại");
                return response;
            }
            if (package.Status != PackageStatus.DELIVERY)
            {
                response.ToFailedResponse("Gói hàng không ở trạng thái đang giao để chuyển sang giao thất bại");
                return response;
            }
            #endregion

            #region Create history
            HistoryPackage history = new HistoryPackage();
            history.FromStatus = package.Status;
            history.ToStatus = PackageStatus.DELIVERY_FAILED;
            history.Description = "Giao thất bại vào lúc: " + DateTime.UtcNow.ToString(DateTimeFormatConstant.DEFAULT);
            history.PackageId = package.Id;
            await _historyPackageRepo.InsertAsync(history);
            package.Status = PackageStatus.DELIVERY_FAILED;
            #endregion

            int result = await _unitOfWork.CompleteAsync();

            #region Response result
            response.Success = result > 0 ? true : false;
            response.Message = result > 0 ? "Yêu cầu thành công" : "Yêu cầu thất bại";
            #endregion

            return response;
        }

        public async Task<ApiResponse> ShopConfirmDeliverySuccess(Guid packageId)
        {
            ApiResponse response = new ApiResponse();
            decimal profitPercent = decimal.Parse(_configRepo.GetValueConfig(ConfigConstant.PROFIT_PERCENTAGE)) / 100;

            
            #region Includable pakage
            Func<IQueryable<Package>, IIncludableQueryable<Package, object?>> includePackage = (source) => source.Include(p => p.Shop)
                        .ThenInclude(shop => shop != null ? shop.Wallets : null)
                        .Include(p => p.Shipper).ThenInclude(ship => ship != null ? ship.Wallets : null)
                        .Include(p => p.Products);
            #endregion
            Package? package = await _packageRepo.GetByIdAsync(packageId, disableTracking: false, include: includePackage);

            #region Predicate wallet
            Expression<Func<Wallet, bool>> predicateSystemWallet = (wallet) => wallet.WalletType == WalletType.SYSTEM;
            Expression<Func<Wallet, bool>> predicateWalletShipper = (wallet) => wallet.WalletType == WalletType.DEFAULT
                            && package != null ? (wallet.ShipperId == package.ShipperId) : false;
            Expression<Func<Wallet, bool>> predicateWalletShop = (wallet) => wallet.WalletType == WalletType.DEFAULT
                            && package != null ? (wallet.ShopId == package.ShopId) : false;
            #endregion

            Shipper? shipper = package?.Shipper;
            Wallet? shipperWallet = await _walletRepo.GetSingleOrDefaultAsync(predicate: predicateWalletShipper, disableTracking: false);
            Shop? shop = package?.Shop;
            Wallet? shopWallet = await _walletRepo.GetSingleOrDefaultAsync(predicate: predicateWalletShop, disableTracking: false);
            Wallet? systemWallet = await _walletRepo.GetSingleOrDefaultAsync(predicate: predicateSystemWallet, disableTracking: false);
            #region Verify params
            if (package == null)
            {
                response.ToFailedResponse("Gói hàng không tồn tại");
                return response;
            }
            if (shipper == null)
            {
                response.ToFailedResponse("Đơn hàng chưa được shipper pickup");
                return response;
            }
            if (package.Status != PackageStatus.DELIVERED)
            {
                response.ToFailedResponse("Gói hàng chưa được giao để có thể hoàn thành");
                return response;
            }
            if (shipperWallet == null || shopWallet == null || systemWallet == null)
            {
                response.ToFailedResponse("Không tìm thấy đủ các ví để tạo giao dịch");
                return response;
            }
            #endregion
            decimal totalPrice = 0;
            package.Products.ToList().ForEach(pr =>
            {
                totalPrice += pr.Price;
            });
            _logger.LogInformation($"Profit percent: {profitPercent} ,Total price : {totalPrice}");

            #region Create transactions
            Transaction systemTrans = new Transaction();
            systemTrans.Description = shipper.Email + "giao thành công gói hàng với id: " + package.Id;
            systemTrans.Status = TransactionStatus.ACCOMPLISHED;
            systemTrans.TransactionType = TransactionType.DELIVERED_SUCCESS;
            systemTrans.CoinExchange = package.PriceShip * profitPercent - totalPrice;
            systemTrans.BalanceWallet = systemWallet.Balance - totalPrice + package.PriceShip * profitPercent;
            systemTrans.PackageId = package.Id;
            systemTrans.WalletId = systemWallet.Id;
            _logger.LogInformation($"System transaction: {systemTrans.CoinExchange}, Balance: {systemTrans.BalanceWallet}");

            Transaction shipperTrans = new Transaction();
            shipperTrans.Description = "Giao thành công đơn hàng id : " + package.Id;
            shipperTrans.Status = TransactionStatus.ACCOMPLISHED;
            shipperTrans.TransactionType = TransactionType.DELIVERED_SUCCESS;
            shipperTrans.CoinExchange = totalPrice + package.PriceShip * (1 - profitPercent);
            shipperTrans.BalanceWallet = shipperWallet.Balance + totalPrice + package.PriceShip * (1 - profitPercent);
            shipperTrans.PackageId = package.Id;
            shipperTrans.WalletId = shipperWallet.Id;
            _logger.LogInformation($"Shipper transaction: {shipperTrans.CoinExchange}, Balance: {shipperTrans.BalanceWallet}");

            Transaction shopTrans = new Transaction();
            shopTrans.Description = "Giao thành công đơn hàng id : " + package.Id;
            shopTrans.Status = TransactionStatus.ACCOMPLISHED;
            shopTrans.TransactionType = TransactionType.DELIVERED_SUCCESS;
            shopTrans.CoinExchange = -totalPrice - package.PriceShip;
            shopTrans.BalanceWallet = shopWallet.Balance - totalPrice - package.PriceShip;
            shopTrans.PackageId = package.Id;
            shopTrans.WalletId = shopWallet.Id;
            _logger.LogInformation($"Shop transaction: {shopTrans.CoinExchange}, Balance: {shopTrans.BalanceWallet}");

            systemWallet.Balance = systemWallet.Balance - totalPrice + package.PriceShip * profitPercent;
            shipperWallet.Balance = shipperWallet.Balance + totalPrice + package.PriceShip * (1 - profitPercent);
            shopWallet.Balance = shopWallet.Balance - totalPrice - package.PriceShip;

            List<Transaction> transactions = new List<Transaction> {
                    systemTrans, shipperTrans, shopTrans
                };
            await _transactionRepo.InsertAsync(transactions);
            #endregion

            #region Create history
            HistoryPackage history = new HistoryPackage();
            history.FromStatus = package.Status;
            history.ToStatus = PackageStatus.SHOP_CONFIRM_DEIVERED;
            history.Description = $"Shop({package.ShipperId}) xác nhận shipper đã giao hàng thành công vào lúc: " + DateTime.UtcNow.ToString(DateTimeFormatConstant.DEFAULT);
            history.PackageId = package.Id;

            package.Status = PackageStatus.SHOP_CONFIRM_DEIVERED;
            await _historyPackageRepo.InsertAsync(history);
            #endregion
            int result = await _unitOfWork.CompleteAsync();
            if (result > 0) response.ToSuccessResponse("Yêu cầu thành công");
            return response;
        }

        public async Task<ApiResponse> RefundSuccess(Guid packageId)
        {
            ApiResponse response = new ApiResponse();
            decimal profitPercent = decimal.Parse(_configRepo.GetValueConfig(ConfigConstant.PROFIT_PERCENTAGE));
            decimal profitPercentRefund = decimal.Parse(_configRepo.GetValueConfig(ConfigConstant.PROFIT_PERCENTAGE_REFUND));

            #region Predicate
            Expression<Func<Wallet, bool>> predicateSystemWallet = (wallet) => wallet.WalletType == WalletType.SYSTEM;
            #endregion
            #region Includable pakage
            Func<IQueryable<Package>, IIncludableQueryable<Package, object?>> includePackage = (source) => source.Include(p => p.Shop)
                        .ThenInclude(shop => shop != null ? shop.Wallets : null)
                        .Include(p => p.Shipper).ThenInclude(ship => ship != null ? ship.Wallets : null);
            #endregion
            Package? package = await _packageRepo.GetByIdAsync(packageId, disableTracking: false, include: includePackage);
            Shipper? shipper = package?.Shipper;
            Wallet? shipperWallet = shipper?.Wallets.SingleOrDefault(w => w.WalletType == WalletType.DEFAULT);
            Shop? shop = package?.Shop;
            Wallet? shopWallet = shipper?.Wallets.SingleOrDefault(w => w.WalletType == WalletType.DEFAULT);
            Wallet? systemWallet = await _walletRepo.GetSingleOrDefaultAsync(predicate: predicateSystemWallet, disableTracking: false);
            #region Verify params
            if (package == null)
            {
                response.ToFailedResponse("Gói hàng không tồn tại");
                return response;
            }
            if (shipper == null)
            {
                response.ToFailedResponse("Đơn hàng chưa được shipper pickup");
                return response;
            }
            if (package.Status != PackageStatus.DELIVERY_FAILED)
            {
                response.ToFailedResponse("Hàng không giao thất bại thì hoàn trả cái gì!!");
                return response;
            }
            if (shipperWallet == null || shopWallet == null || systemWallet == null)
            {
                response.ToFailedResponse("Không tìm thấy đủ các ví để tạo giao dịch");
                return response;
            }
            #endregion
            decimal totalPrice = 0;
            package.Products.ToList().ForEach(pr =>
            {
                totalPrice += pr.Price;
            });

            #region Create transactions
            Transaction systemTrans = new Transaction();
            systemTrans.Description = shipper.Email + "hoàn trả thành công gói hàng với id: " + package.Id;
            systemTrans.Status = TransactionStatus.ACCOMPLISHED;
            systemTrans.TransactionType = TransactionType.REFUND;
            systemTrans.CoinExchange = package.PriceShip * profitPercent - (package.PriceShip * profitPercentRefund) - totalPrice;
            systemTrans.BalanceWallet = systemWallet.Balance - (package.PriceShip * profitPercentRefund) - totalPrice;
            systemTrans.PackageId = package.Id;
            systemTrans.WalletId = systemWallet.Id;

            Transaction shipperTrans = new Transaction();
            shipperTrans.Description = "Hoàn trả thành công đơn hàng id : " + package.Id;
            shipperTrans.Status = TransactionStatus.ACCOMPLISHED;
            shipperTrans.TransactionType = TransactionType.DELIVERED_SUCCESS;
            shipperTrans.CoinExchange = totalPrice + package.PriceShip * (1 - profitPercent);
            shipperTrans.BalanceWallet = shipperWallet.Balance + totalPrice +
                package.PriceShip * (1 - profitPercent) * package.PriceShip * profitPercentRefund;
            shipperTrans.PackageId = package.Id;
            shipperTrans.WalletId = shipperWallet.Id;

            Transaction shopTrans = new Transaction();
            shopTrans.Description = "Hoàn trả thành công đơn hàng id : " + package.Id;
            shopTrans.Status = TransactionStatus.ACCOMPLISHED;
            shopTrans.TransactionType = TransactionType.DELIVERED_SUCCESS;
            shopTrans.CoinExchange = -package.PriceShip;
            shopTrans.BalanceWallet = shopWallet.Balance - package.PriceShip;
            shopTrans.PackageId = package.Id;
            shopTrans.WalletId = shopWallet.Id;

            systemWallet.Balance = systemWallet.Balance - (package.PriceShip * profitPercentRefund) - totalPrice;
            shipperWallet.Balance = shipperWallet.Balance + totalPrice +
                package.PriceShip * (1 - profitPercent) * package.PriceShip * profitPercentRefund;
            shopWallet.Balance = shopWallet.Balance - package.PriceShip;

            List<Transaction> transactions = new List<Transaction> {
                    systemTrans, shipperTrans, shopTrans
                };
            await _transactionRepo.InsertAsync(transactions);
            #endregion

            #region Create history
            HistoryPackage history = new HistoryPackage();
            history.FromStatus = package.Status;
            history.ToStatus = PackageStatus.REFUND_SUCCESS;
            history.Description = $"Shop({package.Shop}) xác nhận shipper đã trả hàng thành công vào lúc: " + DateTime.UtcNow.ToString(DateTimeFormatConstant.DEFAULT);
            history.PackageId = package.Id;

            await _historyPackageRepo.InsertAsync(history);
            package.Status = PackageStatus.REFUND_SUCCESS;
            #endregion
            await _unitOfWork.CompleteAsync();

            return response;
        }

        public async Task<ApiResponse> RefundFailed(Guid packageId)
        {
            ApiResponse response = new ApiResponse();
            Package? package = await _packageRepo.GetByIdAsync(packageId, disableTracking: false);
            #region Verify params
            if (package == null)
            {
                response.ToFailedResponse("Gói hàng không tồn tại");
                return response;
            }
            if (package.Status != PackageStatus.DELIVERED)
            {
                response.ToFailedResponse("Hàng không giao thất bại thì hoàn trả cái gì!!");
                return response;
            }
            #endregion
            #region Create history
            HistoryPackage history = new HistoryPackage();
            history.FromStatus = package.Status;
            history.ToStatus = PackageStatus.REFUND_FAILED;
            history.Description = $"Shipper({package.ShipperId}) trả hàng thất bại vào lúc: " + DateTime.UtcNow.ToString(DateTimeFormatConstant.DEFAULT);
            history.PackageId = package.Id;

            await _historyPackageRepo.InsertAsync(history);
            package.Status = PackageStatus.REFUND_FAILED;
            #endregion
            await _unitOfWork.CompleteAsync();

            return response;
        }

    }
}