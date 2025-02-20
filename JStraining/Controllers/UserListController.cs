using JStraining.DbConnection;
using JStraining.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JStraining.Controllers
{
    [Authorize]
    public class UserListController : Controller
    {
        public readonly AppDbContext db;
        public UserListController(AppDbContext _db)
        {
            db = _db;
        }
        [HttpGet]     
        public IActionResult Index()
        {
            int? userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var items = db.userLists.Where(x=> x.UserID == userId).ToList();
            return View(items);
        }
        [HttpGet]
        public IActionResult AddList()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddList(UserList ul)
        {
            int? userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0"); 
            if (userId == 0)
            {
                return RedirectToAction("Users", "Login");
            }
            ul.UserID = userId.Value;
            db.userLists.Add(ul);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
