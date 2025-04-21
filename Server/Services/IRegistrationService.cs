using Server.Models.DataModel;

namespace Server.Services
{
    public interface IRegistrationService
    {
        Task<LoginCredentials> RegisterAsync(RegistrationData registrationData);  
    }
}
