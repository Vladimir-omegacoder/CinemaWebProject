using System;
using System.Collections.Generic;

namespace Server.Models.DbEntity;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int GenreId { get; set; }

    public double Rating { get; set; }

    public int AgeRestrictions { get; set; }

    public int Duration { get; set; }

    public string? Description { get; set; }

    public virtual MovieGenre Genre { get; set; } = null!;

    public virtual ICollection<MovieSchedule> MovieSchedules { get; set; } = new List<MovieSchedule>();
}
