﻿using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Data;
using unitofwork_core.Helper;
using unitofwork_core.Service.DatabaseService;
using unitofwork_core.Service.ShopService;

namespace unitofwork_core.Config
{
    public static class DIServiceExtension
    {
        public static void AddDIServiceApp(this IServiceCollection services) {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IJWTHelper, JWTHelper>();
            services.AddTransient<IShopService, ShopService>();
        }
    }
}