using Microsoft.AspNetCore.Mvc;

namespace UFOT.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
