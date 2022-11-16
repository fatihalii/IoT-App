using HefaWebhook.Models;
using HefaWebhook.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using System.Diagnostics;
using System.Text.Json;

namespace HefaWebhook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<SignaIrServer> _hubContext;
        private readonly SignalsService _signalsService;
        string? models = "";

        public HomeController(ILogger<HomeController> logger, IHubContext<SignaIrServer> hubContext, SignalsService signalsService)
        {
            _logger = logger;
            _hubContext = hubContext;
            _signalsService = signalsService;
        }

        [HttpGet]
        public IActionResult Index()
        { 
            GetData();
            return View();
        }

        [HttpGet]
        public IActionResult GetData()
        {
            models = _signalsService.GetAsync().Result.ToJson();
            return Json(models);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] JsonElement json) 
        {
            var rawSignal = JsonSerializer.Deserialize<RawSignal>(json.GetRawText()); 
            var signal = new Signal() 
            {
                Co = rawSignal.uplink_message.decoded_payload.analog_in_1,
                Co2 = rawSignal.uplink_message.decoded_payload.analog_in_2,
                So2 = rawSignal.uplink_message.decoded_payload.analog_in_3,
                No2 = rawSignal.uplink_message.decoded_payload.analog_in_4,
                O3 = rawSignal.uplink_message.decoded_payload.analog_in_5,
                Ch4 = rawSignal.uplink_message.decoded_payload.analog_in_6,
                Pm25 = rawSignal.uplink_message.decoded_payload.analog_in_7,
                Pm10 = rawSignal.uplink_message.decoded_payload.analog_in_8,
                Humidity = rawSignal.uplink_message.decoded_payload.relative_humidity_9,
                Temperature = rawSignal.uplink_message.decoded_payload.temperature_10,
                Time = DateTime.Now
            };
            await _signalsService.CreateAsync(signal);

            await _hubContext.Clients.All.SendAsync("LoadProducts");

            return Json(json);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}