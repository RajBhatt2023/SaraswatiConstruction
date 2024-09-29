using SaraswatiConstruction.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaraswatiConstruction.Service.IService
{
    public interface IAccountService
    {
        public Task<Result> RegisterUser(UserDetail userDetail);
        public Task<Result> VerifyEmail(string? token);
        public Task<UserDetail> Login(UserDetail userCredential);
    }
}
