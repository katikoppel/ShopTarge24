using Newtonsoft.Json;

namespace ShopTARge24.Core.Dto.OpenWeatherDto
{
    public class OpenWeatherLocationDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
