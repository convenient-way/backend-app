using Bogus;
using unitofwork_core.Constant.ConfigConstant;
using unitofwork_core.Constant.OrderRouting;
using unitofwork_core.Constant.Package;
using unitofwork_core.Constant.Role;
using unitofwork_core.Constant.User;
using unitofwork_core.Constant.Wallet;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Core.Repository;
using unitofwork_core.Entities;

namespace unitofwork_core.Service.DatabaseService
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ILogger<DatabaseService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepo;
        private readonly IShipperRepository _shipperRepo;
        private readonly IPackageRepository _packageRepo;
        private readonly IShopRepository _shopRepo;
        private readonly IAdminRepository _adminRepo;
        private readonly IWalletRepository _walletRepo;
        private readonly IConfigRepostiory _configRepo;
        public DatabaseService(ILogger<DatabaseService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _productRepo = unitOfWork.Products;
            _shipperRepo = unitOfWork.Shippers;
            _walletRepo = unitOfWork.Wallets;
            _shopRepo = unitOfWork.Shops;
            _packageRepo = unitOfWork.Packages;
            _adminRepo = unitOfWork.Admins;
            _configRepo = unitOfWork.ConfigApps;
        }
        public void RemoveData()
        {
            _unitOfWork.ConfigApps.DeleteRange(_configRepo.GetAll());
            _unitOfWork.Products.DeleteRange(_productRepo.GetAll());
            _unitOfWork.Packages.DeleteRange(_packageRepo.GetAll());
            _unitOfWork.Wallets.DeleteRange(_walletRepo.GetAll());
            _unitOfWork.Shops.DeleteRange(_shopRepo.GetAll());
            _unitOfWork.Shippers.DeleteRange(_shipperRepo.GetAll());
            _unitOfWork.Complete();
        }

        public async void GenerateData()
        {
            double minLongitude = 106.60934755879953;
            double maxLongitude = 106.82648934410292;
            double minLatitude = 10.77371671523056;
            double maxLatitude = 10.843294269787952;

            List<string> avatarsLink = new List<string>();
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/4333/4333609.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/2202/2202112.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/4140/4140047.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/236/236832.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/3006/3006876.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/4333/4333609.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/4140/4140048.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/924/924874.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/4202/4202831.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/921/921071.png");
            Faker<Admin> FakerAdmin = new Faker<Admin>()
                 .RuleFor(u => u.UserName, faker => faker.Person.UserName)
                .RuleFor(u => u.Email, faker => faker.Person.Email)
                .RuleFor(u => u.Status, faker => faker.PickRandom(UserStatus.GetAllStatus()))
                .RuleFor(u => u.DisplayName, faker => faker.Person.FullName)
                .RuleFor(u => u.PhotoUrl, faker => faker.PickRandom(avatarsLink))
                .RuleFor(u => u.Gender, faker => faker.PickRandom(UserGender.GetGenders()))
                .RuleFor(u => u.Address, (faker, shipper) => faker.Person.Address.Street)
                .RuleFor(u => u.Password, faker => faker.Person.FirstName.ToLower())
                .RuleFor(u => u.PhoneNumber, faker => faker.Person.Phone);
            Admin admin = FakerAdmin.Generate();

            ConfigApp configProfit = new ConfigApp
            {
                Name = ConfigConstant.PROFIT_PERCENTAGE,
                Note = "20",
                ModifiedBy = admin.Id
            };
            ConfigApp configProfitRefund = new ConfigApp
            {
                Name = ConfigConstant.PROFIT_PERCENTAGE_REFUND,
                Note = "50",
                ModifiedBy = admin.Id
            };
            List<ConfigApp> configApps = new List<ConfigApp> {
                configProfit, configProfitRefund
            };
            await _configRepo.InsertAsync(configApps);

            await _adminRepo.InsertAsync(admin);
            Faker<Shipper> FakerShipper = new Faker<Shipper>()
                .RuleFor(u => u.UserName, faker => faker.Person.UserName)
                .RuleFor(u => u.Email, faker => faker.Person.Email)
                .RuleFor(u => u.Status, faker => faker.PickRandom(UserStatus.GetAllStatus()))
                .RuleFor(u => u.DisplayName, faker => faker.Person.FullName)
                .RuleFor(u => u.PhotoUrl, faker => faker.PickRandom(avatarsLink))
                .RuleFor(u => u.Gender, faker => faker.PickRandom(UserGender.GetGenders()))
                .RuleFor(u => u.Address, (faker, shipper) => faker.Person.Address.Street)
                .RuleFor(u => u.Password, faker => faker.Person.FirstName.ToLower())
                .RuleFor(u => u.HomeLongitude, faker => faker.Random.Double(min: minLongitude, max: maxLongitude))
                .RuleFor(u => u.HomeLatitude, faker => faker.Random.Double(min: minLatitude, max: maxLatitude))
                .RuleFor(u => u.DestinationLongitude, faker => faker.Random.Double(min: minLongitude, max: maxLongitude))
                .RuleFor(u => u.DestinationLatitude, faker => faker.Random.Double(min: minLatitude, max: maxLatitude))
                .RuleFor(u => u.PhoneNumber, faker => faker.Person.Phone);
            List<Shipper> shippers = FakerShipper.Generate(10);
            for (int i = 0; i < shippers.Count; i++)
            {
                Shipper shipperIndex = shippers[i];
                Wallet defaultWallet = new Wallet
                {
                    WalletType = WalletType.DEFAULT,
                    Status = WalletStatus.ACTIVE,
                    ShipperId = shipperIndex.Id,
                    Balance = 1000000
                };
                Wallet promotionWallet = new Wallet
                {
                    WalletType = WalletType.PROMOTION,
                    Status = WalletStatus.ACTIVE,
                    ShipperId = shipperIndex.Id
                };
                List<Wallet> wallets = new List<Wallet> {
                defaultWallet, promotionWallet
            };
                shipperIndex.Wallets = wallets;
            }

            Wallet systemWallet = new Wallet
            {
                WalletType = WalletType.SYSTEM,
                Status = WalletStatus.ACTIVE,
                Balance = 0,
            };
            _logger.LogInformation("Insert wallets");
            await _walletRepo.InsertAsync(systemWallet);

            await _shipperRepo.InsertAsync(shippers);
            _logger.LogInformation("Insert shippers");

            Faker<Shop> FakerShop = new Faker<Shop>()
               .RuleFor(u => u.UserName, faker => faker.Person.UserName)
               .RuleFor(u => u.Email, faker => faker.Person.Email)
               .RuleFor(u => u.Status, faker => faker.PickRandom(UserStatus.GetAllStatus()))
               .RuleFor(u => u.DisplayName, faker => faker.Person.FullName)
               .RuleFor(u => u.PhotoUrl, faker => faker.PickRandom(avatarsLink))
                .RuleFor(u => u.Password, faker => faker.Person.FirstName.ToLower())
               .RuleFor(u => u.Address, (faker, shipper) => faker.Person.Address.Street)
               .RuleFor(u => u.Longitude, faker => faker.Random.Double(min: minLongitude, max: maxLongitude))
               .RuleFor(u => u.Latitude, faker => faker.Random.Double(min: minLatitude, max: maxLatitude))
               .RuleFor(u => u.PhoneNumber, faker => faker.Person.Phone);
            List<Shop> shops = FakerShop.Generate(10);
            for (int i = 0; i < shops.Count; i++)
            {
                Shop shopIndex = shops[i];
                Wallet defaultWallet = new Wallet
                {
                    WalletType = WalletType.DEFAULT,
                    Status = WalletStatus.ACTIVE,
                    ShopId = shopIndex.Id,
                    Balance = 1000000
                };
                Wallet promotionWallet = new Wallet
                {
                    WalletType = WalletType.PROMOTION,
                    Status = WalletStatus.ACTIVE,
                    ShopId = shopIndex.Id
                };
                List<Wallet> wallets = new List<Wallet> {
                defaultWallet, promotionWallet
            };
                shopIndex.Wallets = wallets;
            }
            await _shopRepo.InsertAsync(shops);
            _logger.LogInformation("Insert shops");

            Faker<Package> FakerPackage = new Faker<Package>()
                .RuleFor(o => o.StartAddress, faker => faker.Address.FullAddress())
                .RuleFor(o => o.StartLongitude, faker => faker.Random.Double(min: minLongitude, max: maxLongitude))
                .RuleFor(o => o.StartLatitude, faker => faker.Random.Double(min: minLatitude, max: maxLatitude))
                .RuleFor(o => o.DestinationAddress, faker => faker.Address.FullAddress())
                .RuleFor(o => o.DestinationLongitude, faker => faker.Random.Double(min: minLongitude, max: maxLongitude))
                .RuleFor(o => o.DestinationLatitude, faker => faker.Random.Double(min: minLatitude, max: maxLatitude))
                .RuleFor(o => o.Distance, faker => faker.Random.Double(min: 2.5, max: 20))
                .RuleFor(o => o.ReceiverName, faker => faker.Person.FullName)
                .RuleFor(o => o.ReceiverPhone, faker => faker.Person.Phone)
                .RuleFor(o => o.Volume, faker => faker.Random.Double(min: 5, max: 50))
                .RuleFor(o => o.Weight, faker => faker.Random.Int(5, 20))
                .RuleFor(o => o.PhotoUrl, faker => faker.Image.ToString())
                .RuleFor(o => o.Note, faker => faker.Lorem.Sentence(6))
                .RuleFor(o => o.PriceShip, faker => faker.Random.Int(min: 10, max: 40) * 1000)
                .RuleFor(o => o.Status, faker => PackageStatus.APPROVED)
                .RuleFor(o => o.Shop, faker => faker.PickRandom(shops));
            List<Package> packages = FakerPackage.Generate(300);
            for (int i = 0; i < packages.Count; i++)
            {

                Faker<Product> FakerProduct = new Faker<Product>()
                       .RuleFor(p => p.Name, faker => faker.Lorem.Letter(2))
                       .RuleFor(p => p.Price, faker => faker.Random.Int(100, 200) * 1000)
                       .RuleFor(p => p.Description, faker => faker.Lorem.Sentence(1));
                List<Product> products = FakerProduct.Generate(2);
                packages[i].Products = products;
            }

            _logger.LogInformation("Insert orders");
            await _packageRepo.InsertAsync(packages);

            // Save change
            _unitOfWork.Complete();
        }
    }
}
