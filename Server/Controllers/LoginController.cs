using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models.DataModel;

namespace Server.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login page
        public IActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml", new LoginData() { Email = "", Password = "" });
        }

        // POST: Handle login
        [HttpPost]
        public IActionResult Login(LoginData model)
        {
            

            return View(model);
        }
    }
}
