﻿namespace unitofwork_core.Config
{
    public static class CorsExtension
    {
        public static void AddCorsApp(this IServiceCollection services) {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builer => {
                    builer
                         .WithOrigins("http://localhost:3000", "https://old-stuff-exchange-3d9f0.web.app",
                         "https://old-stuff-exchange-3d9f0.firebaseapp.com")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                         .AllowAnyHeader()
                        .AllowAnyMethod();
                    });

            });
        }
    }
}
