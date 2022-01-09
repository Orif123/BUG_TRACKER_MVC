using BugTrackerProj.Data;
using BugTrackerProj.Models;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBugService _bugService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IHubContext<ApplicationHub> _hubContext;
        public HomeController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IBugService service, IHubContext<ApplicationHub> hubContext, ICategoryService categoryService, IUserService userService)
        {
            _userService = userService;
            _categoryService = categoryService;
            _signInManager = signInManager;
            _userManager = userManager;
            _bugService = service;
            _hubContext = hubContext;
        }
        [Authorize(Roles = "User, Admin, CompanyManager")]
        public IActionResult Index(MainPageViewModel model, string userid = "")
        {
            userid = _userManager.GetUserId(User);
            var user = _userManager.Users.SingleOrDefault(i => i.Id == userid);
            ViewBag.CategoryId = _categoryService.GetCategories(user.ProjectId);
            return View(_bugService.GetAllUserBugs(model, userid));
        }
        public IActionResult NewBug()
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == _userManager.GetUserId(User));
            ViewBag.CategoryId = _categoryService.GetCategories(user.ProjectId);
            ViewBag.MCategoryId = _categoryService.ListItemCategories();
            return View();
        }
        [HttpPost]
        public IActionResult Add(NewBugViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bug = new Bug()
                {
                    UserId = _userManager.GetUserId(User),
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    RepoLink = model.RepoLink
                };
                var category = _categoryService.GetAllCategories().SingleOrDefault(c => c.CategoryId == model.CategoryId);
                bug.ProjectId = category.ProjectId;
                _bugService.NewBug(bug);
                
                if (!User.IsInRole("CompanyManager"))
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "AdminRole");
            }
            return RedirectToAction("NewBug");
        }
        public IActionResult BugDetails(string id)
        {
            return View(_bugService.GetDetails(id));
        }
        [HttpPost]
        public async Task <IActionResult> AddComment(BugCommentDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = _userManager.GetUserId(User);
                _bugService.AddComment(model);
                var user = User.Identity.Name;
                var message = model.CommentText;

                await _hubContext.Clients.Group(model.BugId).SendAsync("NewBugReceived", user, message);
                return RedirectToAction("BugDetails", new { id = model.BugId });
            }
            return RedirectToAction("BugDetails");
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
