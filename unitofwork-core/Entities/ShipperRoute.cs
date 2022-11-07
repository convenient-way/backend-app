using unitofwork_core.Model.ShipperRouteModel;

namespace unitofwork_core.Entities
{
    public class ShipperRoute : BaseEntity
    {
        public string FromAddress { get; set; } = string.Empty;
        public double FromLongitude { get; set; }
        public double FromLatitude { get; set; }
        public string ToAddress { get; set; } = string.Empty;
        public double ToLongitude { get; set; }
        public double ToLatitude { get; set; }
        public bool IsActive { get; set; } = false;
        public Guid ShipperId { get; set; }
        public Shipper? Shipper { get; set; }

        public ResponseShipperRouteModel ToResponseModel() {
            ResponseShipperRouteModel model = new ResponseShipperRouteModel();
            model.FromAddress = this.FromAddress;
            model.FromLongitude = this.FromLongitude;
            model.FromLatitude = this.FromLatitude;
            model.ToAddress = this.ToAddress;
            model.ToLongitude = this.ToLongitude;
            model.ToLatitude = this.ToLatitude;
            model.IsActive = this.IsActive;
            model.ShipperId = this.ShipperId;
            return model;
        }
    }
}
