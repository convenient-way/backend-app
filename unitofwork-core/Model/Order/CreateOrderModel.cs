using System.ComponentModel;
using OrderEntity = unitofwork_core.Entities.Order;
using OrderRoutingEntity = unitofwork_core.Entities.OrderRouting;
using unitofwork_core.Model.OrderRouting;

namespace unitofwork_core.Model.Order
{
    public class CreateOrderModel
    {
        public int NumberPackage { get; set; }
        public double StartLongitude { get; set; }
        public double StartLatitude { get; set; }
        public double Distance { get; set; }
        public double Volume { get; set; }
        public double Weight { get; set; }
        public Guid ShopId { get; set; }
        public List<CreateOrderRoutingModel> OrderRoutings { get; set; } = new List<CreateOrderRoutingModel>();

        public OrderEntity ConvertToEntity()
        {
            OrderEntity order = new OrderEntity();
            order.NumberPackage = this.NumberPackage;
            order.StartLongitude = this.StartLongitude;
            order.StartLatitude = this.StartLatitude;
            order.Distance = this.Distance;
            order.Volume = this.Volume;
            order.Weight = this.Weight;
            order.ShopId = this.ShopId;

            List<OrderRoutingEntity> orderRoutings = new List<OrderRoutingEntity>();
            int orderRoutingCount = this.OrderRoutings.Count;
            for (int i = 0; i < orderRoutingCount; i++)
            {
                OrderRoutingEntity orderRouteEn = this.OrderRoutings[i].ConvertToEntity();
            }
            order.OrderRoutings = orderRoutings;
            return order;
        }

    }
}