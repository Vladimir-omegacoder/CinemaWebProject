using Server.Models.DbEntity;

namespace Server.Models.DataModel
{
    public class AvailableSeatsInfo
    {
        public Ticket Ticket { get; set; }

        public MovieSchedule MovieSchedule { get; set; }

        public HallInfo HallInfo { get; set; }
        
        public List<Booking> Bookings { get; set; }
    }
}
