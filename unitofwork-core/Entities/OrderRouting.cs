using unitofwork_core.Model.OrderRouting;
using unitofwork_core.Model.Product;

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

        public ResponseOrderRoutingModel ToResponseModel()
        {
            ResponseOrderRoutingModel model = new ResponseOrderRoutingModel();
            model.Id = this.Id;
            model.RoutingIndex = this.RoutingIndex;
            model.ToLongitude = this.ToLongitude;
            model.ToLatitude = this.ToLatitude;
            model.Distance = this.Distance;
            model.Address = this.Address;
            model.Status = this.Status;
            model.ModifiedAt = this.ModifiedAt;
            model.OrderId = this.OrderId;

            if (this.Products.Count > 0)
            {
                model.Products = new List<ResponseProductModel>();
                for (int i = 0; i < this.Products.Count; i++)
                {
                    model.Products.Add(this.Products[i].ToResponseModel());
                }
            }
            return model;
        }
    }
}
