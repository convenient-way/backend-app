using unitofwork_core.Entities;

namespace unitofwork_core.Model.ShipperRouteModel
{
    public class RegisterShipperRouteModel
    {
        public string FromAddress { get; set; } = string.Empty;
        public double FromLongitude { get; set; }
        public double FromLatitude { get; set; }
        public string ToAddress { get; set; } = string.Empty;
        public double ToLongitude { get; set; }
        public double ToLatitude { get; set; }
        public Guid ShipperId { get; set; }

        public ShipperRoute ConvertToEntity() {
            ShipperRoute route = new ShipperRoute();
            route.FromAddress = FromAddress;
            route.FromLatitude = FromLatitude;
            route.FromLongitude = FromLongitude;
            route.ToAddress = ToAddress;
            route.ToLatitude = ToLatitude;
            route.ToLongitude = ToLongitude;
            route.IsActive = true;
            route.ShipperId = ShipperId;
            return route;
        }
    }
}
