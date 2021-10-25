using BugTrackerProj.Data;
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
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IBugService bugService, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _bugService = bugService;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            ViewBag.ProjectId = _bugService.GetProjects().ToList();
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
                await _userManager.AddToRoleAsync(user, "User");
                if (!result.Succeeded)
                {
                    return View(model);
                }
                return RedirectToAction("Index", "Home");
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
                    if (_signInManager.IsSignedIn(User))
                    {
                        if (User.IsInRole("CompanyManager"))
                        {
                            return RedirectToAction("Index", "AdminRole");
                        }
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

                }
                return View(user);
            }
            return View(user);
        }


    }
}
