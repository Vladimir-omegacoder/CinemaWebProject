using Server.Models.DbEntity;

namespace Server.Services
{
    public interface ITicketBookingService
    {
        Task<Booking> BookTicketAsync(int customerId, int scheduleId, int seatId);

        Task<List<Booking>> BookTicketsAsync(int customerId, int scheduleId, int[] seatIds);

        Task CancelBookingAsync(int bookingId);

        Task<List<Booking>> GetBookingsAsync(int scheduleId);
    }
}
