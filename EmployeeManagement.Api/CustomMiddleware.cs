using EmployeeManagement.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api
{
    public static class CustomMiddleware
    {
        public static IServiceCollection AddCustomeDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddEntityFrameworkSqlServer().AddDbContext<EmployeeManagementContext>(options =>
            {
                options.UseLazyLoadingProxies()
                .UseSqlServer(configuration["EmployeeDbConnectionString"],
                              sqlServerOptionsAction: sqlOptions =>
                              {
                                  sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                              });
            });

            return service;

        }

        public static IServiceCollection EnableCors(this IServiceCollection services, IConfiguration configuration)
        {

            var allowedOrigins = new List<string>();
            var allowOrigins = configuration["AllowedOrigins"].Split(",");

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy",
                          builder => builder.WithOrigins(allowOrigins)
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
            });

            return services;
        }
    }
}
