using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Nancy.Json;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.ApplicationServices.Services
{
    public class ChuckNorrisServices : IChuckNorrisServices
    {
        public async Task<ChuckNorrisJokeResultDto> ChuckNorrisRandomJoke(ChuckNorrisJokeResultDto dto)
        {
            const string url = "https://api.chucknorris.io/jokes/random";

            string json;
            using (var client = new WebClient())
            {
                json = await client.DownloadStringTaskAsync(url);
            }

            var serializer = new JavaScriptSerializer();
            var joke = serializer.Deserialize<ChuckNorrisJokeResultDto>(json);

            dto.Id = joke.Id;
            dto.Url = joke.Url;
            dto.IconUrl = joke.IconUrl;
            dto.Value = joke.Value;

            return dto;
        }
    }
}
