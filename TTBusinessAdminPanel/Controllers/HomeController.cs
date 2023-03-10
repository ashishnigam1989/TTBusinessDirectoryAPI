using ApplicationService.IServices;
using CommonService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
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
        private Logger _logger;
        private IMaster _master;

        public HomeController(IMaster master)
        {
            _logger = LogManager.GetLogger("Home");
            _master = master;
        }

        //Dashboard
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        //View 
        public IActionResult Role()
        {
            return View();
        }

        public IActionResult GetRoles()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int pageNo = (skip / pageSize);
                int recordsTotal = 0;
                var allData = _master.GetRoles(pageNo, pageSize, searchValue).Result;
                var cData = (List<RoleModel>)allData.Data;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    cData = cData.OrderBy(o => sortColumn + " " + sortColumnDirection).ToList();
                }
                recordsTotal = allData.Total;
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = cData };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return Ok(null);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
