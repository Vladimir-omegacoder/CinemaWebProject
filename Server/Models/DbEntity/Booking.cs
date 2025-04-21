using System;
using System.Collections.Generic;

namespace Server.Models.DbEntity;

public partial class Booking
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int TicketId { get; set; }

    public int SeatId { get; set; }

    public DateTime BookingDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
