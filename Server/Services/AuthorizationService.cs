using Server.Models.DataModel;
using Server.Repositories;
using System.Security.Claims;

namespace Server.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthorizationService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<List<Claim>> GetUserClaimsAsync(LoginCredentials loginCredentials, string role)
        {
            List<Claim> claims = new();

            var users = await _userRepository.GetAllAsync();
            var roles = await _roleRepository.GetAllAsync();

            int? userId = users.FirstOrDefault(u => u.Email == loginCredentials.Email)?.Id;
            int? roleId = roles.FirstOrDefault(r => r.Name == role)?.Id;

            if (userId == null || roleId == null)
                throw new Exception("Authorization error");

            claims.Add(new Claim("UserId", userId.ToString()));
            claims.Add(new Claim("RoleId", roleId.ToString()));

            return claims;
        }
    }
}
