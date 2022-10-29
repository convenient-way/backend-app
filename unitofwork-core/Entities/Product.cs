using unitofwork_core.Model.Product;

namespace unitofwork_core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        #region Relationship
        public Guid OrderRoutingId { get; set; }
        public OrderRouting? OrderRouting { get; set; }
        #endregion

        public ResponseProductModel ToResponseModel()
        {
            ResponseProductModel model = new ResponseProductModel();
            model.Id = this.Id;
            model.Name = this.Name;
            model.Description = this.Description;
            model.OrderRoutingId = this.OrderRoutingId;
            return model;
        }
    }
}
