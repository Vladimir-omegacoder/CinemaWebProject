using Microsoft.EntityFrameworkCore;
using Server.Models.DbEntity;
using Server.Services;

namespace Server.Repositories
{
    public class HallSeatRepository : IHallSeatRepository
    {
        private readonly CinemaContext _context;

        public HallSeatRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<List<HallSeat>> GetAllAsync()
        {
            return await _context.HallSeats.ToListAsync();
        }

        public async Task<HallSeat?> GetAsync(int id)
        {
            return await _context.HallSeats.FindAsync(id);
        }
    }
}
