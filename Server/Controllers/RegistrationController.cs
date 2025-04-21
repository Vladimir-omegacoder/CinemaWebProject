using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models.DataModel;
using Server.Services;

namespace Server.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegistrationData());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegistrationData model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var credentials = await _registrationService.RegisterAsync(model);
                return RedirectToAction("Login", "Login");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("Email", ex.Message);
                return View(model);
            }
        }
    }
}
