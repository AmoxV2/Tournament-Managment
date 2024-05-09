using Microsoft.AspNetCore.Mvc;

namespace WWW_APP_PROJECT.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
