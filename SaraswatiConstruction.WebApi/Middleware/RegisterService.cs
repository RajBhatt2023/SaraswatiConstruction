using SaraswatiConstruction.Infrastructure.IRepository;
using SaraswatiConstruction.Infrastructure.Repository;
using SaraswatiConstruction.Service.IService;
using SaraswatiConstruction.Service.Service;

namespace SaraswatiConstruction.WebApi.Middleware
{
    public class RegisterService
    {
        public static IServiceCollection AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            return services;
        }
    }
}
