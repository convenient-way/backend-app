using unitofwork_core.Model.AdminModel;
using unitofwork_core.Model.ShipperModel;
using unitofwork_core.Model.ShopModel;

namespace unitofwork_core.Model.AuthorizeModel
{
    public class ResponseLoginModel
    {
        public string? Token { get; set; }
        public ResponseAdminModel? Admin { get; set; }
        public ResponseShopModel? Shop { get; set; }
        public ResponseShipperModel? Shipper { get; set; }
    }
}
