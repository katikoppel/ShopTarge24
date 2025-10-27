using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Models.ChuckNorris;

namespace ShopTARge24.Controllers
{
    public class ChuckNorrisController : Controller
    {
        private readonly IChuckNorrisServices _chuckNorrisServices;

        public ChuckNorrisController(IChuckNorrisServices chuckNorrisServices)
        {
            _chuckNorrisServices = chuckNorrisServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dto = new ChuckNorrisJokeResultDto();

            dto = await _chuckNorrisServices.ChuckNorrisRandomJoke(dto);

            var vm = new ChuckNorrisViewModel
            {
                Id = dto.Id,
                Url = dto.Url,
                IconUrl = dto.IconUrl,
                Value = dto.Value
            };

            return View(vm);
        }
    }
}
