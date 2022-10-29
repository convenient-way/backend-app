using unitofwork_core.Model.Order;
using unitofwork_core.Model.OrderRouting;
using unitofwork_core.Model.Shop;

namespace unitofwork_core.Entities
{
    public class Order : BaseEntity
    {
        public int NumberPackage { get; set; }
        public double StartLongitude { get; set; }
        public double StartLatitude { get; set; }
        public double Distance { get; set; }
        public double Volume { get; set; }
        public double Weight { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        #region Relationship
        public Guid ShopId { get; set; }
        public Shop? Shop { get; set; }

        public Guid? ShipperId { get; set; }
        public Shipper? Shipper { get; set; }

        public IList<Transaction> Transactions { get; set; }
        public IList<HistoryOrder> HistoryOrders { get; set; }
        public IList<OrderRouting> OrderRoutings { get; set; }
        #endregion
        public Order()
        {
            Transactions = new List<Transaction>();
            HistoryOrders = new List<HistoryOrder>();
            OrderRoutings = new List<OrderRouting>();

        }

        public ResponseOrderModel ToResponseModel()
        {
            ResponseOrderModel model = new ResponseOrderModel();
            model.Id = this.Id;
            model.NumberPackage = this.NumberPackage;
            model.StartLongitude = this.StartLongitude;
            model.StartLatitude = this.StartLatitude;
            model.Distance = this.Distance;
            model.Volume = this.Volume;
            model.Weight = this.Weight;
            model.CreatedAt = this.CreatedAt;
            model.ModifiedAt = this.ModifiedAt;
            model.ShopId = this.ShopId;
            model.Shop = this.Shop != null ? this.Shop.ToResponseModel() : null;
            model.ShipperId = this.ShipperId;
            model.Shipper = this.Shipper != null ? this.Shipper.ToResponseModel() : null;

            if (this.OrderRoutings.Count > 0)
            {
                model.OrderRoutings = new List<ResponseOrderRoutingModel>();
                int count = this.OrderRoutings.Count;
                for (int i = 0; i < count; i++)
                {
                    model.OrderRoutings.Add(this.OrderRoutings[i].ToResponseModel());
                }
            }
            return model;
        }

    }
}
