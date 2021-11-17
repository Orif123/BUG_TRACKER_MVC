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
        public async Task Join(string bugid)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, bugid);
        }
        public async Task SendMessageToGroup(string groupName, string user, string message)
        {
            await Clients.Group(groupName).SendAsync("NewBugReceived", user , message);
        }
        public Task Disconnect(string bugid)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, bugid);
        }
    }
}