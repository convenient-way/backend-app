using unitofwork_core.Model.Product;

namespace unitofwork_core.Model.OrderRouting
{
    public class ResponseOrderRoutingModel
    {
        public Guid Id { get; set; }
        public int RoutingIndex { get; set; }
        public double ToLongitude { get; set; }
        public double ToLatitude { get; set; }
        public double Distance { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime ModifiedAt { get; set; }
        public Guid OrderId { get; set; }
        public IList<ResponseProductModel>? Products { get; set; }
    }
}