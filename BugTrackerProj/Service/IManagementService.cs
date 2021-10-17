using BugTrackerProj.Data;
using BugTrackerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Service
{
    public interface IManagementService
    {
        IEnumerable<Bug> GetAllBugs();
        List<Bug> GetBugsByProject(string projectid);
        IEnumerable<ApplicationUser> GetUsers();
    }
}
