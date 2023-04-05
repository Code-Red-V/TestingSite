using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;
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
            ViewBag.Categories = app.Categories.ToList();
             return View();
        }

        public IActionResult Questions(int id)
        {
            ViewBag.Test = app.Tests.Include(c=>c.Category).FirstOrDefault(i=>i.TestId==id);
            ViewBag.Categories = app.Categories.ToList();

            var questions = app.Questions.Include(a => a.Answers).Where(t=>t.TestId==id).ToList();
            return View(questions);
        }

        [DefaultBreadcrumb("Home")]
        public IActionResult ShowTests(int id)
        {
            if (id == 0)
            {
                ViewBag.Tests = app.Tests.Include(c => c.Category).ToList();
            }
            else
            {
                ViewBag.Tests = app.Tests.Include(c => c.Category).Where(c => c.CategoryId == id).ToList();
            }
         
            return PartialView("_ShowTests");
        }      
        
    }
}