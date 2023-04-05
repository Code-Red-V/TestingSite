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
        public IActionResult ShowTests(int id,int page=1)
        {
            List<Test> tests;
            if (id == 0)
            {
                tests = app.Tests.Include(c => c.Category).ToList();
            }
            else
            {
               tests= app.Tests.Include(c => c.Category).Where(c => c.CategoryId == id).ToList();
            }
            int pageSize = 5;
           
            int count = tests.Count();
            var items = tests.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Tests = items
            };

            return PartialView("_ShowTests",viewModel);
        }      
        
    }
}