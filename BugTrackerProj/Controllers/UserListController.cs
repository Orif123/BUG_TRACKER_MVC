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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        public UserListController(IBugService bugService, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _bugService = bugService;
            _userManager = userManager;
        }
        public IActionResult Index(string projectid)
        {
            if (User.IsInRole("CompanyManager"))
            {
                return View(_bugService.GetRealUsers());
            }
            var userid = _userManager.GetUserId(User);
            var user = _bugService.GetRealUsers().SingleOrDefault(u => u.Id == userid);
            projectid = user.ProjectId;
            return View(_bugService.GetUserByProject(projectid));
        }
        public IActionResult UpdateUser(string id)
        {
            ViewBag.ProjectId = _bugService.GetProjects().ToList();
            return View(_bugService.FindUserById(id));
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
            var user = _bugService.GetUserById(id);

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
