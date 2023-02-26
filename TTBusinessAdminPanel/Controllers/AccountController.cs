using ApplicationService.IServices;
using CommonService.RequestModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace TTBusinessAdminPanel.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private IAccount _account;
        public AccountController(ILogger<AccountController> logger, IAccount account)
        {
            _logger = logger;
            _account = account;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginRequestModel login)
        {
            if (ModelState.IsValid)
            {
                var user = _account.Login(login).Result;
                if (user != null)
                {
                    var identity = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, user.Name) }, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                  //  login.LoginMessage = "User does not exists!!!";
                    return View(login);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
