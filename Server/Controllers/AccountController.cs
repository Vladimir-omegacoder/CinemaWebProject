using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models.DataModel;
using Server.Repositories;
using System.Security.Claims;

namespace Server.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AccountController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<IActionResult> AccountOverview()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            int roleId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "RoleId").Value);

            var user = await _userRepository.GetAsync(userId);
            var role = await _roleRepository.GetAsync(roleId);

            var username = user?.Username ?? "Unknown";
            var email = user?.Email ?? "Unknown";
            var roleName = role?.Name ?? "Unknown";

            var model = new AccountInfo
            {
                Username = username,
                Email = email,
                Password = "********",
                Role = roleName
            };

            return View("AccountOverview", model);
        }
    }
}
