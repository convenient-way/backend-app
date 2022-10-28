/*using Bogus;
using unitofwork_core.Constant.Role;
using unitofwork_core.Constant.User;
using unitofwork_core.Core.IConfiguraton;
using unitofwork_core.Core.IRepository;
using unitofwork_core.Entities;

namespace unitofwork_core.Service.DatabaseService
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ILogger<DatabaseService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepo;
        private readonly IUserRepository _userRepo;
        public DatabaseService(ILogger<DatabaseService> logger, IUnitOfWork unitOfWork) {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _roleRepo = unitOfWork.Roles;
            _userRepo = unitOfWork.Users;
        }
        public void RemoveData()
        {
            _unitOfWork.Users.DeleteRange(_userRepo.GetAll());
            _unitOfWork.Roles.DeleteRange(_roleRepo.GetAll());
            _unitOfWork.Complete();
        }

        public void GenerateData()
        {
            Role roleAdmin = new Role
            {
                Name = RoleName.ADMIN
            };
            Role roleUser = new Role
            {
                Name = RoleName.USER
            };
            List<Role> roles = new List<Role>();
            roles.Add(roleAdmin);
            roles.Add(roleUser);
            _unitOfWork.Roles.Insert(roles);

            List<string> avatarsLink = new List<string>();
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/4333/4333609.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/2202/2202112.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/4140/4140047.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/236/236832.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/3006/3006876.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/4333/4333609.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/4140/4140048.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/924/924874.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/4202/4202831.png");
            avatarsLink.Add("https://cdn-icons-png.flaticon.com/512/921/921071.png");
            Faker<User> FakerUser = new Faker<User>()
                .RuleFor(u => u.UserName, faker => faker.Person.UserName)
                .RuleFor(u => u.Email, faker => faker.Person.Email)
                .RuleFor(u => u.Status, faker => faker.PickRandom(UserStatus.GetAllStatus()))
                .RuleFor(u => u.Role, roleUser)
                .RuleFor(u => u.DisplayName, faker => faker.Person.FullName)
                .RuleFor(u => u.PhotoUrl, faker => faker.PickRandom(avatarsLink))
                .RuleFor(u => u.Gender, faker => faker.PickRandom(UserGender.GetGenders()))
                .RuleFor(u => u.Phone, faker => faker.Person.Phone);
            User admin = FakerUser.Generate();
            admin.Role = roleAdmin;
            List<User> users = FakerUser.Generate(800);
            _unitOfWork.Users.Insert(admin);
            _unitOfWork.Users.Insert(users);

            // Save change
            _unitOfWork.Complete();
        }
    }
}
*/