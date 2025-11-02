using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShopTARge24.Core.Dto.OpenWeatherDto
{
    public class OpenWeatherCurrentRootDto
    {
        [JsonProperty("coord")]
        public CoordDto Coord { get; set; }

        [JsonProperty("weather")]
        public List<WeatherDto> Weather { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("main")]
        public MainDto Main { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("wind")]
        public WindDto Wind { get; set; }

        [JsonProperty("rain")]
        public RainDto Rain { get; set; }

        [JsonProperty("clouds")]
        public CloudsDto Clouds { get; set; }

        [JsonProperty("dt")]
        public long DateTimeUnix { get; set; }

        [JsonProperty("sys")]
        public SysDto Sys { get; set; }

        [JsonProperty("timezone")]
        public int Timezone { get; set; }

        [JsonProperty("id")]
        public int CityId { get; set; }

        [JsonProperty("name")]
        public string CityName { get; set; }

        [JsonProperty("cod")]
        public int Code { get; set; }
    }

    public class CoordDto
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }

    public class WeatherDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class MainDto
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("temp_min")]
        public double TempMin { get; set; }

        [JsonProperty("temp_max")]
        public double TempMax { get; set; }

        [JsonProperty("pressure")]
        public int Pressure { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("sea_level")]
        public int SeaLevel { get; set; }

        [JsonProperty("grnd_level")]
        public int GroundLevel { get; set; }
    }

    public class WindDto
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public int Degrees { get; set; }

        [JsonProperty("gust")]
        public double Gust { get; set; }
    }

    public class RainDto
    {
        [JsonProperty("1h")]
        public double OneHour { get; set; }
    }

    public class CloudsDto
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }

    public class SysDto
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }
    }
}
