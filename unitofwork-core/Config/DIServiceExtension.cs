using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Data;
using unitofwork_core.Helper;
using unitofwork_core.Service.AdminService;
using unitofwork_core.Service.AuthorizeService;
using unitofwork_core.Service.MapboxService;
using unitofwork_core.Service.OrderService;
using unitofwork_core.Service.ShipperService;
using unitofwork_core.Service.ShopService;

namespace unitofwork_core.Config
{
    public static class DIServiceExtension
    {
        public static void AddDIServiceApp(this IServiceCollection services) {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IJWTHelper, JWTHelper>();
            services.AddTransient<IShopService, ShopService>();
            services.AddTransient<IShipperService, ShipperService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IAuthorizeService, AuthorizeService>();
            services.AddTransient<IMapboxService, MapboxService>();
            services.AddTransient<IOrderService, OrderService>();
        }
    }
}
