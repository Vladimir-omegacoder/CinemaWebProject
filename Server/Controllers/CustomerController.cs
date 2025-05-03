using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Server.Models.DataModel;
using Server.Models.DbEntity;
using Server.Repositories;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMovieInfoRepository _movieInfoRepository;
        private readonly IMovieScheduleRepository _movieScheduleRepository;
        private readonly IHallInfoRepository _hallInfoRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketBookingService _ticketBookingService;

        public CustomerController(
            IUserRepository userRepository,
            ICustomerRepository customerRepository,
            IMovieInfoRepository movieInfoRepository,
            IHallInfoRepository hallInfoRepository,
            IMovieScheduleRepository movieScheduleRepository,
            ITicketRepository ticketRepository,
            ITicketBookingService ticketBookingService)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _movieInfoRepository = movieInfoRepository;
            _hallInfoRepository = hallInfoRepository;
            _movieScheduleRepository = movieScheduleRepository;
            _ticketRepository = ticketRepository;
            _ticketBookingService = ticketBookingService;
        }

        [HttpGet]
        public async Task<IActionResult> Home()
        {
            return View(await _movieInfoRepository.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Account()
        {
            string? value = User.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (string.IsNullOrEmpty(value))
            {
                return NotFound();
            }

            int userId = int.Parse(value);

            var user = await _userRepository.GetAsync(userId);
            var customer = await _customerRepository.GetAsync(userId);

            var username = user?.Username ?? "No username";
            var email = user?.Email ?? "No email";
            var phone = customer?.Phone ?? "No phone";

            var model = new CustomerInfo
            {
                Username = username,
                Email = email,
                Password = "********",
                Role = "Customer",
                Phone = phone,
            };

            return View("Account", model);
        }

        [HttpGet]
        public async Task<IActionResult> MovieInfo(int id)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var movieInfo = await _movieInfoRepository.GetAsync(id);
            if (movieInfo == null)
            {
                return NotFound();
            }
            return View(movieInfo);
        }

        [HttpGet]
        public async Task<IActionResult> ViewSeats(int scheduleId)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var schedule = await _movieScheduleRepository.GetAsync(scheduleId);
            if (schedule == null)
            {
                return NotFound();
            }

            var hallInfo = await _hallInfoRepository.GetAsync(schedule.HallId);
            if (hallInfo == null)
            {
                return NotFound();
            }

            var bookings = await _ticketBookingService.GetBookingsAsync(scheduleId);
            var ticket = (await _ticketRepository.GetAllAsync()).FirstOrDefault(t => t.ScheduleId == scheduleId);

            var availableSeatsInfo = new AvailableSeatsInfo()
            {
                Ticket = ticket,
                HallInfo = hallInfo,
                Bookings = bookings,
                MovieSchedule = schedule,
            };
            return View(availableSeatsInfo);
        }

        [HttpPost]
        public async Task<IActionResult> BuyTickets(int scheduleId, string selectedSeats)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var seatIds = selectedSeats?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(id => int.Parse(id))
                .ToList();

            if (seatIds == null || !seatIds.Any())
                return BadRequest("No seats selected");

            var customerId = User.FindFirst("CustomerId")?.Value;
            if (customerId == null)
                return BadRequest("Customer is not authorized");

            await _ticketBookingService.BookTicketsAsync(int.Parse(customerId), scheduleId, seatIds.ToArray());

            return RedirectToAction("ViewSeats", new { scheduleId });
        }

        [HttpGet]
        public async Task<IActionResult> MyTickets()
        {
            return View();
        }
    }
}
