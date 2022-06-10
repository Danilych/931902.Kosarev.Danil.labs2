using Microsoft.AspNetCore.Mvc;

namespace Web5.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
