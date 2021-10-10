using BugTrackerProj.Data;
using BugTrackerProj.ViewModels;
using BugTrackerProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Service
{
    public class BugService : IBugService
    {
        private readonly ApplicationDbContext _context;
        public BugService(ApplicationDbContext context)
        {
            _context = context;
        }
        public MainPageViewModel GetAllBugs(string searchtext = "")
        {
            MainPageViewModel mp = new MainPageViewModel();
            if (searchtext == "" || searchtext == null)
            {
                mp.Bugs = _context.Bugs.FromSqlRaw($"select * from bugs");
                return mp;
            }
            mp.Bugs = _context.Bugs.Where(p => p.User.FirstName.Contains(searchtext) ||
                             p.BugId.Contains(searchtext) ||
                             p.Category.CtaegoryName.Contains(searchtext) ||
                             p.Description.Contains(searchtext));
            return mp;
        }

        public void NewBug(Bug bug)
        {
            bug.Category = (Category)_context.Categories.Where(p => p.CategoryId == bug.CategoryId);
            bug.CategoryId = bug.Category.CategoryId;
            bug.Project = (Project)_context.Projects.Where(p => p.ProjectId == bug.ProjectId);
            bug.ProjectId = bug.Project.ProjectId;
            bug.User = (ApplicationUser)_context.Users.Where(p => p.Id == p.Id);
            bug.UserId = bug.User.Id;

        }
    }
}

