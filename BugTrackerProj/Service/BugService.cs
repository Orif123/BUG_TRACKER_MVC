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
                mp.Bugs = _context.Bugs.ToList();
                return mp;
            }
            mp.Bugs = _context.Bugs.Where(p => p.User.FirstName.Contains(searchtext) ||
                             p.BugId.Contains(searchtext) ||
                             p.Category.CtaegoryName.Contains(searchtext) ||
                             p.Description.Contains(searchtext));
            return mp;
        }

        public List<string> GetCategories()
        {
            var list = _context.Categories.Select(list=>list.CtaegoryName).ToList();
            return list;
        }

        public List<string> GetProjects()
        {
            var list = _context.Projects.Select(list => list.ProjectName).ToList();
            return list;
        }

        public List<string> GetUsers()
        {
            var list = _context.Users.Select(list => list.UserName).ToList();
            return list;
        }

        public void NewBug(Bug bug)
        {
            _context.Bugs.Add(bug);
            _context.SaveChanges();
        }

        
    }
}

