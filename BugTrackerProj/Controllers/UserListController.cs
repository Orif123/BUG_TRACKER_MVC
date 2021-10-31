using BugTrackerProj.Data;
using BugTrackerProj.Models;
using BugTrackerProj.Service;
using BugTrackerProj.ViewModels;
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
    public class UserListController : Controller
    {
        private readonly IBugService _bugService;
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        public UserListController(IBugService bugService, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostEnvironment, IUserService userService, IProjectService projectService)
        {
            _hostEnvironment = hostEnvironment;
            _bugService = bugService;
            _userManager = userManager;
            _userService = userService;
            _projectService = projectService;
        }
        public IActionResult Index(string projectid)
        {
            if (User.IsInRole("CompanyManager"))
            {
                return View(_userService.GetRealUsers());
            }
            var userid = _userManager.GetUserId(User);
            var user = _userService.GetRealUsers().SingleOrDefault(u => u.Id == userid);
            projectid = user.ProjectId;
            return View(_userService.GetUserByProject(projectid));
        }
        public IActionResult UpdateUser(string id)
        {
            ViewBag.ProjectId = _projectService.GetProjects().ToList();
            return View(_userService.FindUserById(id));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserViewModel model, string id)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                var uploadsfolder = Path.Combine(_hostEnvironment.WebRootPath, "Assets");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                var filePath = Path.Combine(uploadsfolder, uniqueFileName);
                model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            var user = _userService.GetUserById(id);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhotoPath = uniqueFileName ?? "deafaultphoto.jfif";
            user.ProjectId = model.ProjectId;


            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("index", "UserList");
            }
            return View(user);
        }
        public IActionResult UserDetails(string id)
        {
            return View(_bugService.GetBugsByUserId(id));
        }
    }
}
