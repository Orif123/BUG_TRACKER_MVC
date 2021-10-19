using BugTrackerProj.Data;
using BugTrackerProj.Service;
using BugTrackerProj.ViewModels;
using BugTrackerProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBugService _bugService;
        private readonly IHubContext<ApplicationHub> _hubContext;
        public HomeController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IBugService service, IHttpContextAccessor accessor, IHubContext<ApplicationHub> hubContext)
        {
            _accessor = accessor;
            _signInManager = signInManager;
            _userManager = userManager;
            _bugService = service;
            _hubContext = hubContext;
        }
        [Authorize(Roles ="User, CompanyManager, Admin")]
        public async Task <IActionResult> Index(MainPageViewModel model, string userid = "")
        {
            userid = _userManager.GetUserId(User);
            var user = _bugService.GetRealUsers().SingleOrDefault(i => i.Id == userid);
            ViewBag.CategoryId = _bugService.GetCategories(user.ProjectId);
            await _hubContext.Clients.All.SendAsync("loadBugs");
            return View(_bugService.GetAllBugs(model, userid));
        }

        public IActionResult NewBug(Bug bug)
        {
            bug.BugId = GetId().ToString();
            bug.UserId = _userManager.GetUserId(User);
            bug.BugDate = DateTime.Now;
            ApplicationUser user = _bugService.GetRealUsers().SingleOrDefault(u => u.Id == bug.UserId);
            bug.ProjectId = user.ProjectId;
            ViewBag.CategoryId = _bugService.GetCategories(bug.ProjectId);
            return View(bug);
        }
        [HttpPost]
        public  IActionResult Add(Bug bug)
        {
            if (ModelState.IsValid)
            {
                _bugService.NewBug(bug);
               
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("NewBug");
        }
        [HttpPost]
        public IActionResult Solved(string id)
        {
            _bugService.BugSolved(id);
            return RedirectToAction("Index", "Home");
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
        public Guid GetId()
        {
            var id =Guid.NewGuid();
            return id;


        }
            
    }

    
}
