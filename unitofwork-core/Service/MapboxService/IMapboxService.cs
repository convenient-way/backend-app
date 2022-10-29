using Newtonsoft.Json.Linq;
using unitofwork_core.Model.Mapbox;

namespace unitofwork_core.Service.MapboxService
{
    public interface IMapboxService
    {
        Task<JObject> DirectionApi(DirectionApiModel model);
        Task<JObject> SearchApi(string search);
    }
}
