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
            await Clients.Group(bugid).SendAsync("Send", $"{Context.ConnectionId} has joined the group {bugid}.");

        }
        public Task Disconnect(string bugid)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, bugid);
        }
    }
}