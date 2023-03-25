using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Testing.Models;

namespace Testing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext app;

        public HomeController(ILogger<HomeController> logger, ApplicationContext app)
        {
            _logger = logger;
            this.app = app;
        }

        public IActionResult Index()
        {
             return View(app.Categories.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}