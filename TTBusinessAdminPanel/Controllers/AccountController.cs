using ApplicationService.IServices;
using CommonService.RequestModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using NLog;
using System;

namespace TTBusinessAdminPanel.Controllers
{
    public class AccountController : Controller
    {
        private Logger _logger;

        private IAccount _account;
        public AccountController(IAccount account)
        {
            _logger = LogManager.GetLogger("Account");
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
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _account.Login(login).Result;
                    if (user != null)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Name) }, CookieAuthenticationDefaults.AuthenticationScheme);
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
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
            }
            return View();
        }

        public IActionResult Logout()
        {
            try { 
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
