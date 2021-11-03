using BugTrackerProj.Models;
using BugTrackerProj.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Service
{
    public interface IUserService
    {
        List<SelectListItem> GetUserRoles();
        List<SelectListItem> GetUserNames();
        List<ApplicationUser> GetUserByProject(string projectid, string searchtext="");
        List<ApplicationUser> GetRealUsers(string searchtext="");
        UpdateUserViewModel FindUserById(string id);
        ApplicationUser GetUserById(string id);
    }
}
