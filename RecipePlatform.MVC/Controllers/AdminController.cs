using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipePlatform.Models;

namespace RecipePlatform.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: Admin
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        // GET: Admin/Details/{id}
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            ViewBag.Roles = roles;

            return View(user);
        }

        // POST: Admin/MakeAdmin/{id}
        [HttpPost]
        public async Task<IActionResult> MakeAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("Index");
        }

        // POST: Admin/RemoveAdmin/{id}
        [HttpPost]
        public async Task<IActionResult> RemoveAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _userManager.RemoveFromRoleAsync(user, "Admin");
            return RedirectToAction("Index");
        }

        // POST: Admin/DeleteUser/{id}
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        // GET: Admin/Search?email=abc@mail.com
        public async Task<IActionResult> Search(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return View();

            var user = await _userManager.FindByEmailAsync(email);
            return View(user);
        }
    }
}
