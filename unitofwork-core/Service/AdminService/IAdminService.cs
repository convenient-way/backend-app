using unitofwork_core.Model.Admin;

namespace unitofwork_core.Service.AdminService
{
    public interface IAdminService
    {
        Task<ResponseAdminModel> Create(CreateAdminModel model);
    }
}
