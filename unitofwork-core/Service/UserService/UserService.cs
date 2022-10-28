/*using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;
using unitofwork_core.Entities.Extension;
using unitofwork_core.Model.Collection;
using unitofwork_core.Model.User;

namespace unitofwork_core.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepo;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepo = unitOfWork.Users;
        }
        public async Task<User> Create(CreateUserModel model)
        {
            User user = new User
            {
                UserName = model.UserName,
                Password = model.Password,
                DisplayName = model.DisplayName,
                Status = model.Status,
                Phone = model.Phone,
                Gender = model.Gender,
                Email = model.Email,
                PhotoUrl = model.PhotoUrl,
                RoleId = model.RoleId
            };
            user = await _userRepo.InsertAsync(user);
            await _unitOfWork.CompleteAsync();
            return user;
        }

        public async Task<bool> Delete(Guid id)
        {
            bool result = _userRepo.Delete(id);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<User?> Get(Guid id)
        {
            Func<IQueryable<User>, IIncludableQueryable<User, object?>> include = (user) => user.Include(u => u.Role);
            User? user = await _userRepo.GetByIdAsync(id, include: include);
            return user;
        }

        public async Task<PaginatedList<User>> GetList(string? name, string? gender, string? status, int pageIndex, int pageSize)
        {
            #region Filtering
            List<Expression<Func<User, bool>>> predicates = new List<Expression<Func<User, bool>>>();
            if (!string.IsNullOrEmpty(name)) {
                Expression<Func<User, bool>> filterName = (user) => user.DisplayName.Contains(name);
                predicates.Add(filterName);
            }
            if (!string.IsNullOrEmpty(gender)) {
                Expression<Func<User, bool>> filterGender = (user) => user.Gender.Equals(gender.ToUpper());
                predicates.Add(filterGender);
            }
            if (!string.IsNullOrEmpty(status)) {
                Expression<Func<User, bool>> filterStatus = (user) => user.Status.Equals(status.ToUpper());
                predicates.Add(filterStatus);
            }
            #endregion
            #region Includable
            Func<IQueryable<User>, IIncludableQueryable<User, object?>> include =
                (source) => source.Include(user => user.Role);
            #endregion
            #region Order
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = 
                (source) => source.OrderBy(user => user.DisplayName);
            #endregion
            // Expression<Func<User, ResponseUserModel>> selector = (user) => user.ToResponseModel();
            PaginatedList<User> items = await _userRepo.GetPagedListAsync(
                predicates: predicates, include: include, orderBy: orderBy, pageIndex: pageIndex, pageSize: pageSize);
            return items;
        }

        public async Task<PaginatedList<ResponseUserModel>> GetListResponseModel(string? name, string? gender, string? status,
            int pageIndex, int pageSize)
        {
            #region Filtering
            List<Expression<Func<User, bool>>> predicates = new List<Expression<Func<User, bool>>>();
            if (!string.IsNullOrEmpty(name))
            {
                Expression<Func<User, bool>> filterName = (user) => user.DisplayName.Contains(name);
                predicates.Add(filterName);
            }
            if (!string.IsNullOrEmpty(gender))
            {
                Expression<Func<User, bool>> filterGender = (user) => user.Gender.Equals(gender.ToUpper());
                predicates.Add(filterGender);
            }
            if (!string.IsNullOrEmpty(status))
            {
                Expression<Func<User, bool>> filterStatus = (user) => user.Status.Equals(status.ToUpper());
                predicates.Add(filterStatus);
            }
            #endregion
            #region Includable
            Func<IQueryable<User>, IIncludableQueryable<User, object?>> include =
                (source) => source.Include(user => user.Role);
            #endregion
            #region Order
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy =
                (source) => source.OrderBy(user => user.DisplayName);
            #endregion
            Expression<Func<User, ResponseUserModel>> selector = (user) => user.ToResponseModel();
            PaginatedList<ResponseUserModel> items = await _userRepo.GetPagedListAsync(
                selector : selector, predicates: predicates, 
                include: include, orderBy: orderBy, pageIndex: pageIndex, pageSize: pageSize);
            return items;
        }

        public async Task<User> Update(UpdateUserModel model)
        {
            User user = new User
            {
                Id = model.Id,
                UserName = model.UserName,
                Password = model.Password,
                DisplayName = model.DisplayName,
                Status = model.Status,
                Phone = model.Phone,
                Gender = model.Gender,
                Email = model.Email,
                PhotoUrl = model.PhotoUrl,
                RoleId = model.RoleId
            };
            user = _userRepo.Update(user);
            await _unitOfWork.CompleteAsync();
            return user;
        }
    }
}
*/