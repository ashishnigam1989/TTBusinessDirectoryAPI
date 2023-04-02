using ApplicationService.IServices;
using CommonService.RequestModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using NLog;
using System;
using AspNetCoreHero.ToastNotification.Abstractions;
using System.Collections.Generic;
using TTBusinessAdminPanel.Extensions;

namespace TTBusinessAdminPanel.Controllers
{
    public class AccountController : Controller
    {
        private Logger _logger;
        private IAccount _account;
        private readonly INotyfService _notyfService;

        public AccountController(IAccount account, INotyfService notyfService)
        {
            _logger = LogManager.GetLogger("Account");
            _account = account;
            _notyfService = notyfService;
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
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.Name));
                        claims.Add(new Claim(ClaimTypes.Email, user.EmailAddress));
                        claims.Add(new Claim(ClaimTypes.Role, user.RoleId.ToString()));
                        claims.Add(new Claim(ClaimTypes.PrimarySid, user.Id.ToString()));
                        Helper.SetUserSession(user);

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        _notyfService.Success("Login Successful !!!");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        _notyfService.Error("Invalid User/Password !!!");
                        return View(login);
                    }
                }
                else
                {
                    _notyfService.Error("Validation Error !!!");
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message.ToString());
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
            _notyfService.Success("Logout Successful !!!");
            return RedirectToAction("Login", "Account");
        }
    }
}
