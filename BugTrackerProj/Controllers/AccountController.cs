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
    public class AccountController : Controller
    {
        private readonly IBugService _bugService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IBugService bugService)
        {
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
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ProjectId = model.ProjectId
                };
                var result = await _userManager.CreateAsync(user, model.Password);
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
                        return RedirectToAction("Index", "Home", _userManager.GetUserId(User));
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

                }
                return View(user);
            }
            return View(user);
        }


    }
}
