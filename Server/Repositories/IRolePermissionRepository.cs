using Server.Models.DbEntity;

namespace Server.Repositories
{
    public interface IRolePermissionRepository
    {
        Task<List<RolePermission>> GetAllAsync();
        Task<RolePermission?> GetAsync(int id);
        Task<RolePermission> CreateAsync(RolePermission rolePermission);
        Task UpdateAsync(RolePermission rolePermission);
        Task DeleteAsync(int id);
    }
}
