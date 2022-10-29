

using unitofwork_core.Model.Shipepr;
using unitofwork_core.Model.Shop;

namespace unitofwork_core.Entities.Extension
{
    public static class EntityExtension
    {
        public static ResponseShopModel ToResponseModel(this Shop shop)
        {

            ResponseShopModel model= new ResponseShopModel();
            model.Id = shop.Id;
            model.UserName = shop.UserName;
            model.DisplayName = shop.DisplayName;
            model.Status = shop.Status;
            model.PhoneNumber = shop.PhoneNumber;
            model.Email = shop.Email;
            model.PhotoUrl = shop.PhotoUrl;
            model.CreatedAt = shop.CreatedAt;
            model.ModifiedAt = shop.ModifiedAt;
            model.Longitude = shop.Longitude;
            model.Latitude = shop.Latitude;
            model.Address = shop.Address;
            return model;
        }

        public static ResponseShipeprModel ToResponseModel(this Shipper shipper) { 
            ResponseShipeprModel model = new ResponseShipeprModel();
            model.UserName = shipper.UserName;
            model.Email = shipper.Email;
            model.DisplayName = shipper.DisplayName;
            model.PhoneNumber = shipper.PhoneNumber;
            model.PhotoUrl = shipper.PhotoUrl;
            model.Status = shipper.Status;
            model.Address = shipper.Address;
            model.HomeLongitude = shipper.HomeLongitude;
            model.HomeLatitude = shipper.HomeLatitude;
            model.DestinationLongitude = shipper.DestinationLongitude;
            model.DestinationLatitude = shipper.DestinationLatitude;
            model.CreatedAt = shipper.CreatedAt;
            model.ModifiedAt = shipper.ModifiedAt;
            return model;
        }
    }
}
