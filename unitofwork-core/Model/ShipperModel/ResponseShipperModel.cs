using unitofwork_core.Model.ShipperRoute;
using unitofwork_core.Model.WalletModel;

namespace unitofwork_core.Model.ShipperModel
{
    public class ResponseShipperModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        
        public List<ResponseShipperRouteModel>? Routes { get; set; }
        public List<ResponseWalletModel>? Wallets { get; set; }
    }
}
