using message_notification_with_signalR.Models;
using System.Threading.Tasks;

namespace message_notification_with_signalR.Hub
{
   public interface IHubClient
    {

        Task BroadcastMessage();
        Task broadcastNotification(NotificationMessageModel data);
    }
}
