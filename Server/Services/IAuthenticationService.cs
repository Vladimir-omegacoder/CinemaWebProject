namespace Server.Services
{
    public interface IAuthenticationService
    {
        Task<bool> CheckUserCredentialsAsync(string email, string password);
    }
}
