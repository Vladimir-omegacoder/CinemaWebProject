using Server.Models.DataModel;
using System.Security.Claims;

namespace Server.Services
{
    public interface IAuthorizationService
    {
        Task<List<Claim>> GetUserClaimsAsync(LoginCredentials loginCredentials, string role);
    }
}
