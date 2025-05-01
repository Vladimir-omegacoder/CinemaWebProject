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

        public CustomerController(IUserRepository userRepository, ICustomerRepository customerRepository, IMovieInfoRepository movieInfoRepository)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _movieInfoRepository = movieInfoRepository;
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
        public async Task<IActionResult> Tickets()
        {
            return View();
        }
    }
}
