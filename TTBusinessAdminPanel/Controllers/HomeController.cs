using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TTBusinessAdminPanel.Models;

namespace TTBusinessAdminPanel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Dashboard
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //View 
        public IActionResult Role()
        {
            return View();
        }

        //View 
        public IActionResult Country()
        {
            return View();
        }

        #region Region
        //View/Add/Edit
        public IActionResult Region()
        {
            return View();
        }

        public IActionResult RegionAdd()
        {
            return View();
        }

        public IActionResult RegionEdit()
        {
            return View();
        }
        #endregion

        #region Category
        public IActionResult Category()
        {
            return View();
        }
        public IActionResult CategoryAdd()
        {
            return View();
        }
        public IActionResult CategoryEdit()
        {
            return View();
        }
        #endregion

        #region Brand
        //View/Add/Edit
        public IActionResult Brand()
        {
            return View();
        }
        public IActionResult BrandAdd()
        {
            return View();
        }
        public IActionResult BrandEdit()
        {
            return View();
        }
        public IActionResult BrandCategoryMapping()
        {
            return View();
        }
        #endregion

        #region Design Demo
        public IActionResult Form()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }
        #endregion
    }
}
