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
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        #region Relationship
        public Guid ShopId { get; set; }
        public Shop? Shop { get; set; }

        public Guid ShipperId { get; set; }
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

    }
}
