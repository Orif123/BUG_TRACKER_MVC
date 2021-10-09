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
        public MainPageViewModel GetAllBugs(string id)
        {
            MainPageViewModel mp = new MainPageViewModel();
            mp.Bugs = _context.Bugs.FromSqlRaw($"select * from bugs where bugs.projectid={id}");
            return mp;
        }
        public MainPageViewModel GetBugsByCategory(string id)
        {
            MainPageViewModel mp = new MainPageViewModel();
            mp.Category = (Category)_context.Categories.FromSqlRaw($"select * from categories where categoryid={id}");
            mp.Bugs = _context.Bugs.FromSqlRaw($"select * from bugs where bugs.categoryid={id}").ToList();
            return mp;
        }
        public MainPageViewModel GetBugsByUser(string id)
        {
            var mp = new MainPageViewModel();
            mp.Users = _context.Users.FromSqlRaw($"select * from user where userid={id}");
            mp.Bugs = _context.Bugs.FromSqlRaw($"select * from bugs where bug.use={id}").ToList();
            return mp;
        }


    }
}

