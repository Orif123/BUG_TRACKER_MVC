using BugTrackerProj.Data;
using BugTrackerProj.ViewModels;
using BugTrackerProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BugTrackerProj.Service
{
    public class BugService : IBugService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly ApplicationDbContext _context;
        public BugService(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _httpContext = contextAccessor;
            _context = context;
        }
        public MainPageViewModel GetAllBugs(string searchtext = "", string userid = "")
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userid);
            var project = _context.Projects.SingleOrDefault(p => p.ProjectId == user.ProjectId);
            MainPageViewModel mp = new MainPageViewModel();
            if (searchtext == "" || searchtext == null)
            {
                
                mp.Bugs = _context.Bugs.Where(b => b.ProjectId == user.ProjectId);
                return mp;
            }
            mp.Bugs = _context.Bugs.Where(p => p.User.FirstName.Contains(searchtext) ||
                             p.BugId.Contains(searchtext) ||
                             p.Category.CtaegoryName.Contains(searchtext) ||
                             p.Description.Contains(searchtext) &&
                             p.ProjectId == project.ProjectId);
            return mp;
        }

        public List<SelectListItem> GetCategories(string id)
        {
            var categolist = _context.Categories.ToList();
            var selectlist = new List<SelectListItem>();
            foreach (var item in categolist)
            {
                if (item.ProjectId == id)
                {
                    selectlist.Add(new SelectListItem { Text = item.CtaegoryName, Value = item.CategoryId });
                }
            }
            return selectlist;

        }

        public List<SelectListItem> GetProjects()
        {
            var projlist = _context.Projects.ToList();
            var selectlist = new List<SelectListItem>();
            foreach (var item in projlist)
            {
                    selectlist.Add(new SelectListItem { Text = item.ProjectName, Value = item.ProjectId });
            }
            return selectlist;
        }
        public List<Project> GetRealProjects()
        {
            var list = _context.Projects.ToList();
            return list;
        }
        public List<ApplicationUser> GetRealUsers()
        {
            var list = _context.Users.ToList();
            return list;
        }

        public List<string> GetUsers()
        {
            var list = _context.Users.Select(list => list.Id).ToList();
            return list;
        }
        public void NewBug(Bug bug)
        {
            _context.Bugs.Add(bug);
            _context.SaveChanges();
        }




    }
}

