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
    public class UserListController : Controller
    {
        private readonly IBugService _bugService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserListController(IBugService bugService, UserManager<ApplicationUser> userManager)
        {
            _bugService = bugService;
            _userManager = userManager;
        }
        public IActionResult Index(string projectid)
        {
            if (User.IsInRole("CompanyManagaer"))
            {
                return View(_bugService.GetRealUsers());
            }
            var userid = _userManager.GetUserId(User);
            var user = _bugService.GetRealUsers().SingleOrDefault(u => u.Id == userid);
            projectid = user.ProjectId;
            return View(_bugService.GetUserByProject(projectid));
        }
    }
}
