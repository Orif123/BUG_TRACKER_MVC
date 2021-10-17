using BugTrackerProj.Data;
using BugTrackerProj.ViewModels;
using BugTrackerProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Service
{
    public class ManagementService : IManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public ManagementService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IEnumerable<Bug> GetAllBugs()
        {
            var list = _context.Bugs.Include(p=>p.Category).Include(P=>P.User).ToList();
            return list;
        }
        public List<Bug> GetBugsByProject(string projectid)
        {
            var list = _context.Bugs.FromSqlRaw($"select * from bugs where bugs.projectid={projectid}").Include(p => p.Category).Include(P => P.User).ToList();
            return list;
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            var list = _context.Users.ToList();
            return list;
        }
    }
}
