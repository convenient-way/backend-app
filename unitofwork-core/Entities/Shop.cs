using System.ComponentModel.DataAnnotations;

namespace unitofwork_core.Entities
{
    public class Shop : Actor
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        #region Relationship
        public IList<Wallet> Wallets { get; set; }
        public IList<Order> Orders { get; set; }
        #endregion
        public Shop()
        {
            Wallets = new List<Wallet>();
            Orders = new List<Order>();
        }
    }
}
