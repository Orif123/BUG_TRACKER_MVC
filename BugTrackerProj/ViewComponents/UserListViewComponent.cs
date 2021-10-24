using BugTrackerProj.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.ViewComponents
{
    public class UserList: ViewComponent
    {
        private readonly IBugService _bugService;
        public UserList(IBugService bugService)
        {
            _bugService = bugService;
        }
        public Task <IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult<IViewComponentResult>(View("Deafault" ,_bugService.GetRealUsers()));
        }
    }
}
