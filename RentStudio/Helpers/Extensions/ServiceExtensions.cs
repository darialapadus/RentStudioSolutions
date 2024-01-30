using RentStudio.Helpers.JwtUtils;
using RentStudio.Repositories.UserRepository;
using RentStudio.Services.UserService;

namespace RentStudio.Helpers.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddTransient<IJwtUtils, JwtUtils.JwtUtils>();

            return services;
        }
    }
}
