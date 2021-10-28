using BugTrackerProj.ViewModels;
using BugTrackerProject.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BugTrackerProj
{
    public class ApplicationHub: Hub
    {
        public async Task NewBugReceived(BugCommentDetailsViewModel model)
        {
           await Clients.All.SendAsync("NewBugReceived", model.BugId, model.CommentText);
        }
    }
}