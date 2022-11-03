using GeoCoordinatePortable;
using Newtonsoft.Json.Linq;
using unitofwork_core.Model.MapboxModel;

namespace unitofwork_core.Service.MapboxService
{
    public interface IMapboxService
    {
        Task<JObject> DirectionApi(DirectionApiModel model);
        Task<JObject> SearchApi(string search);
        Task<List<ResponsePolyLineModel>> GetPolyLine(DirectionApiModel model);
        Task<PolyLineModel> GetPolyLineModel(GeoCoordinate From, GeoCoordinate To);
    }
}
