using Server.Models.DbEntity;
using Server.Repositories;

namespace Server.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CheckUserCredentialsAsync(string email, string password)
        {
            var users = await _userRepository.GetAllAsync();
            if (users.FirstOrDefault(u => u.Email == email) is User user)
            {
                if (PasswordManager.VerifyPassword(password, user.PasswordHash, Convert.FromHexString(user.PasswordSalt)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
