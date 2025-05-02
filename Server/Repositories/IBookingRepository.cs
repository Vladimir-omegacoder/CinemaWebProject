using Server.Models.DbEntity;

namespace Server.Repositories
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllAsync();
        Task<Booking?> GetAsync(int id);
        Task<Booking> CreateAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(int id);
    }
}
