using Server.Models.DbEntity;

namespace Server.Models.DataModel
{
    public class MovieInfo
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public double Rating { get; set; }

        public int AgeRestrictions { get; set; }

        public int Duration { get; set; }

        public string? Description { get; set; }

        public List<MovieSchedule> MovieSchedules { get; set; } = new List<MovieSchedule>();
    }
}
