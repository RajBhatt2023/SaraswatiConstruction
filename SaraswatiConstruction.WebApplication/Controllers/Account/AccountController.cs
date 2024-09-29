using Microsoft.AspNetCore.Mvc;
using SaraswatiConstruction.WebApplication.Models;
using SaraswatiConstruction.WebApplication.Shared;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;

namespace SaraswatiConstruction.WebApplication.Controllers.Account
{
    public class AccountController : Controller
    {

        HttpClient _client;

        public AccountController(IConfiguration _configuration, ILogger<AccountController> logger)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration["APIUrl"] + AppConstants.Account)
            };


        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Login(UserDetail UserCredential)
        {
            UserDetailResult? result = new UserDetailResult();
            try
            {
                var data = JsonSerializer.Serialize(UserCredential);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"{_client.BaseAddress}Login", content);

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<UserDetailResult>(apiResponse);

                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    result = new UserDetailResult { resultCode = 500, resultDescription = Messages.DatabaseIssue };
                }
                else
                {
                    result = new UserDetailResult { resultCode = 2, resultDescription = Messages.SomethingWrong };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return Json(result);
        }


        // This is a placeholder for the Dashboard action method.
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> VerifyEmail(string token)
        {
            TempData["HideNavbar"] = "HideNavbar";

            if (token != null)
            {
                try
                {
                    var activateToken = token;
                    token = HttpUtility.UrlEncode(token);
                    var response = await _client.GetAsync($"{_client.BaseAddress}{AppConstants.VerifyEmail}{token}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        Result? result = JsonSerializer.Deserialize<Result>(content);
                        if (result?.resultCode == 0)
                        {
                            ViewData["UserID"] = result.id;
                            ViewData["Message"] = result.resultDescription;

                        }
                        else
                        {
                            ViewData["Message"] = result.resultDescription;
                        }
                    }
                    else
                    {
                        ViewData["Message"] = "Something is Wrong!";
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> Registration(UserDetail userDetail)
        {
            try
            {
                if (userDetail != null)
                {
                    var data = JsonSerializer.Serialize(userDetail);

                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    var response = await _client.PostAsync($"{_client.BaseAddress}Registration", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Result? result = JsonSerializer.Deserialize<Result>(apiResponse);

                        return Json(result);
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        Result errorResult = new Result { resultCode = 500, resultDescription = Messages.DatabaseIssue };
                        return Json(errorResult);
                    }
                    else
                    {
                        Result errorResult = new Result { resultCode = 2, resultDescription = Messages.SomethingWrong };
                        return Json(errorResult);
                    }

                }
                else
                {
                    Result errorResult = new Result { resultCode = 1, resultDescription = Messages.Null_Input };
                    return Json(errorResult);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }
    }
}
