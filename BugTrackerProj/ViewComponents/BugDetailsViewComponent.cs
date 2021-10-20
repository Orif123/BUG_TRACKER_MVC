using BugTrackerProj.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.ViewComponents
{
    public class BugDetailsViewComponent: ViewComponent
    {
        private readonly IBugService _bugService;
        public BugDetailsViewComponent(IBugService bugService)
        {
            _bugService = bugService;
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }


    }
}
