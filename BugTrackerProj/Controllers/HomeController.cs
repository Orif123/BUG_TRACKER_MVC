using BugTrackerProj.Data;
using BugTrackerProj.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BugTrackerProj.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBugService _bugService;
        public HomeController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IBugService service)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _bugService = service;

        }
        public IActionResult Index(string id)
        {
            return View(_bugService.GetAllBugs(id));
        }
        [HttpGet]
        public IActionResult GetBugByCategory(string id)
        {
            return View(_bugService.GetBugsByCategory(id));
        }
        //[HttpGet]
        //public IActionResult (string id)
        //{
        //    return View(_bugService.GetBugsByUser(id));
        //}

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
