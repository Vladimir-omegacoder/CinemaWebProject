using Server.Models.DbEntity;

namespace Server.Models.DataModel
{
    public class HallInfo
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public HallSeat[][] HallSeats { get; set; } = new HallSeat[0][];
    }
}
