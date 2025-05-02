using Server.Models.DataModel;
using Server.Models.DbEntity;
using Server.Repositories;

namespace Server.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ICustomerRepository _customerRepository;

        public RegistrationService(IUserRepository userRepository, IUserRoleRepository userRoleRepository, ICustomerRepository customerRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _customerRepository = customerRepository;
        }

        public async Task<LoginCredentials> RegisterAsync(RegistrationData registrationData)
        {
            var users = await _userRepository.GetAllAsync();

            if (users.FirstOrDefault(u => u.Email == registrationData.Email) != null)
                throw new InvalidOperationException("This email is already taken.");

            byte[] passwordSalt;
            string passwordHash = PasswordManager.HashPassword(registrationData.Password, out passwordSalt);

            User user = new()
            {
                Id = 0,
                Email = registrationData.Email,
                Username = registrationData.Username,
                PasswordHash = passwordHash,
                PasswordSalt = PasswordManager.SaltToString(passwordSalt)
            };

            await _userRepository.CreateAsync(user);

            int userId = (await _userRepository.GetAllAsync()).FirstOrDefault(u => u.Email == registrationData.Email).Id;

            UserRole userRole = new()
            {
                RoleId = 5,
                UserId = userId
            };
            await _userRoleRepository.CreateAsync(userRole);

            Customer customer = new()
            {
                UserId = userId,
                Phone = null,
            };
            await _customerRepository.CreateAsync(customer);

            return new LoginCredentials { Email = registrationData.Email, Password = registrationData.Password };
        }
    }
}
