using System.ComponentModel.DataAnnotations;
using unitofwork_core.Constant.Package;
using unitofwork_core.Model.ProductModel;
using PackageEntity = unitofwork_core.Entities.Package;

namespace unitofwork_core.Model.PackageModel
{
    public class CreatePackageModel
    {
        public string StartAddress { get; set; } = string.Empty;
        public double StartLongitude { get; set; }
        public double StartLatitude { get; set; }
        public string DestinationAddress { get; set; } = string.Empty;
        public double DestinationLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public string ReceiverName { get; set; } = string.Empty;
        [Phone]
        public string ReceiverPhone { get; set; } = string.Empty;
        public double Distance { get; set; }
        public double Volume { get; set; }
        public double Weight { get; set; }
        public decimal PriceShip { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public Guid ShopId { get; set; }
        public List<CreateProductModel> Products { get; set; } = new List<CreateProductModel>();

        public PackageEntity ConverToEntity() { 
             PackageEntity entity = new PackageEntity();
            entity.StartAddress = this.StartAddress;
            entity.StartLongitude = this.StartLongitude;
            entity.StartLatitude = this.StartLatitude;
            entity.DestinationAddress = this.DestinationAddress;
            entity.DestinationLongitude = this.DestinationLongitude;
            entity.DestinationLatitude = this.DestinationLatitude;
            entity.Distance = this.Distance;
            entity.Volume = this.Volume;
            entity.Weight = this.Weight;
            entity.ReceiverName = this.ReceiverName;
            entity.ReceiverPhone = this.ReceiverPhone;
            entity.Status = PackageStatus.WAITING;
            entity.PriceShip = this.PriceShip;
            entity.PhotoUrl = this.PhotoUrl;
            entity.Note = this.Note;
            entity.ShopId = this.ShopId;

            int productCount = this.Products.Count;
            for (int i = 0; i < productCount; i++)
            {
                entity.Products.Add(this.Products[i].ConvertToEntity());
            }

            return entity;
        }
    }

}
