using System.ComponentModel.DataAnnotations;
using unitofwork_core.Model.ShipperModel;
using unitofwork_core.Model.ShopModel;

namespace unitofwork_core.Entities
{
    public class Shop : Actor
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        #region Relationship
        public IList<Wallet> Wallets { get; set; }
        public IList<Package> Packages { get; set; }
        #endregion
        public Shop()
        {
            Wallets = new List<Wallet>();
            Packages = new List<Package>();
        }

        public ResponseShopModel ToResponseModel()
        {
            ResponseShopModel model = new ResponseShopModel();
            model.Id = this.Id;
            model.UserName = this.UserName;
            model.DisplayName = this.DisplayName;
            model.Status = this.Status;
            model.PhoneNumber = this.PhoneNumber;
            model.Email = this.Email;
            model.PhotoUrl = this.PhotoUrl;
            model.CreatedAt = this.CreatedAt;
            model.ModifiedAt = this.ModifiedAt;
            model.Longitude = this.Longitude;
            model.Latitude = this.Latitude;
            model.Address = this.Address;
            return model;
        }
    }
}
