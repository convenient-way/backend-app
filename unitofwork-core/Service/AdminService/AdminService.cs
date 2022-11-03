using unitofwork_core.Constant.Admin;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;
using unitofwork_core.Model.AdminModel;

namespace unitofwork_core.Service.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly ILogger<AdminService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdminRepository _adminRepo;

        public AdminService(ILogger<AdminService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _adminRepo = unitOfWork.Admins;
        }
        public async Task<ResponseAdminModel> Create(CreateAdminModel model)
        {
            Admin admin = new Admin();
            admin.UserName = model.UserName;
            admin.Password = model.Password;
            admin.Email = model.Email;
            admin.DisplayName = model.DisplayName;
            admin.FirstName = model.FirstName;
            admin.LastName = model.LastName;
            admin.Gender = model.Gender;
            admin.PhoneNumber = model.PhoneNumber;
            admin.PhotoUrl = model.PhotoUrl;
            admin.Status = AdminStatus.ACTIVE;
            admin.Address = model.Address;
            await _adminRepo.InsertAsync(admin);
            await _unitOfWork.CompleteAsync();
            return admin.ToResponseModel();
        }
    }
}
