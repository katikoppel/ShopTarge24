using System.Globalization;
using System.Net.Http;
using Newtonsoft.Json;
using ShopTARge24.Core.Dto.OpenWeatherDto;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.ApplicationServices.Services
{
    public class OpenWeatherServices : IOpenWeatherServices
    {
        private readonly HttpClient _http = new();

        public async Task<OpenWeatherResultDto> OpenWeatherResult(OpenWeatherResultDto dto)
        {
            string apiKey = "243d54e3b87c90965272679d018e0825";

            var city = dto.CityName?.Trim() ?? "";
            var country = string.IsNullOrWhiteSpace(dto.Country) ? "EE" : dto.Country.Trim();
            var q = Uri.EscapeDataString($"{city},{country}");

            var geoUrl = $"https://api.openweathermap.org/geo/1.0/direct?q={q}&limit=1&appid={apiKey}";
            var geoJson = await _http.GetStringAsync(geoUrl);
            var geo = JsonConvert.DeserializeObject<List<OpenWeatherLocationDto>>(geoJson);

            dto.CityName = geo[0].Name;
            dto.Country = geo[0].Country;
            dto.Latitude = geo[0].Latitude;
            dto.Longitude = geo[0].Longitude;

            var lat = dto.Latitude.ToString(CultureInfo.InvariantCulture);
            var lon = dto.Longitude.ToString(CultureInfo.InvariantCulture);
            var weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";

            var weatherJson = await _http.GetStringAsync(weatherUrl);
            var w = JsonConvert.DeserializeObject<OpenWeatherCurrentRootDto>(weatherJson);

            dto.Temperature = w.Main?.Temperature ?? 0;
            dto.FeelsLike = w.Main?.FeelsLike ?? 0;
            dto.TempMin = w.Main?.TempMin ?? 0;
            dto.TempMax = w.Main?.TempMax ?? 0;
            dto.Humidity = w.Main?.Humidity ?? 0;
            dto.Pressure = w.Main?.Pressure ?? 0;

            dto.Main = w.Weather?.FirstOrDefault()?.Main ?? "";
            dto.Description = w.Weather?.FirstOrDefault()?.Description ?? "";
            dto.Icon = w.Weather?.FirstOrDefault()?.Icon ?? "";

            dto.WindSpeed = w.Wind?.Speed ?? 0;
            dto.WindDegrees = w.Wind?.Degrees ?? 0;
            dto.WindGust = w.Wind?.Gust ?? 0;

            dto.Cloudiness = w.Clouds?.All ?? 0;
            dto.Visibility = w.Visibility;
            dto.Rain1h = w.Rain?.OneHour ?? 0;

            dto.Timezone = w.Timezone;
            dto.Sunrise = w.Sys?.Sunrise ?? 0;
            dto.Sunset = w.Sys?.Sunset ?? 0;

            return dto;
        }
    }
}