using BugTrackerProj.Data;
using BugTrackerProj.Service;
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
        public EntryController(UserManager<ApplicationUser> userManager, IBugService bugService)
        {
            _bugService = bugService;
            _userManager = userManager;
        }
        public IActionResult HomePage(string projectid)
        {
            if (User.IsInRole("CompanyManager"))
            {
                return View(_bugService.CountAllBugs());
            }
            var userid = _userManager.GetUserId(User);
            var user = _bugService.GetRealUsers().SingleOrDefault(u => u.Id == userid);
            projectid = user.ProjectId;
            return View(_bugService.CountProjectBugs(projectid));
        }
        [HttpPost]
        public IActionResult Navigate()
        {
            if (User.IsInRole("CompanyManager")|| User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "AdminRole");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
