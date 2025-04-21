using Microsoft.EntityFrameworkCore;
using Server.Models.DbEntity;
using Server.Services;

namespace Server.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly CinemaContext _context;

        public PermissionRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<List<Permission>> GetAllAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission?> GetAsync(int id)
        {
            return await _context.Permissions.FindAsync(id);
        }

        public async Task<Permission> CreateAsync(Permission permission)
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
            return _context.Permissions.FirstOrDefault(p => p.TableName == permission.TableName && p.Operation == permission.Operation);
        }

        public async Task UpdateAsync(Permission permission)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var permission = await GetAsync(id);
            if (permission != null)
            {
                _context.Permissions.Remove(permission);
                await _context.SaveChangesAsync();
            }
        }
    }
}
