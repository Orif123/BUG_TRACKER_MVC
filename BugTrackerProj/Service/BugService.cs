using BugTrackerProj.Data;
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
        public IEnumerable<Bug> GetAllBugs()
        {
            var list = _context.Bugs.FromSqlRaw("select * from bugs");
            return list;
        }
    }
}
