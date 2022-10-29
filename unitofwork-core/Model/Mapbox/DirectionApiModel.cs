namespace unitofwork_core.Model.Mapbox
{
    public class DirectionApiModel
    {
        public CoordinateApp From { get; set; } = new CoordinateApp();
        public CoordinateApp To { get; set; } = new CoordinateApp();
    }
}
