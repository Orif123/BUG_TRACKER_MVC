using BugTrackerProj.Data;
using BugTrackerProj.Service;
using BugTrackerProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq;
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

        [HttpGet]
        public IActionResult Index(string searchtext = "")
        {
            return View(_bugService.GetAllBugs(searchtext));
        }
        public IActionResult NewBug(Bug bug)
        {
            ViewBag.CategoryId = _bugService.GetCategories().Select(x => new SelectListItem { Text =bug.Category.CtaegoryName , Value = bug.CategoryId })
                 .ToList(); ;
            return View();
        }

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
