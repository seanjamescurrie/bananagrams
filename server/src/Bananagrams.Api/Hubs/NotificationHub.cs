using Bananagrams.Api.Hubs.Clients;
using Bananagrams.Api.ViewModels.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace Bananagrams.Api.Hubs;

public class NotificationHub : Hub
{
    public async Task SendNotification()
    {
        await Clients.All.SendAsync("NotificationCount");
    }
}