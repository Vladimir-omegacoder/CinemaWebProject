using Microsoft.EntityFrameworkCore;
using Server.Models.DataModel;
using Server.Services;

namespace Server.Repositories
{
    public class HallInfoRepository : IHallInfoRepository
    {
        private readonly CinemaContext _context;

        public HallInfoRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<HallInfo?> GetAsync(int id)
        {
            var hall = await _context.Halls.FindAsync(id);
            if (hall == null)
            {
                return null;
            }

            var seats = await _context.HallSeats
                .Where(seat => seat.HallId == id)
                .GroupBy(seat => seat.RowNumber)
                .OrderBy(group => group.Key)
                .Select(group => group
                .OrderBy(seat => seat.SeatNumber)
                .ToArray())
                .ToArrayAsync();
            return new HallInfo { Id = id, Name = hall.Name, HallSeats = seats };
        }
    }
}
