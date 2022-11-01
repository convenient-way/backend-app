﻿using GeoCoordinatePortable;
using Newtonsoft.Json.Linq;
using unitofwork_core.Model.Mapbox;

namespace unitofwork_core.Service.MapboxService
{
    public interface IMapboxService
    {
        Task<JObject> DirectionApi(DirectionApiModel model);
        Task<JObject> SearchApi(string search);
        Task<ResponsePolyLineModel> GetPolyLine(DirectionApiModel model);
        Task<PolyLineModel> GetPolyLine(GeoCoordinate From, GeoCoordinate To);
    }
}
