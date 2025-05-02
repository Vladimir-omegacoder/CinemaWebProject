using Server.Models.DbEntity;

namespace Server.Repositories
{
    public interface IMovieScheduleRepository
    {
        Task<List<MovieSchedule>> GetAllAsync();
        Task<MovieSchedule?> GetAsync(int id);
        Task<MovieSchedule> CreateAsync(MovieSchedule movieSchedule);
        Task UpdateAsync(MovieSchedule movieSchedule);
        Task DeleteAsync(int id);
    }
}
