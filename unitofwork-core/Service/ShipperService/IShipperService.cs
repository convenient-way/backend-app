using unitofwork_core.Model.ShipperModel;

namespace unitofwork_core.Service.ShipperService
{
    public interface IShipperService
    {
        Task<ResponseShipperModel> Register(RegisterShipperModel model);
        Task<ResponseShipperModel?> GetShipperId(Guid id);
    }
}
