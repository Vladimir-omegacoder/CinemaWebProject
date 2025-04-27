using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repositories;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRoleRepository _roleRepository;
        private readonly IMovieInfoRepository _movieInfoRepository;

        public HomeController(ILogger<HomeController> logger, IRoleRepository roleRepository, IMovieInfoRepository movieInfoRepository)
        {
            _logger = logger;
            _roleRepository = roleRepository;
            _movieInfoRepository = movieInfoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Home()
        {
            string? roleId = User.Claims?.FirstOrDefault(c => c.Type == "RoleId")?.Value;
            if (roleId == null)
            {
                return View(await _movieInfoRepository.GetAllAsync());
            }

            var role = await _roleRepository.GetAsync(int.Parse(roleId));
            if (role == null)
            {
                return View(await _movieInfoRepository.GetAllAsync());
            }

            return RedirectToAction("Home", $"{role.Name}");
        }

        [HttpGet]
        public IActionResult Account()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Account", "Account");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
