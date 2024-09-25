using Dapper;
using SaraswatiConstruction.Domain.Constant;
using SaraswatiConstruction.Domain.GlobalResource;
using SaraswatiConstruction.Domain.Models;
using SaraswatiConstruction.Infrastructure.DataAccess.Interface;
using SaraswatiConstruction.Infrastructure.IRepository;
using SaraswatiConstruction.Utility;

namespace SaraswatiConstruction.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDataAccessHelper _dataAccessHelper;

        public AccountRepository(IDataAccessHelper dataAccessHelper)
        {
            _dataAccessHelper = dataAccessHelper;
        }

        /// <summary>
        /// This will Register User.
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        public async Task<Result> RegisterUser(UserDetail userDetail)
        {
            Result result = new Result();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FirstName", userDetail.FirstName);
                parameters.Add("@LastName", userDetail.LastName);
                parameters.Add("@Email", userDetail.Email);
                parameters.Add("@HashPassword", CommonFunctions.EncryptPassword(userDetail.Password));

                var dbResult = await _dataAccessHelper.ExecuteStoredProcedureDataTableAsync<dynamic>("Api_Account_RegisterUser", parameters);

                if (dbResult != null && dbResult.Any())
                {
                    var getResult = dbResult.FirstOrDefault();
                    if (getResult != null)
                    {
                        if (getResult.ResultCode == Convert.ToString(CommonConstants.Zero))
                        {
                            result.ResultCode = Convert.ToInt32(getResult.ResultCode);
                            result.ResultDescription = Convert.ToString(getResult.ResultDescription);
                        }
                        else
                        {
                            result.ResultCode = Convert.ToInt32(getResult.ResultCode);
                            result.ResultDescription = Convert.ToString(getResult.ResultDescription);
                        }
                    }
                    else
                    {
                        result.ResultCode = CommonConstants.Two;
                        result.ResultDescription = Messages.FailedToRegister;
                    }
                }
                else
                {
                    result.ResultCode = CommonConstants.Two;
                    result.ResultDescription = Messages.FailedToRegister;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return result;
        }
    }
}
