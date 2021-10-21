using BugTrackerProj.Data;
using BugTrackerProj.Service;
using BugTrackerProj.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Controllers
{
   [Authorize(Roles = "CompanyManager, Admin")]
    public class AdminRoleController : Controller
    {
        
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBugService _bugService;
        public AdminRoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IBugService bugService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _bugService = bugService;
        }
        public  IActionResult Index(MainPageViewModel model)
        {
            if (User.IsInRole("CompanyManager"))
            {
               model.Bugs = _bugService.GetAllBugs();
                return View(model);
            }
            var userid =  _userManager.GetUserId(User);
            var user = _bugService.GetRealUsers().SingleOrDefault(p => p.Id == userid);
            model.Bugs=_bugService.GetBugsByProject(user.ProjectId);
            return View(model);
        }
            
        [Authorize(Roles = "CompanyManager")]
        public IActionResult Create()
        {
            return View();
        } 
        [HttpPost]
        public async Task<IActionResult> Create(UserToRoleRegistraionViewModel model)
        {
            var IsRoleExists = await _roleManager.RoleExistsAsync(model.RoleName);
            if (IsRoleExists)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                await _userManager.AddToRoleAsync(user, model.RoleName);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Solved(string id)
        {
            _bugService.BugSolved(id);
            return RedirectToAction("Index", "AdminRole");
        }


    }
}
