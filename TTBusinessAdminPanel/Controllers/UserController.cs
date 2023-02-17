using ApplicationService.IServices;
using CommonService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

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
            try
            {
            }
            catch
            {
                throw;
            }

            return View();
        }

        [HttpPost]
        public IActionResult LoadUser()
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
                int pageNo = (skip/pageSize);
                int recordsTotal = 0;
                var allData = _account.GetUsers(pageNo, pageSize, searchValue).Result;
                var userData = allData.UserList;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    userData = userData.OrderBy(o=>sortColumn + " " + sortColumnDirection).ToList() ;
                }
                //if (!string.IsNullOrEmpty(searchValue))
                //{
                //    userData = (List<UserModel>)userData.Where(m => m.Name.Contains(searchValue)
                //                                //|| m.Rolename.Contains(searchValue)
                //                                || m.EmailAddress.Contains(searchValue)
                //                                || m.Mobile.Contains(searchValue)).ToList();
                //}
                recordsTotal = allData.Count;
                //var data = userData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = userData };
                return Ok(jsonData);

            }
            catch (Exception ex)
            {
                throw;
            }
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
