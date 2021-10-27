using BugTrackerProject.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BugTrackerProj
{
    public class ApplicationHub: Hub
    {
        public async Task NewBugReceived(Bug bug)
        {
           await Clients.All.SendAsync("NewBugReceived", bug);
        }
    }
}