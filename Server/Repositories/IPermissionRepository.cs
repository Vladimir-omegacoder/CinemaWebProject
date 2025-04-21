using Server.Models.DbEntity;

namespace Server.Repositories
{
    public interface IPermissionRepository
    {
        Task<List<Permission>> GetAllAsync();
        Task<Permission?> GetAsync(int id);
        Task<Permission> CreateAsync(Permission permission);
        Task UpdateAsync(Permission permission);
        Task DeleteAsync(int id);
    }
}
