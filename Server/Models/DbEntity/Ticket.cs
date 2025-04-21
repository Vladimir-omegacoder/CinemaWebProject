using System;
using System.Collections.Generic;

namespace Server.Models.DbEntity;

public partial class Ticket
{
    public int Id { get; set; }

    public int ScheduleId { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual MovieSchedule Schedule { get; set; } = null!;
}
