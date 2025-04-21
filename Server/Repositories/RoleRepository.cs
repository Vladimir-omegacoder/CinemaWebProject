using Microsoft.EntityFrameworkCore;
using Server.Models.DbEntity;
using Server.Services;

namespace Server.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly CinemaContext _context;

        public RoleRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role> CreateAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return _context.Roles.FirstOrDefault(r => r.Name == role.Name);
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var role = await GetAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
