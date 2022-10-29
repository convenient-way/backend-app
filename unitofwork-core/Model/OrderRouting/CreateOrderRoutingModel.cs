using unitofwork_core.Model.Product;
using OrderRoutingEntity = unitofwork_core.Entities.OrderRouting;
using ProductEntity = unitofwork_core.Entities.Product;
namespace unitofwork_core.Model.OrderRouting
{
    public class CreateOrderRoutingModel
    {
        public int RoutingIndex { get; set; }
        public double ToLongitude { get; set; }
        public double ToLatitude { get; set; }
        public double Distance { get; set; }
        public string Address { get; set; } = string.Empty;
        // set default status with waiting
        public List<CreateProductModel> Products { get; set; } = new List<CreateProductModel>();

        public OrderRoutingEntity ConvertToEntity()
        {
            OrderRoutingEntity orderRoute = new OrderRoutingEntity();
            orderRoute.RoutingIndex = this.RoutingIndex;
            orderRoute.ToLongitude = this.ToLongitude;
            orderRoute.ToLatitude = this.ToLatitude;
            orderRoute.Distance = this.Distance;
            orderRoute.Address = this.Address;

            List<ProductEntity> products = new List<ProductEntity>();
            int productCount = this.Products.Count;
            for (int i = 0; i < productCount; i++)
            {
                ProductEntity productEn = this.Products[i].ConvertToEntity();
                products.Add(productEn);
            }
            orderRoute.Products = products;

            return orderRoute;
        }
    }
}