using Server.Models.DbEntity;

namespace Server.Repositories
{
    public interface IHallSeatRepository
    {
        Task<List<HallSeat>> GetAllAsync();
        Task<HallSeat?> GetAsync(int id);
    }
}
