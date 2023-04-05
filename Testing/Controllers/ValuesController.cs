using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using Testing.Models;
using System.Runtime.Serialization.Json;
using System.Configuration;
using System.Net;
using Testing.ViewModels;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace Testing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationContext app;

        public ValuesController(ApplicationContext app)
        {
            this.app = app;
        }
        //[HttpGet]
        //[Route("Results")]
        //public IActionResult GetResults()
        //{
        //    User user = app.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
        //    List<Result> result = app.Results.Include(c => c.Test).ThenInclude(c => c.Category).Where(u => u.UserId == user.UserId).ToList();

        //    return new JsonResult(result.ToJson());
        //}
        
        [Route("Process")]
        [HttpPost]
        public IActionResult PostSaveResult(SaveResultModel saveResult)
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = app.Users.FirstOrDefault(e => e.Email == User.Identity.Name);

                bool isTestPassed = true;
                if (saveResult.percentPar < 100)
                {
                    isTestPassed = false;
                }

                Result result = new();
                result.TestId = saveResult.testPar;
                result.IsTestPassed = isTestPassed;
                result.UserId = user.UserId;
                result.CreationDate = DateTime.Now;
                result.RightAnswersPercent = saveResult.percentPar;

                app.Results.Add(result);
                app.SaveChanges();

                return new JsonResult(new { isValid = true,message="Данные добавлены в дневник" });
            }
        
            else
            {
                return new JsonResult(new { isValid = false, message = "Необходимо авторизоваться" });
            }

        }
    }
}
