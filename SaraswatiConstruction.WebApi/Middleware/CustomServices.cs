using SaraswatiConstruction.Infrastructure.DataAccess;
using SaraswatiConstruction.Infrastructure.DataAccess.Interface;
using SaraswatiConstruction.Infrastructure.IRepository;
using SaraswatiConstruction.Infrastructure.Repository;
using SaraswatiConstruction.Service.IService;
using SaraswatiConstruction.Service.Service;
using SaraswatiConstruction.Utility.CommunicationService;

namespace SaraswatiConstruction.WebApi.Middleware
{
    public class CustomServices
    {
        public static IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddScoped<IDataAccessHelper, DataAccessHelper>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
