using BugTrackerProj.ViewModels;
using BugTrackerProject.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BugTrackerProj
{
    public class ApplicationHub: Hub
    {
        public async Task NewBug(string user, string message)
        {
           await Clients.All.SendAsync("NewBugReceived", user, message);
        }
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,groupName);
        }
       public async Task SendMessageToGroup(string groupName, string user, string message)
        {
            await Clients.Group(groupName).SendAsync("NewBugReceived", user, message);
        }
    }
}