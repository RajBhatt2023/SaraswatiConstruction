using Microsoft.AspNetCore.Mvc;
using SaraswatiConstruction.Domain.Constant;
using SaraswatiConstruction.Domain.GlobalResource;
using SaraswatiConstruction.Domain.Models;
using SaraswatiConstruction.Service.IService;

namespace SaraswatiConstruction.WebApi.Controllers.Account
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration(UserDetail userDetail)
        {
            try
            {
                if (string.IsNullOrEmpty(userDetail.FirstName) ||
                    string.IsNullOrEmpty(userDetail.LastName) ||
                    string.IsNullOrEmpty(userDetail.Email) ||
                    string.IsNullOrEmpty(userDetail.Password))
                {
                    return Ok(new
                    {
                        ResultCode = Convert.ToInt32(CommonConstants.One),
                        ResultDescription = Messages.Null_Input
                    });
                }

                Result result = await _accountService.RegisterUser(userDetail);

                return Ok(new { result.ResultCode, result.ResultDescription });

            }
            catch (Exception ex)
            {
                return StatusCode(500, Messages.Error500 + ex.Message);
            }
        }

    }
}
