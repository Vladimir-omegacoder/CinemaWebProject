using Microsoft.EntityFrameworkCore;
using Server.Models.DbEntity;
using Server.Services;

namespace Server.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly CinemaContext _context;

        public BookingRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking?> GetAsync(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<Booking> CreateAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return await _context.Bookings.FirstOrDefaultAsync(b =>
                b.CustomerId == booking.CustomerId &&
                b.TicketId == booking.TicketId &&
                b.SeatId == booking.SeatId &&
                b.BookingDate == booking.BookingDate);
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await GetAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }
    }
}
