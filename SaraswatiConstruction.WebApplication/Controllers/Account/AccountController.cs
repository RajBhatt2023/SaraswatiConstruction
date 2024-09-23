using Microsoft.AspNetCore.Mvc;
using SaraswatiConstruction.WebApplication.Models;

namespace SaraswatiConstruction.WebApplication.Controllers.Account
{
    public class AccountController : Controller
    {
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
    }
}
