namespace unitofwork_core.Entities
{
    public class OrderRouting : BaseEntity
    {
        public int RoutingIndex { get; set; }
        public double ToLongitude { get; set; }
        public double ToLatitude { get; set; }
        public double Distance { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime ModifiedAt { get; set; }

        #region Relationship
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public IList<Product> Products { get; set; }
        #endregion
        public OrderRouting()
        {
            Products = new List<Product>();
        }
    }
}
