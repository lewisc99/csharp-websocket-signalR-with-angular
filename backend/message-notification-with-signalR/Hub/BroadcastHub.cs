using message_notification_with_signalR.Hub;
using message_notification_with_signalR.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;

namespace message_notification_with_signalR.Hub
{
    public class BroadcastHub: Hub<IHubClient>
    {
        private readonly ILogger<BroadcastHub> _logger;

        public BroadcastHub(ILogger<BroadcastHub> logger) =>
            _logger = logger;

    

        public string getConnectionId() => Context.ConnectionId;


       public void SendMessage(NotificationMessageModel message)
        {

            _logger.LogInformation("Result");
            _logger.LogInformation(" User: " + message.Name + "Message: " + message.Message);

            Clients.AllExcept(Context.ConnectionId).broadcastNotification(message);


        }

    }
}
