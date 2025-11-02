using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.Dto.OpenWeatherDto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Models.OpenWeathers;

namespace ShopTARge24.Controllers
{
    public class OpenWeathersController : Controller
    {
        private readonly IOpenWeatherServices _openWeatherServices;

        public OpenWeathersController
            (
                IOpenWeatherServices openWeatherServices
            )
        {
            _openWeatherServices = openWeatherServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchCity(OpenWeatherSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "OpenWeathers", new { city = model.CityName });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> City(string city)
        {
            OpenWeatherResultDto dto = new();
            dto.CityName = city;

            dto = await _openWeatherServices.OpenWeatherResult(dto);

            OpenWeatherViewModel vm = new();

            vm.CityName = dto.CityName;
            vm.Country = dto.Country;
            vm.Latitude = dto.Latitude;
            vm.Longitude = dto.Longitude;

            vm.Main = dto.Main;
            vm.Description = dto.Description;
            vm.Icon = dto.Icon;

            vm.Temperature = dto.Temperature;
            vm.FeelsLike = dto.FeelsLike;
            vm.TempMin = dto.TempMin;
            vm.TempMax = dto.TempMax;
            vm.Pressure = dto.Pressure;
            vm.Humidity = dto.Humidity;

            vm.WindSpeed = dto.WindSpeed;
            vm.WindDegrees = dto.WindDegrees;
            vm.WindGust = dto.WindGust;

            vm.Cloudiness = dto.Cloudiness;
            vm.Visibility = dto.Visibility;
            vm.Sunrise = dto.Sunrise;
            vm.Sunset = dto.Sunset;
            vm.Timezone = dto.Timezone;

            return View(vm);
        }
    }
}
