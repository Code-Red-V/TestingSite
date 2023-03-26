using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Testing.Models;

namespace Testing.Controllers
{
    public class BackendController : Controller
    {
        private readonly ILogger<BackendController> _logger;
        private readonly ApplicationContext app;

        public BackendController(ILogger<BackendController> logger, ApplicationContext app)
        {
            _logger = logger;
            this.app = app;
        }
        public IActionResult Index()
        {
            return View(); /*"/Views/Backend/Index.cshtml"*/
        }

        
    }
}
