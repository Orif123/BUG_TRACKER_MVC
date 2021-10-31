using BugTrackerProj.Data;
using BugTrackerProj.Models;
using BugTrackerProj.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Controllers
{
    public class EntryController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBugService _bugService;
        private readonly IUserService _userService;
        public EntryController(UserManager<ApplicationUser> userManager, IBugService bugService, IUserService userService)
        {
            _userService = userService;
            _bugService = bugService;
            _userManager = userManager;
        }
        [Authorize(Roles ="User, Admin, CompanyManager")]
        public IActionResult HomePage(string projectid)
        {
            if (User.IsInRole("CompanyManager"))
            {
                return View(_bugService.CountAllBugs());
            }
            var userid = _userManager.GetUserId(User);
            var user = _userService.GetRealUsers().SingleOrDefault(u => u.Id == userid);
            projectid = user.ProjectId;
            return View(_bugService.CountProjectBugs(projectid));
        }
        [HttpPost]
        public IActionResult Navigate()
        {
            if (User.IsInRole("CompanyManager"))
            {
                return RedirectToAction("Index", "AdminRole");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
