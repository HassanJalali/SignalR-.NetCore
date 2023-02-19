using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRSample.Hubs;
using SignalRSample.Models;
using System.Diagnostics;

namespace SignalRSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<DeathlyHallowsHub> _hubContext;

        public HomeController(ILogger<HomeController> logger, IHubContext<DeathlyHallowsHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Notification()
        {
            return View();
        }
        public IActionResult DeathlyHallowRace()
        {
            return View();
        }
        public IActionResult HarryPotterHouse()
        {
            return View();
        }
        public IActionResult BasicChat()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> DeathlyHallows(string type)
        {
            if (StaticDetail.DealthyHallowRace.ContainsKey(type))
            {
                StaticDetail.DealthyHallowRace[type]++;
            }
            await _hubContext.Clients.All.SendAsync("UpdateDeathlyHallowsCount",
                StaticDetail.DealthyHallowRace[StaticDetail.Cloak],
                StaticDetail.DealthyHallowRace[StaticDetail.Stone],
                StaticDetail.DealthyHallowRace[StaticDetail.Wand]);

            return Accepted();
        }
    }
}