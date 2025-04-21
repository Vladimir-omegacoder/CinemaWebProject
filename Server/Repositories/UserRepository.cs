using Microsoft.EntityFrameworkCore;
using Server.Models.DbEntity;
using Server.Services;

namespace Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CinemaContext _context;

        public UserRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Username == user.Username && u.PasswordHash == user.PasswordHash);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
