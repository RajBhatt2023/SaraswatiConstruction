using SaraswatiConstruction.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraswatiConstruction.Infrastructure.IRepository
{
    public interface IAccountRepository
    {
        public Task<Result> RegisterUser(UserDetail userDetail);
        public Task<Result> VerifyEmail(int UserID);
        public Task<UserDetail> Login(UserDetail userCredential);
    }
}
