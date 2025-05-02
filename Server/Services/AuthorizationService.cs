using Server.Models.DataModel;
using Server.Repositories;
using System.Security.Claims;

namespace Server.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICustomerRepository _customerRepository;

        public AuthorizationService(IUserRepository userRepository, IRoleRepository roleRepository, ICustomerRepository customerRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _customerRepository = customerRepository;
        }

        public async Task<List<Claim>> GetUserClaimsAsync(LoginCredentials loginCredentials, string role)
        {
            List<Claim> claims = new();

            var users = await _userRepository.GetAllAsync();
            var roles = await _roleRepository.GetAllAsync();

            int? userId = users.FirstOrDefault(u => u.Email == loginCredentials.Email)?.Id;
            int? roleId = roles.FirstOrDefault(r => r.Name == role)?.Id;

            if (userId == null || roleId == null)
                throw new InvalidOperationException("Authorization error");

            claims.Add(new Claim("UserId", userId.ToString()));
            claims.Add(new Claim("RoleId", roleId.ToString()));

            if (roleId == 5)
            {
                var customers = await _customerRepository.GetAllAsync();
                int? customerId = customers.FirstOrDefault(c => c.UserId == userId)?.Id;
                if (customerId == null)
                    throw new InvalidOperationException("Authorization error");

                claims.Add(new Claim("CustomerId", customerId.ToString()));
            }

            return claims;
        }
    }
}
