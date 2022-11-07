using unitofwork_core.Model.PackageModel;

namespace unitofwork_core.Entities
{
    public class Package : BaseEntity
    {
        public string StartAddress { get; set; } = string.Empty;
        public double StartLongitude { get; set; }
        public double StartLatitude { get; set; }
        public string DestinationAddress { get; set; } = string.Empty;
        public double DestinationLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public double Distance { get; set; }
        public string ReceiverName { get; set; } = string.Empty;
        public string ReceiverPhone { get; set; } = string.Empty;
        public double Volume { get; set; }
        public double Weight { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal PriceShip { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        #region Relationship
        public Guid ShopId { get; set; }
        public Shop? Shop { get; set; }
        public Guid? ShipperId { get; set; }
        public Shipper? Shipper { get; set; }

        public IList<Transaction> Transactions { get; set; }
        public IList<HistoryPackage> HistoryPackages { get; set; }
        public IList<Product> Products { get; set; }
        #endregion

        public Package()
        {
            Transactions = new List<Transaction>();
            HistoryPackages = new List<HistoryPackage>();
            Products = new List<Product>();
        }

        public ResponsePackageModel ToResponseModel()
        {
            ResponsePackageModel model = new ResponsePackageModel();
            model.Id = this.Id;
            model.StartAddress = this.StartAddress;
            model.StartLongitude = this.StartLongitude;
            model.StartLatitude = this.StartLatitude;
            model.DestinationAddress = this.DestinationAddress;
            model.DestinationLongitude = this.DestinationLongitude;
            model.DestinationLatitude = this.DestinationLatitude;
            model.ReceiverName = this.ReceiverName;
            model.ReceiverPhone = this.ReceiverPhone;
            model.Distance = this.Distance;
            model.Volume = this.Volume;
            model.Weight = this.Weight;
            model.Status = this.Status;
            model.PriceShip = this.PriceShip;
            model.PhotoUrl = this.PhotoUrl;
            model.Note = this.Note;
            model.CreatedAt = this.CreatedAt;
            model.ModifiedAt = this.ModifiedAt;
            model.ShopId = this.ShopId;
            model.ShipperId = this.ShipperId;
            model.Shipper = this.Shipper != null ? this.Shipper.ToResponseModel() : null;
            model.Shop = this.Shop != null ? this.Shop.ToResponseModel() : null;

            int countProduct = this.Products.Count;
            for (int i = 0; i < countProduct; i++)
            {
                model.Products.Add(this.Products[i].ToResponseModel());
            }
            return model;
        }
    }
}
