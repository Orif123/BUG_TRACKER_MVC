using BugTrackerProj.Data;
using BugTrackerProj.Models;
using BugTrackerProj.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Service
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public UpdateUserViewModel FindUserById(string id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            var model = new UpdateUserViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProjectId = user.ProjectId,
            };
            return model;
        }
        public ApplicationUser GetUserById(string id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            return user;
        }
        public List<ApplicationUser> GetUserByProject(string projectid, string searchtxt = "")
        {
            var list = new List<ApplicationUser>();
            if (searchtxt == null || searchtxt == "")
            {
                list = _userManager.Users.Where(u => u.ProjectId == projectid || u.ProjectId == "NoProject").Include(u => u.Project).ToList();
                return list;
            }
            list = _userManager.Users.Where(u => u.ProjectId == projectid || u.ProjectId == "NoProject").Where(u => u.UserName.Contains(searchtxt) || u.FirstName.Contains(searchtxt) || u.LastName.Contains(searchtxt)).Include(u => u.Project).ToList();
            return list;
        }
        public List<ApplicationUser> GetRealUsers(string searchtext = "")
        {
            var list = new List<ApplicationUser>();
            if (searchtext == null || searchtext == "")
            {
                list = _context.Users.Include(p => p.Project).ToList();
                return list;
            }
            list = _context.Users.Where(u => u.UserName.Contains(searchtext) || u.FirstName.Contains(searchtext) || u.LastName.Contains(searchtext)).Include(p => p.Project).ToList();
            return list;
        }
        public List<SelectListItem> GetUserNames()
        {
            var roleList = _context.Users.ToList();
            var selectedlist = new List<SelectListItem>();
            foreach (var item in roleList)
            {
                selectedlist.Add(new SelectListItem { Value = item.UserName, Text = item.UserName });
            }
            return selectedlist;
        }
        public List<SelectListItem> GetUserRoles()
        {
            var roleList = _context.Roles.ToList();
            var selectedlist = new List<SelectListItem>();
            foreach (var item in roleList)
            {
                selectedlist.Add(new SelectListItem { Value = item.Name, Text = item.Name });
            }
            return selectedlist;
        }
    }
}
