using Microsoft.AspNetCore.Mvc;
using SaraswatiConstruction.WebApplication.Models;
using System.Text;
using System.Text.Json;

namespace SaraswatiConstruction.WebApplication.Controllers.Account
{
    public class AccountController : Controller
    {

        HttpClient _client;

        public AccountController(IConfiguration configuration, ILogger<AccountController> logger)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7150/Account/")
            };


        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(UserDetail userDetail)
        {
            if (userDetail.Email == "rajeev@test.com" && userDetail.Password == "Rajeev@123")
            {
                // Redirect to the Dashboard action of the DashboardController
                return RedirectToAction("Dashboard", "Dashboard");
            }
            else
            {
                ViewBag.Message = "Invalid email or password.";
                return View();
            }
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

        [HttpPost]
        public async Task<JsonResult> Registration(UserDetail userDetail)
        {
            try
            {
                if (userDetail != null)
                {
                    var data = JsonSerializer.Serialize(userDetail);

                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    var response = await _client.PostAsync($"{_client.BaseAddress}" + "Registration", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Result? result = JsonSerializer.Deserialize<Result>(apiResponse);

                        return Json(result);
                    }
                    else
                    {
                        return Json("");
                    }

                }
                else { return Json(""); }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }
    }
}
