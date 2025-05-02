using Microsoft.EntityFrameworkCore;
using Server.Models.DbEntity;
using Server.Services;

namespace Server.Repositories
{
    public class MovieScheduleRepository : IMovieScheduleRepository
    {
        private readonly CinemaContext _context;

        public MovieScheduleRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<List<MovieSchedule>> GetAllAsync()
        {
            return await _context.MovieSchedules.ToListAsync();
        }

        public async Task<MovieSchedule?> GetAsync(int id)
        {
            return await _context.MovieSchedules.FindAsync(id);
        }

        public async Task<MovieSchedule> CreateAsync(MovieSchedule movieSchedule)
        {
            _context.MovieSchedules.Add(movieSchedule);
            await _context.SaveChangesAsync();
            return await _context.MovieSchedules.FirstOrDefaultAsync(ms => ms.Id == movieSchedule.Id);
        }

        public async Task UpdateAsync(MovieSchedule movieSchedule)
        {
            _context.MovieSchedules.Update(movieSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var movieSchedule = await GetAsync(id);
            if (movieSchedule != null)
            {
                _context.MovieSchedules.Remove(movieSchedule);
                await _context.SaveChangesAsync();
            }
        }
    }
}
