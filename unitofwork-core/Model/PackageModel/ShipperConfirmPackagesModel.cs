using unitofwork_core.Constant.Wallet;

namespace unitofwork_core.Model.PackageModel
{
    public class ShipperConfirmPackagesModel
    {
        public List<Guid> packageIds { get; set; } = new();
        public Guid shipperId{ get; set; }
        public string walletType { get; set; } = WalletType.DEFAULT;
    }
}
