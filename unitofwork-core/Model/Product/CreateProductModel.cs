using System.Diagnostics.Contracts;
using ProductEntity = unitofwork_core.Entities.Product;

namespace unitofwork_core.Model.Product
{
    public class CreateProductModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ProductEntity ConvertToEntity()
        {
            ProductEntity pro = new ProductEntity();
            pro.Name = this.Name;
            pro.Description = this.Description;
            return pro;
        }
    }
}