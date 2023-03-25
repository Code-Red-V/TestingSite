using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Testing.Models;
using Testing.ViewModels;

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
            var questions = app.Questions.Include(a=>a.Answers).ToList();
             return View(questions);
        }
        [HttpPost]
        public IActionResult CheckAnswers(List<int> question,List<string> Q3)
        {
            return Ok();
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