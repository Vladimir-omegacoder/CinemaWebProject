using Server.Models.DataModel;
using Server.Models.DbEntity;

namespace Server.Repositories
{
    public interface IMovieInfoRepository
    {
        Task<List<MovieInfo>> GetAllAsync();
        Task<MovieInfo?> GetAsync(int id);
    }
}
