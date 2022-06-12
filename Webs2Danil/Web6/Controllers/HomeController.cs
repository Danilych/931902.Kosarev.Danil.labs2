using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web6.Models;
using Web6.Data;
using Web6.Models;
using Microsoft.EntityFrameworkCore;

namespace Web6.Controllers
{
    public class HomeController : Controller
    {
        private readonly Web6Context _context;

        public HomeController(Web6Context context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return _context.MainPost != null ?
                          View(await _context.MainPost.ToListAsync()) :
                          Problem("Entity set 'Web6Context.MainPost'  is null.");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}