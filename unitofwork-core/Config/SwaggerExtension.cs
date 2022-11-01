using Microsoft.OpenApi.Models;

namespace unitofwork_core.Config
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerApp(this IServiceCollection services) {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Convenient way", Version = "v1" });
            });
        }
    }
}
