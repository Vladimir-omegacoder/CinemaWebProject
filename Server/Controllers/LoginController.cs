using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Models.DataModel;
using Server.Repositories;
using Server.Services;
using System.Security.Claims;

namespace Server.Controllers
{
    public class LoginController : Controller
    {
        private readonly Services.IAuthenticationService _authenticationService;
        private readonly Services.IAuthorizationService _authorizationService;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;

        public LoginController(Services.IAuthenticationService authenticationService, Services.IAuthorizationService authorizationService, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginCredentials() { Email = "", Password = "" });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginCredentials model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!await _authenticationService.CheckUserCredentialsAsync(model.Email, model.Password))
            {
                ModelState.AddModelError("", "Invalid user credentials.");
                return View(model);
            }

            var userRoles = (await _userRoleRepository.GetAllAsync()).Where(ur => ur.User.Email == model.Email).ToList();

            if (!userRoles.Any())
            {
                ModelState.AddModelError("", "No roles assigned to this user.");
                return View(model);
            }

            if (userRoles.Count > 1)
            {
                TempData["Email"] = model.Email;
                TempData["Password"] = model.Password;

                var roles = (await _roleRepository.GetAllAsync()).Where(r => userRoles.Select(ur => ur.RoleId).Contains(r.Id)).Select(r => r.Name);

                TempData["Roles"] = JsonConvert.SerializeObject(roles.ToList());
                return RedirectToAction("SelectRole", "Login");
            }

            var role = (await _roleRepository.GetAsync(userRoles.First().RoleId)).Name;

            var email = model.Email;
            var password = model.Password;

            if (email == null || password == null)
                return RedirectToAction("Login");

            var loginModel = new LoginCredentials { Email = email, Password = password };

            var claims = await _authorizationService.GetUserClaimsAsync(loginModel, role);

            var identity = new ClaimsIdentity(claims, Program.cookieAuthenticationSchemeName);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(Program.cookieAuthenticationSchemeName, principal);

            return RedirectToAction("Home", $"{role}");
        }

        [HttpGet]
        public IActionResult SelectRole()
        {
            if (TempData["Roles"] == null)
            {
                return RedirectToAction("Login");
            }
            var roles = JsonConvert.DeserializeObject<List<string>>((string)TempData["Roles"]);
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> SelectRole(string selectedRole)
        {
            var email = TempData["Email"]?.ToString();
            var password = TempData["Password"]?.ToString();

            if (email == null || password == null)
                return RedirectToAction("Login");

            var loginModel = new LoginCredentials { Email = email, Password = password };

            var claims = await _authorizationService.GetUserClaimsAsync(loginModel, selectedRole);

            var identity = new ClaimsIdentity(claims, Program.cookieAuthenticationSchemeName);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(Program.cookieAuthenticationSchemeName, principal);

            return RedirectToAction("Home", $"{selectedRole}");
        }

    }
}
