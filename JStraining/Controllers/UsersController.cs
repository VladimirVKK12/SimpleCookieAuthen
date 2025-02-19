using JStraining.DbConnection;
using JStraining.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace JStraining.Controllers
{
    public class UsersController : Controller
    {
        public readonly AppDbContext db;

        public UsersController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User users)
        {

            if (db.Users.Any(u => u.Name == users.Name))
            {
                ModelState.AddModelError("", "Този email вече съществува.");
                return View();
            }

            if (!db.Users.Any())
            {
                users.Role = "Admin";
            }
            else
            {
                users.Role = "User";
            }

            db.Users.Add(users);
            db.SaveChanges();

            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var users = db.Users.FirstOrDefault(u => u.Name == user.Name && u.Password == user.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Грешен email или парола.");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, users.Id.ToString()),
                new Claim(ClaimTypes.Name, users.Name),
                new Claim(ClaimTypes.Role, users.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
