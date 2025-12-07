using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ShopTARge24.Hubs; 


namespace ShopTARge24.Controllers
{
    public class SignalRController : Controller
    {
        private readonly IHubContext<DeathlyHallowsHub> _deathlyHallowsHub;

        public SignalRController(IHubContext<DeathlyHallowsHub> deathlyHallowsHub)
        {
            _deathlyHallowsHub = deathlyHallowsHub;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }

        public async Task<IActionResult> DeathlyHallows(string type)
        {
            if (SD.DeathlyHallowRace.ContainsKey(type))
            {
                SD.DeathlyHallowRace[type]++;
            }

            await _deathlyHallowsHub.Clients.All.SendAsync("updateDeathlyHallowCount",
                SD.DeathlyHallowRace[SD.Cloak],
                SD.DeathlyHallowRace[SD.Stone],
                SD.DeathlyHallowRace[SD.Wand]);

            return Accepted();
        }
    }
}
