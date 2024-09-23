using Microsoft.AspNetCore.Mvc;

namespace SaraswatiConstruction.WebApplication.Controllers
{
    public class UserManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserManagement()
        {
            return View();
        }

        
    }

}
