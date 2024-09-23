using Microsoft.AspNetCore.Mvc;

namespace SaraswatiConstruction.WebApplication.Controllers.Dashboard
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View(); 
        }
    }
}
