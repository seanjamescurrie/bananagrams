using Bananagrams.Api.ViewModels.Notifications;

namespace Bananagrams.Api.Hubs.Clients;

public interface INotificationClient
{
    Task NotificationCount();

    // Task ReceiveNotification(NotificationViewModel model);
}