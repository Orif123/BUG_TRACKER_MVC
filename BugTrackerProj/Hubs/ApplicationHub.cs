using BugTrackerProj.ViewModels;
using BugTrackerProject.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BugTrackerProj
{
    public class ApplicationHub: Hub
    {
        public async Task NewBugReceived(string user, string message)
        {
           await Clients.All.SendAsync("NewBugReceived", user, message);
        }
    }
}