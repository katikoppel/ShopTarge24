namespace ShopTARge24.Models.OpenWeathers
{
    public class OpenWeatherViewModel
    {
        public string CityName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public double WindSpeed { get; set; }
        public int WindDegrees { get; set; }
        public double WindGust { get; set; }
        public double Rain1h { get; set; }
        public int Cloudiness { get; set; }
        public int Visibility { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
        public int Timezone { get; set; }

    }
}
