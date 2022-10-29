using unitofwork_core.Model.Shipepr;

namespace unitofwork_core.Service.ShipperService
{
    public interface IShipperService
    {
        Task<ResponseShipeprModel> Register(RegisterShipperModel model);
    }
}
