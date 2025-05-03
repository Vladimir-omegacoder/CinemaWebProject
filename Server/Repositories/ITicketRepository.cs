using Server.Models.DbEntity;

namespace Server.Repositories
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllAsync();
        Task<Ticket?> GetAsync(int id);
        Task<Ticket> CreateAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
        Task DeleteAsync(int id);
    }
}
