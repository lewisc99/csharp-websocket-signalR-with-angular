using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace message_notification_with_signalR.Models
{
    public class NotificationMessageModel
    {
        public string Name { get; set; }
        public string Message { get; set; }

        public NotificationMessageModel() { }
    }
}
