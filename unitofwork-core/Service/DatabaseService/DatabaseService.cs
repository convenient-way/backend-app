using Bogus;
using unitofwork_core.Constant.Order;
using unitofwork_core.Constant.OrderRouting;
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
        private readonly IOrderRoutingRepository _orderRoutingRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IShipperRepository _shipperRepo;
        private readonly IShopRepository _shopRepo;
        private readonly IWalletRepository _walletRepo;
        public DatabaseService(ILogger<DatabaseService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _productRepo = unitOfWork.Products;
            _orderRoutingRepo = unitOfWork.OrderRoutings;
            _orderRepo = unitOfWork.Orders;
            _shipperRepo = unitOfWork.Shippers;
            _walletRepo = unitOfWork.Wallets;
            _shopRepo = unitOfWork.Shops;
        }
        public void RemoveData()
        {
            _unitOfWork.Products.DeleteRange(_productRepo.GetAll());
            _unitOfWork.OrderRoutings.DeleteRange(_orderRoutingRepo.GetAll());
            _unitOfWork.Orders.DeleteRange(_orderRepo.GetAll());
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
                .RuleFor(u => u.HomeLatitude, faker => faker.Random.Double(min : minLatitude, max: maxLatitude))
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
            List<Shop> shops = FakerShop.Generate(4);
            for (int i = 0; i < shops.Count; i++)
            {
                Shop shopIndex = shops[i];
                Wallet defaultWallet = new Wallet
                {
                    WalletType = WalletType.DEFAULT,
                    Status = WalletStatus.ACTIVE,
                    ShopId = shopIndex.Id,
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

            Faker<Order> FakerOrder = new Faker<Order>()
                .RuleFor(o => o.NumberPackage, faker => 2)
                .RuleFor(o => o.StartLongitude, faker => faker.Random.Double(min: minLongitude, max: maxLongitude))
                .RuleFor(o => o.StartLatitude, faker => faker.Random.Double(min: minLatitude, max: maxLatitude))
                .RuleFor(o => o.Distance, faker => faker.Random.Double(min: 2.5, max: 20))
                .RuleFor(o => o.Volume, faker => faker.Random.Double(min: 5, max: 50))
                .RuleFor(o => o.Price, faker => faker.Random.Int(min : 200, max :400) * 1000)
                .RuleFor(o => o.Status, faker => OrderStatus.APPROVED)
                .RuleFor(o => o.Shop, faker => faker.PickRandom(shops));
            List<Order> orders = FakerOrder.Generate(500);
            for (int i = 0; i < orders.Count; i++)
            {
                Faker<OrderRouting> FakerOrderRouting = new Faker<OrderRouting>()
                    .RuleFor(or => or.ToLongitude, faker => faker.Random.Double(min: minLongitude, max: maxLongitude))
                    .RuleFor(or => or.ToLatitude, faker => faker.Random.Double(min: minLatitude, max: maxLatitude))
                    .RuleFor(or => or.Distance, faker => faker.Random.Double(min: 2.5, max: 20))
                    .RuleFor(or => or.Status, faker => OrderRoutingStatus.WAITING)
                    .RuleFor(or => or.Order, faker => orders[i])
                    .RuleFor(or => or.Address, faker => faker.Person.Address.City);
                List<OrderRouting> orderRoutings = FakerOrderRouting.Generate(2);
                orderRoutings[0].RoutingIndex = 0;
                orderRoutings[1].RoutingIndex = 1;
                for (int j = 0; j < orderRoutings.Count; j++)
                {
                    Faker<Product> FakerProduct = new Faker<Product>()
                        .RuleFor(p => p.Name, faker => faker.Lorem.Letter(2))
                        .RuleFor(p => p.Description, faker => faker.Lorem.Sentence(1))
                        .RuleFor(p => p.OrderRouting, faker => orderRoutings[j]);
                    List<Product> products = FakerProduct.Generate(2);
                    orderRoutings[j].Products = products;
                }
                orders[i].OrderRoutings = orderRoutings;
            }
            List<Order> orders2 = FakerOrder.Generate(500);
            for (int i = 0; i < orders2.Count; i++)
            {
                Faker<OrderRouting> FakerOrderRouting = new Faker<OrderRouting>()
                    .RuleFor(or => or.ToLongitude, faker => faker.Random.Double(min: minLongitude, max: maxLongitude))
                    .RuleFor(or => or.ToLatitude, faker => faker.Random.Double(min: minLatitude, max: maxLatitude))
                    .RuleFor(or => or.Distance, faker => faker.Random.Double(min: 2.5, max: 20))
                    .RuleFor(or => or.Status, faker => OrderRoutingStatus.WAITING)
                    .RuleFor(or => or.Order, faker => orders2[i])
                    .RuleFor(or => or.Address, faker => faker.Person.Address.City);
                List<OrderRouting> orderRoutings = FakerOrderRouting.Generate(1);
                orderRoutings[0].RoutingIndex = 0;
                for (int j = 0; j < orderRoutings.Count; j++)
                {
                    Faker<Product> FakerProduct = new Faker<Product>()
                        .RuleFor(p => p.Name, faker => faker.Lorem.Letter(2))
                        .RuleFor(p => p.Description, faker => faker.Lorem.Sentence(1))
                        .RuleFor(p => p.OrderRouting, faker => orderRoutings[j]);
                    List<Product> products = FakerProduct.Generate(2);
                    orderRoutings[j].Products = products;
                }
                orders[i].OrderRoutings = orderRoutings;
            }
            _logger.LogInformation("Insert orders");
            await _orderRepo.InsertAsync(orders);

            // Save change
            _unitOfWork.Complete();
        }
    }
}
