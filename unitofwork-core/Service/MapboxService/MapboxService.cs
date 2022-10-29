using Newtonsoft.Json.Linq;
using unitofwork_core.Model.Mapbox;

namespace unitofwork_core.Service.MapboxService
{
    public class MapboxService : IMapboxService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MapboxService> _logger;
        public MapboxService(IConfiguration configuration, ILogger<MapboxService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<JObject> DirectionApi(DirectionApiModel model)
        {
            JObject result;
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_configuration["Mapbox:uriDirection"] + model.From.Longitude + "," + model.From.Latitude + ";" + model.To.Longitude + "," + model.To.Latitude + _configuration["Mapbox:endUriDirection"])
            };
            _logger.LogInformation("Request mapbox uri: " + request.RequestUri);
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                result = JObject.Parse(body);
            }
            return result;
        }

        public async Task<JObject> SearchApi(string search)
        {
            JObject result;
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_configuration["Mapbox:uriSearch"] + search + _configuration["Mapbox:endUriSearch"])
            };
            _logger.LogInformation("Request mapbox uri: " + request.RequestUri);
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                result = JObject.Parse(body);
            }
            return result;
        }
    }
}
