using unitofwork_core.Entities;
using unitofwork_core.Model.OrderRouting;

namespace unitofwork_core.Model.Product
{
    public class ResponseProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid OrderRoutingId { get; set; }
    }
}