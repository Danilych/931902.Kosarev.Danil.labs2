using Microsoft.AspNetCore.Mvc;

namespace Web1.Controllers;

public class CalcServiceController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult PassUsingViewData()
    {
        Random random = new Random();

        ViewData["Num1"] = random.Next(0,100);
        ViewData["Num2"] = random.Next(0,100);

        return View();
    }
    public IActionResult PassUsingModel()
    {
        Random random = new Random();

        var ViewModel = new Web1.Models.RandModel()
        {
            Num1 = random.Next(0, 100),
            Num2 = random.Next(0, 100)
        };

        return View(ViewModel);
    }
    public IActionResult PassUsingViewBag()
    {
        Random random = new Random();
        ViewBag.Num1 = random.Next(0, 100);
        ViewBag.Num2 = random.Next(0, 100);

        return View();
    }

    public IActionResult AccessServiceDirectly()
    {
        return View();
    }
}
