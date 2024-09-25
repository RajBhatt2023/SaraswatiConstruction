using SaraswatiConstruction.Domain.Models;
using SaraswatiConstruction.Infrastructure.IRepository;
using SaraswatiConstruction.Service.IService;

namespace SaraswatiConstruction.Service.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Result> RegisterUser(UserDetail userDetail)
        {
            Result result = await _accountRepository.RegisterUser(userDetail);
            return result;
        }

    }
}
