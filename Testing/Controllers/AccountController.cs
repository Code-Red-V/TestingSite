using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Testing.Models;
using Testing.ViewModels;
using Microsoft.EntityFrameworkCore;
using Elfie.Serialization;

namespace Testing.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext _context;
        public AccountController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult PersonalCabinet()
        {
            return View();
        }
        public IActionResult Profile()
        {
            User user = _context.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
            return PartialView("_ShowProfile", user);
        }
        public IActionResult Diary(int page = 1)
        {

            int pageSize = 10;
           
            User user = _context.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
            List<Result> result = _context.Results.Include(c=>c.Test).ThenInclude(c=>c.Category).Where(u => u.UserId == user.UserId).ToList();

            int count = result.Count();
            var items = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Results = items
            };

            return PartialView("_ShowDiary", viewModel);
        }
        [HttpPost]
        public IActionResult EditUser(string UserName)
        {
            User user = _context.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
            user.UserName = UserName;

            _context.Update(user);
            _context.SaveChanges();

            return RedirectToAction("PersonalCabinet","Account");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    _context.Users.Add(new User {UserName = model.UserName, Email = model.Email, Password = model.Password, RoleId = model.RoleId });
                    await _context.SaveChangesAsync();

                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            Role? role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == user.RoleId);

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimTypes.Role,role.Name.ToString().Replace(" ","")),
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
