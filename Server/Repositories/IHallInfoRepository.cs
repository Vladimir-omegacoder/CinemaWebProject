using Server.Models.DataModel;

namespace Server.Repositories
{
    public interface IHallInfoRepository
    {
        Task<HallInfo?> GetAsync(int id);
    }
}
