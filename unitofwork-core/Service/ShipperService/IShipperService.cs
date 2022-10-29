using unitofwork_core.Model.Shipper;

namespace unitofwork_core.Service.ShipperService
{
    public interface IShipperService
    {
        Task<ResponseShipperModel> Register(RegisterShipperModel model);
    }
}
