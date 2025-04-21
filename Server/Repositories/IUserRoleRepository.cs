using Server.Models.DbEntity;

namespace Server.Repositories
{
    public interface IUserRoleRepository
    {
        Task<List<UserRole>> GetAllAsync();
        Task<UserRole?> GetAsync(int id);
        Task<UserRole> CreateAsync(UserRole userRole);
        Task UpdateAsync(UserRole userRole);
        Task DeleteAsync(int id);
    }
}
