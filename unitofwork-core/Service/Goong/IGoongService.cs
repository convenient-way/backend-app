using Newtonsoft.Json.Linq;

namespace unitofwork_core.Service.Goong
{
    public interface IGoongService
    {
        Task<JObject> SearchApi(string search, double longitude, double latitude);

    }
}
