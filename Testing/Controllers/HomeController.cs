﻿using Microsoft.AspNetCore.Mvc;
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
            ViewBag.TestId = 1;

            var questions = app.Questions.Include(a=>a.Answers).ToList();
             return View(questions);
        }
        [HttpPost]
        public IActionResult SaveResult(string percent,int testId)
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = app.Users.FirstOrDefault(e => e.Email == User.Identity.Name);

                string newPercent = "";
                for (int i = 0; i < percent.Length; i++)
                {
                    if (percent[i] != '%')
                    {
                        newPercent += percent[i];
                    }
                }
                int intPercent = Convert.ToInt32(newPercent);
                var resultText = "Тест пройден";

                if (intPercent < 100)
                {
                    resultText = "";
                    resultText = "Тест не пройден";
                }

                Result result = new();
                result.TestId = testId;
                result.ResultText = resultText;
                result.UserId = user.UserId;
                result.CreationDate = DateTime.Now;
                result.RightAnswersPercent = intPercent;

                app.Results.Add(result);
                app.SaveChanges();

                return Json(new { isValid = true, message="Данные добавлены в дневник" });
            }
            else
            {
                return Json(new { isValid = false, message="Необходимо авторизоваться" });
            }

        }
        //[HttpPost]
        //public IActionResult CheckAnswers(List<int> question,List<string> Q3)
        //{
        //    return Ok();
        //}
        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

    }
}