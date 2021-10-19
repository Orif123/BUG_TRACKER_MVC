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
        private readonly IManagementService _managementService;
        public AdminRoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IManagementService managementService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _managementService = managementService;
        }
        public  IActionResult Index(MainPageViewModel model)
        {
            if (User.IsInRole("CompanyManager"))
            {
               model.Bugs = _managementService.GetAllBugs();
                return View(model);
            }
            var userid =  _userManager.GetUserId(User);
            var user = _managementService.GetUsers().SingleOrDefault(p => p.Id == userid);
            model.Bugs=_managementService.GetBugsByProject(user.ProjectId);
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


    }
}
