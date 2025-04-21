using Microsoft.EntityFrameworkCore;
using Server.Models.DbEntity;
using Server.Services;

namespace Server.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly CinemaContext _context;

        public UserRoleRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<List<UserRole>> GetAllAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<UserRole?> GetAsync(int id)
        {
            return await _context.UserRoles.FindAsync(id);
        }

        public async Task<UserRole> CreateAsync(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
            return _context.UserRoles.FirstOrDefault(ur => ur.UserId == userRole.UserId && ur.RoleId == userRole.RoleId);
        }

        public async Task UpdateAsync(UserRole userRole)
        {
            _context.UserRoles.Update(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userRole = await GetAsync(id);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }
    }
}
