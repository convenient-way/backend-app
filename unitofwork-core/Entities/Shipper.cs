using System.ComponentModel.DataAnnotations;
using unitofwork_core.Model.Shipper;
using unitofwork_core.Model.Wallet;

namespace unitofwork_core.Entities
{
    public class Shipper : Actor
    {
        public string Gender { get; set; }
        public double HomeLongitude { get; set; }
        public double HomeLatitude { get; set; }
        public double DestinationLongitude { get; set; }
        public double DestinationLatitude { get; set; }

        #region Relationship
        public IList<Wallet> Wallets { get; set; }
        public IList<Order> Orders { get; set; }
        #endregion
        public Shipper()
        {
            Wallets = new List<Wallet>();
            Orders = new List<Order>();
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
            model.HomeLongitude = this.HomeLongitude;
            model.HomeLatitude = this.HomeLatitude;
            model.DestinationLongitude = this.DestinationLongitude;
            model.DestinationLatitude = this.DestinationLatitude;
            model.CreatedAt = this.CreatedAt;
            model.ModifiedAt = this.ModifiedAt;
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
