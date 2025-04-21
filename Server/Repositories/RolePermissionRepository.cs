using Microsoft.EntityFrameworkCore;
using Server.Models.DbEntity;
using Server.Services;

namespace Server.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly CinemaContext _context;

        public RolePermissionRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<List<RolePermission>> GetAllAsync()
        {
            return await _context.RolePermissions.ToListAsync();
        }

        public async Task<RolePermission?> GetAsync(int id)
        {
            return await _context.RolePermissions.FindAsync(id);
        }

        public async Task<RolePermission> CreateAsync(RolePermission rolePermission)
        {
            _context.RolePermissions.Add(rolePermission);
            await _context.SaveChangesAsync();
            return _context.RolePermissions.FirstOrDefault(rp => rp.RoleId == rolePermission.RoleId && rp.PermissionId == rolePermission.PermissionId);
        }

        public async Task UpdateAsync(RolePermission rolePermission)
        {
            _context.RolePermissions.Update(rolePermission);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rolePermission = await GetAsync(id);
            if (rolePermission != null)
            {
                _context.RolePermissions.Remove(rolePermission);
                await _context.SaveChangesAsync();
            }
        }
    }
}
