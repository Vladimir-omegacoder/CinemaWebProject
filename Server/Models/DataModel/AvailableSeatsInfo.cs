using Server.Models.DbEntity;

namespace Server.Models.DataModel
{
    public class AvailableSeatsInfo
    {
        public HallInfo HallInfo { get; set; }
        
        public List<Booking> Bookings { get; set; }
    }
}
