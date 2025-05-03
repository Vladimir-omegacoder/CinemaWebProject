using Server.Models.DbEntity;
using Server.Repositories;

namespace Server.Services
{
    public class TicketBookingService : ITicketBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMovieScheduleRepository _movieScheduleRepository;
        private readonly IHallSeatRepository _hallSeatRepository;
        private readonly ITicketRepository _ticketRepository;

        public TicketBookingService(
            IBookingRepository bookingRepository,
            ICustomerRepository customerRepository,
            IMovieScheduleRepository movieScheduleRepository,
            IHallSeatRepository hallSeatRepository,
            ITicketRepository ticketRepository)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
            _movieScheduleRepository = movieScheduleRepository;
            _hallSeatRepository = hallSeatRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<Booking> BookTicketAsync(int customerId, int scheduleId, int seatId)
        {
            var customer = await _customerRepository.GetAsync(customerId);
            var ticket = (await _ticketRepository.GetAllAsync()).FirstOrDefault(t => t.ScheduleId == scheduleId);
            var seat = await _hallSeatRepository.GetAsync(seatId);

            if (customer == null || ticket == null || seat == null)
            {
                throw new InvalidOperationException("Booking error");
            }

            var booking = new Booking
            {
                CustomerId = customerId,
                TicketId = ticket.Id,
                SeatId = seatId
            };

            return await _bookingRepository.CreateAsync(booking);
        }

        public async Task<List<Booking>> BookTicketsAsync(int customerId, int scheduleId, int[] seatIds)
        {
            var bookings = new List<Booking>();

            for (int i = 0; i < seatIds.Length; i++)
            {
                var booking = await BookTicketAsync(customerId, scheduleId, seatIds[i]);
                bookings.Add(booking);
            }

            return bookings.ToList();
        }

        public async Task CancelBookingAsync(int bookingId)
        {
            await _bookingRepository.DeleteAsync(bookingId);
        }

        public async Task<List<Booking>> GetBookingsAsync(int scheduleId)
        {
            var ticket = (await _ticketRepository.GetAllAsync()).FirstOrDefault(t => t.ScheduleId == scheduleId);
            if (ticket == null)
            {
                throw new ArgumentOutOfRangeException(nameof(scheduleId), scheduleId, "Ticket not found");
            }

            var bookings = await _bookingRepository.GetAllAsync();

            return bookings.Where(b => b.TicketId == ticket.Id).ToList();
        }
    }
}
