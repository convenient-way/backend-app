

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
    }
}
