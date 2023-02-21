using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TTBusinessAdminPanel.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ILogger<CompanyController> _logger;

        //View/Add/Edit
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }

        //View/Add/Delete
        public IActionResult Category()
        {
            return View();
        }
        public IActionResult CategoryAdd()
        {
            return View();
        }

        //View/Add/Delete
        public IActionResult Brand()
        {
            return View();
        }
        public IActionResult BrandAdd()
        {
            return View();
        }
    }
}
