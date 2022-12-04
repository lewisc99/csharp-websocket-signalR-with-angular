using message_notification_with_signalR.Hub;
using message_notification_with_signalR.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace message_notification_with_signalR.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]

    public class EmployeesController: ControllerBase
    {

        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;


        public EmployeesController(IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _hubContext = hubContext;
        }



        [HttpPost]
        public async Task<ActionResult> PostEmploye([FromBody] NotificationMessageModel message)
        {
            await _hubContext.Clients.All.broadcastNotification(message);


            return Ok(message);
        }
    }
}
