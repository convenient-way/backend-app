using unitofwork_core.Model.AdminModel;

namespace unitofwork_core.Service.AdminService
{
    public interface IAdminService
    {
        Task<ResponseAdminModel> Create(CreateAdminModel model);
    }
}
