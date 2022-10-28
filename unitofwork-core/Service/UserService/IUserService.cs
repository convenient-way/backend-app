/*using System.Linq.Expressions;
using unitofwork_core.Entities;
using unitofwork_core.Model.Collection;
using unitofwork_core.Model.User;

namespace unitofwork_core.Service.UserService
{
    public interface IUserService
    {
        Task<User?> Get(Guid id);
        Task<PaginatedList<User>> GetList(string? name, string? gender, string? status, int pageIndex, int pageSize);
        Task<PaginatedList<ResponseUserModel>> GetListResponseModel(string? name, string? gender, string? status, 
            int pageIndex, int pageSize);
        Task<User> Create(CreateUserModel model);
        Task<User> Update(UpdateUserModel model);
        Task<bool> Delete(Guid id);
    }
}
*/