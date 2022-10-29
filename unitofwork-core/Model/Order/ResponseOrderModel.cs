using unitofwork_core.Model.OrderRouting;
using unitofwork_core.Model.Shipper;
using unitofwork_core.Model.Shop;

namespace unitofwork_core.Model.Order
{
    public class ResponseOrderModel
    {
        public Guid Id { get; set; }
        public int NumberPackage { get; set; }
        public double StartLongitude { get; set; }
        public double StartLatitude { get; set; }
        public double Distance { get; set; }
        public double Volume { get; set; }
        public double Weight { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        #region Relationship
        public Guid ShopId { get; set; }
        public ResponseShopModel? Shop { get; set; }

        public Guid? ShipperId { get; set; }
        public ResponseShipperModel? Shipper { get; set; }

        public List<ResponseOrderRoutingModel>? OrderRoutings { get; set; }
        #endregion
    }
}