using Microsoft.AspNetCore.Mvc;

namespace Web2.Controllers
{
    using Web2.Models;
    public class CalcServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Manual()
        {
            return View();
        }

        public IActionResult ManualWithSeparateHandlers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ModelBindingInParameters()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ModelBindingInParameters(int firstNumber, string operation, int secondNumber)
        {
            ViewBag.Result = Web2.HtmlHelpersCode.ResultHelper.CreateResult(firstNumber, secondNumber, operation);
            return View("ModelResult");
        }

        [HttpGet]
        public IActionResult ModelBindingInSeparateModel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ModelBindingInSeparateModel(MathModel mathmodel)
        {
            ViewBag.Result = mathmodel.CreateResult();
            return View("ModelResult");
        }

    }
}
