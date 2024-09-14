using Microsoft.AspNetCore.Mvc;

namespace SaraswatiConstruction.WebApplication.Controllers.Account
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
