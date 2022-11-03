using Newtonsoft.Json.Linq;

namespace unitofwork_core.Service.Goong
{
    public class GoongService : IGoongService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GoongService> _logger;

        public GoongService(IConfiguration configuration, ILogger<GoongService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<JObject> DetailPlaceApi(string placeId)
        {
            JObject result;
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_configuration["Goong:uriDetail"] + placeId)
            };
            _logger.LogInformation("Request goong uri: " + request.RequestUri);
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                result = JObject.Parse(body);
            }
            return result;
        }

        public async Task<JObject> SearchApi(string search, double longitude, double latitude)
        {
            JObject result;
            string endUri = $"&input={search}";
            if (longitude != 0 && latitude != 0) {
                endUri = endUri + $"&location={latitude},{longitude}";
            }

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_configuration["Goong:uriSearch"] + endUri)
            };
            _logger.LogInformation("Request goong uri: " + request.RequestUri);
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
