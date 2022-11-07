namespace unitofwork_core.Model.ShipperRoute
{
    public class ResponseShipperRouteModel
    {
        public string FromAddress { get; set; } = string.Empty;
        public double FromLongitude { get; set; }
        public double FromLatitude { get; set; }
        public string ToAddress { get; set; } = string.Empty;
        public double ToLongitude { get; set; }
        public double ToLatitude { get; set; }
        public bool IsActive { get; set; } = false;
        public Guid ShipperId { get; set; }
    }
}
