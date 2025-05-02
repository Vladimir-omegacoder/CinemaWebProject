using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

        public CustomerController(
            IUserRepository userRepository,
            ICustomerRepository customerRepository,
            IMovieInfoRepository movieInfoRepository,
            IHallInfoRepository hallInfoRepository,
            IMovieScheduleRepository movieScheduleRepository)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _movieInfoRepository = movieInfoRepository;
            _hallInfoRepository = hallInfoRepository;
            _movieScheduleRepository = movieScheduleRepository;
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
            //if (!ModelState.IsValid)
            //{
            //    return NotFound();
            //}
            var schedule = await _movieScheduleRepository.GetAsync(scheduleId);
            if (schedule == null)
            {
                return NotFound();
            }

            var hallInfo = await _hallInfoRepository.GetAsync(schedule.HallId);
            return View(hallInfo);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MyTickets()
        {
            return View();
        }
    }
}
