using System;
using System.Collections.Generic;

namespace Server.Models.DbEntity;

public partial class Hall
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<HallSeat> HallSeats { get; set; } = new List<HallSeat>();

    public virtual ICollection<MovieSchedule> MovieSchedules { get; set; } = new List<MovieSchedule>();
}
