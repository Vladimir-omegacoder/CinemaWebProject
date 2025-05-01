using Microsoft.EntityFrameworkCore;
using Server.Models.DataModel;
using Server.Services;

namespace Server.Repositories
{
    public class MovieInfoRepository : IMovieInfoRepository
    {
        private readonly CinemaContext _context;
        public MovieInfoRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<List<MovieInfo>> GetAllAsync()
        {
            var movies = _context.Movies;
            var genres = _context.MovieGenres;
            var schedules = _context.MovieSchedules;
            var movieInfo = movies.Join(genres, m => m.GenreId, g => g.Id, (m, g) =>
            new MovieInfo()
            {
                Id = m.Id,
                Title = m.Title,
                Rating = m.Rating,
                Genre = g.Name,
                Duration = m.Duration,
                AgeRestrictions = m.AgeRestrictions,
                Description = m.Description,
                MovieSchedules = schedules.Where(s => s.MovieId == m.Id).ToList(),
            });

            return await movieInfo.ToListAsync();
        }

        public async Task<MovieInfo?> GetAsync(int id)
        {
            var movies = _context.Movies;
            var genres = _context.MovieGenres;
            var schedules = _context.MovieSchedules;
            var movieInfo = movies.Where(m => m.Id == id).Join(genres, m => m.GenreId, g => g.Id, (m, g) =>
            new MovieInfo()
            {
                Id = m.Id,
                Title = m.Title,
                Rating = m.Rating,
                Genre = g.Name,
                Duration = m.Duration,
                AgeRestrictions = m.AgeRestrictions,
                Description = m.Description,
                MovieSchedules = schedules.Where(s => s.MovieId == m.Id).ToList(),
            });

            var result = await movieInfo.ToListAsync();

            return result.Count == 0 ? null : result[0];
        }
    }
}
