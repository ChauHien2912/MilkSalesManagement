using MilkPurchasingManagement.Repo.Repositories;
using MilkPurchasingManagement.Repo.Service.OrderService;
using MilkPurchasingManagement.Repo.Service.ProductService;
using MilkPurchasingManagement.Repo.Service.UserService;
using System.Runtime.CompilerServices;


namespace WareHouseManagement.API.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            services.AddScoped<ProductService>();
            services.AddScoped<OrderService>();
            services.AddScoped<UserService>();
            return services;    
        }

        public static IServiceCollection AddDIRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }

        public static IServiceCollection AddDIAccessor(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
