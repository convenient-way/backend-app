using System.Diagnostics.Contracts;
using ProductEntity = unitofwork_core.Entities.Product;

namespace unitofwork_core.Model.ProductModel
{
    public class CreateProductModel
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public string Description { get; set; } = string.Empty;

        public ProductEntity ConvertToEntity()
        {
            ProductEntity pro = new ProductEntity();
            pro.Name = this.Name;
            pro.Price = this.Price;
            pro.Description = this.Description;
            return pro;
        }
    }
}