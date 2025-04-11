using System;
using System.Collections.Generic;

namespace Server.Models.DbEntity;

public partial class MovieSchedule
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    public DateOnly ShowDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int HallId { get; set; }

    public virtual Hall Hall { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
