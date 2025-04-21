using Server.Models.DbEntity;

namespace Server.Repositories
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllAsync();
        Task<Role?> GetAsync(int id);
        Task<Role> CreateAsync(Role role);
        Task UpdateAsync(Role role);
        Task DeleteAsync(int id);
    }
}
