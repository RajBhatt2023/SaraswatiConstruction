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
                    string.IsNullOrEmpty(userDetail.Password) ||
                    string.IsNullOrEmpty(userDetail.Url))
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
        [HttpGet("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(string? token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return Ok(new
                    {
                        ResultCode = Convert.ToInt32(CommonConstants.One),
                        ResultDescription = Messages.Null_Input
                    });
                }

                Result result = await _accountService.VerifyEmail(token);

                if (result.ResultCode == CommonConstants.Zero)
                {
                    return Ok(new { UserID = result.Id, result.ResultCode, result.ResultDescription });
                }
                else
                {
                    return Ok(new { result.ResultCode, result.ResultDescription });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Messages.Error500 + ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDetail userCredential)
        {
            try
            {
                if (string.IsNullOrEmpty(userCredential.Email) ||
                    string.IsNullOrEmpty(userCredential.Password))
                {
                    return Ok(new
                    {
                        ResultCode = Convert.ToInt32(CommonConstants.One),
                        ResultDescription = Messages.Null_Input
                    });
                }

                UserDetail result = await _accountService.Login(userCredential);

                return Ok(new { result.ResultCode, result.ResultDescription });

            }
            catch (Exception ex)
            {
                return StatusCode(500, Messages.Error500 + ex.Message);
            }

        }
    }
}
