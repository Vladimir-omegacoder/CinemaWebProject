using System;
using System.Collections.Generic;

namespace Server.Models.DbEntity;

public partial class Ticket
{
    public int Id { get; set; }

    public int ScheduleId { get; set; }

    public int SeatId { get; set; }

    public decimal Price { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual MovieSchedule Schedule { get; set; } = null!;

    public virtual HallSeat Seat { get; set; } = null!;
}
