using unitofwork_core.Constant.Wallet;

namespace unitofwork_core.Model.PackageModel
{
    public class ShipperPickUpModel { 
        public Guid shipperId { get; set; }
        public List<Guid> packageIds { get; set; } = new List<Guid>();
        public string walletType { get; set; } = WalletType.DEFAULT;
    }
}
