using System.ComponentModel.DataAnnotations;
using unitofwork_core.Model.ProductModel;
using unitofwork_core.Model.ShipperModel;
using unitofwork_core.Model.ShopModel;

namespace unitofwork_core.Model.PackageModel
{
    public class ResponsePackageModel
    {
        public Guid Id { get; set; }
        public string StartAddress { get; set; } = string.Empty;
        public double StartLongitude { get; set; }
        public double StartLatitude { get; set; }
        public string DestinationAddress { get; set; } = string.Empty;
        public double DestinationLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public string ReceiverName { get; set; } = string.Empty;
        [Phone]
        public string ReceiverPhone { get; set; } = string.Empty;
        public double Distance { get; set; }
        public double Volume { get; set; }
        public double Weight { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal? PriceShip { get; set; } = null;
        public string PhotoUrl { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid ShopId { get; set; }
        public ResponseShopModel? Shop { get; set; }
        public Guid? ShipperId { get; set; }
        public ResponseShipperModel? Shipper { get; set; }
        public IList<ResponseProductModel> Products { get; set; } = new List<ResponseProductModel>();
    }
}
