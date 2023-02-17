using ApplicationService.IServices;
using CommonService.RequestModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
    }
}
