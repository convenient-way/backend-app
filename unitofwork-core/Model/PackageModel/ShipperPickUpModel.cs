using unitofwork_core.Constant.Wallet;

namespace unitofwork_core.Model.PackageModel
{
    public class ShipperPickUpModel { 
        public Guid shipperId { get; set; }
        public Guid packageId { get; set; }
        public string walletType { get; set; } = WalletType.DEFAULT;
    }
}
