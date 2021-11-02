using BugTrackerProj.Data;
using BugTrackerProj.Models;
using BugTrackerProj.Service;
using BugTrackerProj.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Controllers
{
    public class AccountController : Controller
    {
        private readonly IBugService _bugService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IProjectService _projectService;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IBugService bugService, IWebHostEnvironment hostingEnvironment, IProjectService projectService)
        {
            _projectService = projectService;
            _hostingEnvironment = hostingEnvironment;
            _bugService = bugService;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            ViewBag.ProjectId = _projectService.GetProjects().ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewAccount(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    var uploadsfolder = Path.Combine(_hostingEnvironment.WebRootPath, "Assets");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    var filePath = Path.Combine(uploadsfolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ProjectId = model.ProjectId,
                    PhotoPath = uniqueFileName ?? "deafaultphoto.jfif"
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (_userManager.Users.Count() > 1)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "CompanyManager");
                }
                if (!result.Succeeded)
                {
                    return RedirectToAction("HomePage", "Entry");
                }
                return RedirectToAction("Register");
            }

            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("HomePage", "Entry");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }


    }
}
