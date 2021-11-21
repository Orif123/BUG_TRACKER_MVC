using BugTrackerProj.Data;
using BugTrackerProj.Models;
using BugTrackerProj.Service;
using BugTrackerProj.ViewModels;
using BugTrackerProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IProjectService _projectService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBugService _bugService;
        public AdminRoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IBugService bugService, ICategoryService categoryService, IUserService userService, IProjectService projectService)
        {
            _projectService = projectService;
            _userService = userService;
            _roleManager = roleManager;
            _userManager = userManager;
            _bugService = bugService;
            _categoryService = categoryService;
        }
        [Authorize(Roles = "CompanyManager")]
        public IActionResult Index(MainPageViewModel model)
        {
            ViewBag.CategoryId = _categoryService.ListItemCategories();
            
            return View(_bugService.GetAllBugsManager(model));
        }
        [Authorize(Roles = "CompanyManager")]
        public IActionResult Create()
        {
            var users = _userService.GetUserNames().ToList();
            ViewBag.Users = users;
            var roles = _userService.GetUserRoles().ToList();
            ViewBag.Roles = roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserToRoleRegistraionViewModel model)
        {
            var IsRoleExists = await _roleManager.RoleExistsAsync(model.RoleName);
            if (IsRoleExists)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                var roles = (List<string>)await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                await _userManager.AddToRoleAsync(user, model.RoleName);
                return RedirectToAction("Index", "UserList");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Solved(string id)
        {
            _bugService.BugSolved(id);
            if (User.IsInRole("CompanyManager"))
            {
                return RedirectToAction("Index", "AdminRole");
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "CompanyManager")]
        public IActionResult CreateCategory()
        {
            ViewBag.ProjectId = _projectService.GetProjects().ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(NewCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _categoryService.NewCategory(model);
                return RedirectToAction("GetCategories", "AdminRole");
            }
            return View();
        }
        [Authorize(Roles = "CompanyManager")]
        public IActionResult CreateProject()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            if (ModelState.IsValid)
            {
                _projectService.NewProject(project);
                return RedirectToAction("GetProjects", "AdminRole");
            }
            return View();
        }
        [Authorize(Roles = "CompanyManager")]
        public IActionResult GetCategories()
        {
            return View(_categoryService.GetAllCategories());
        }
        [Authorize(Roles = "CompanyManager")]
        public IActionResult GetProjects()
        {
            return View(_projectService.GetRealProjects());
        }
        [HttpPost]
        public IActionResult DeleteProject(string id)
        {
            _projectService.DeleteProject(id);
            return RedirectToAction("GetProjects");
        }
        [HttpPost]
        public IActionResult DeleteCategory(string id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction("GetCategories");
        }
    }
}
