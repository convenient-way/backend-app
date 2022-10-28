using System.ComponentModel.DataAnnotations;

namespace unitofwork_core.Entities
{
    public class Shipper: Actor
    {
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
    }
}
