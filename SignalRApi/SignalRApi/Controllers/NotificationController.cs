using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRApi.HubConfig;
using SignalRApi.TimerFeatures;


namespace SignalRApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {

        private readonly IHubContext<ChatHub> _hubContext;
        private readonly TimerManager _timer;

        public NotificationController(IHubContext<ChatHub> chatHub
            , TimerManager timer)
        {
            _hubContext = chatHub;
            _timer = timer;
        }
        [HttpGet]
        public IActionResult Get()
        {
            if (!_timer.IsTimerStarted)
                _timer.PrepareTimer(async () => await _hubContext.Clients.All.SendAsync("UpdateAllAsync1", "API", "DEMO 00"));

            return Ok(new { Message = "Request Completed" });
        }

        [HttpPost("sendNotification")]
        public async Task<IActionResult> SendNotification([FromBody] Notification notification)
        {
            await _hubContext.Clients.All.SendAsync("UpdateAllAsync1", notification.User, notification.Message);
            return Ok(new { Message = "Notification sent successfully" });
        }
    }
    public class Notification
    {
        public string User { get; set; }
        public string Message { get; set; }
    }
}

