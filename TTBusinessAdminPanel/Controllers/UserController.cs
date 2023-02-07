using ApplicationService.IServices;
using CommonService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TTBusinessAdminPanel.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private IAccount _account;
        public UserController(ILogger<UserController> logger, IAccount account)
        {
            _logger = logger;
            _account = account;
        }

        public IActionResult Index()
        {
            UserListModel ulist = new UserListModel();
            try
            {
                ulist = _account.GetUsers(0, 20).Result;
            }
            catch
            {
                throw;
            }

            return View(ulist);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Approve()
        {
            return View();
        }

        public IActionResult RoleMenuMap()
        {
            return View();
        }
    }
}
