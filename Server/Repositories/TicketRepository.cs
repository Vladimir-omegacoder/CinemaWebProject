using Microsoft.EntityFrameworkCore;
using Server.Models.DbEntity;
using Server.Services;

namespace Server.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaContext _context;

        public TicketRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<Ticket?> GetAsync(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task<Ticket> CreateAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return _context.Tickets.FirstOrDefault(t => t.ScheduleId == ticket.ScheduleId && t.Price == ticket.Price);
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ticket = await GetAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
