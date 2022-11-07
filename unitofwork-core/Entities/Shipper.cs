using System.ComponentModel.DataAnnotations;
using unitofwork_core.Model.ShipperModel;
using unitofwork_core.Model.ShipperRoute;
using unitofwork_core.Model.WalletModel;

namespace unitofwork_core.Entities
{
    public class Shipper : Actor
    {
        public string Gender { get; set; } = String.Empty;

        #region Relationship
        public IList<ShipperRoute> Routes { get; set; }
        public IList<Wallet> Wallets { get; set; }
        public IList<Package> Packages { get; set; }
        #endregion
        public Shipper()
        {
            Wallets = new List<Wallet>();
            Packages = new List<Package>();
            Routes = new List<ShipperRoute>();
        }

        public ResponseShipperModel ToResponseModel()
        {
            ResponseShipperModel model = new ResponseShipperModel();
            model.Id = this.Id;
            model.UserName = this.UserName;
            model.Email = this.Email;
            model.DisplayName = this.DisplayName;
            model.PhoneNumber = this.PhoneNumber;
            model.PhotoUrl = this.PhotoUrl;
            model.Status = this.Status;
            model.Address = this.Address;
    
            model.CreatedAt = this.CreatedAt;
            model.ModifiedAt = this.ModifiedAt;

            if (this.Routes != null)
            {
                model.Routes = new List<ResponseShipperRouteModel>();
                int countRoute = this.Routes.Count;
                for (int i = 0; i < countRoute; i++)
                {
                    model.Routes.Add(this.Routes[i].ToResponseModel());
                }
            }
            if (this.Wallets != null) {
                model.Wallets = new List<ResponseWalletModel>();
                int countWallet = this.Wallets.Count;
                for (int i = 0; i < countWallet; i++)
                {
                    model.Wallets.Add(this.Wallets[i].ToResponseModel());
                }
            }
            return model;
        }
    }
}
