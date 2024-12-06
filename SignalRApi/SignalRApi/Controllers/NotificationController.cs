using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRApi.SignalR;


namespace SignalRApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {

        private readonly IHubContext<ChatHub> _hubContext;

        public NotificationController(IHubContext<ChatHub> chatHub)
        {
            _hubContext = chatHub;
        }

        [HttpPost("sendNotification")]
        public async Task<IActionResult> SendNotification([FromBody] Notification notification)
        {
            await _hubContext.Clients.All.SendAsync("UpdateAllAsync", notification.User, notification.Message);
            return Ok(new { Message = "Notification sent successfully" });
        }
    }
    public class Notification
    {
        public string User { get; set; }
        public string Message { get; set; }
    }
}

