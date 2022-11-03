namespace unitofwork_core.Entities
{
    public class ShipperRouting : BaseEntity
    {
        public string StartAddress { get; set; } = string.Empty;
        public double StartLongitude { get; set; }
        public double StartLatitude { get; set; }
        public string DestinationAddress { get; set; } = string.Empty;
        public double DestinationLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public bool IsActive { get; set; } = false;

        #region
        public Guid ShipperId { get; set; }
        public Shipper? Shipper { get; set; }
        #endregion

    }
}
