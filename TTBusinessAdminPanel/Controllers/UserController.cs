using ApplicationService.IServices;
using CommonService.RequestModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace TTBusinessAdminPanel.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private IAccount _account;

        private IMaster _master;
        public UserController(ILogger<UserController> logger, IAccount account, IMaster master)
        {
            _logger = logger;
            _account = account;
            _master = master;
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
                var userData =(List<UserModel>) allData.Data;
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
                recordsTotal = allData.Total;
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
            BindRoles();
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserRequestModel userRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    userRequest.Password = RandomPassword();
                    userRequest.UserName = userRequest.EmailAddress;
                    var isadded = _account.CreateUser(userRequest).Result;
                    if (isadded)
                    {
                        return RedirectToAction("Index", "User");
                    }
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                throw;
            }
            return RedirectToAction("Add", "User");
        }

        public IActionResult Edit(int id)
        {
            UserRequestModel umodel = new UserRequestModel();
            var user = _account.GetUserById(id).Result;
            if (user != null)
            {
                umodel = new UserRequestModel()
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    EmailAddress = user.EmailAddress,
                    Mobile = user.Mobile,
                    RoleId = user.RoleId
                };
            }
            BindRoles();
            return View(umodel);
        }

        public IActionResult Approve()
        {
            return View();
        }

        public IActionResult RoleMenuMap()
        {
            BindRoles(); 
            return View();
        }

        private void BindRoles()
        {
            var r = _master.GetRoles().Result;
            ViewBag.Roles = new SelectList(r, "Id", "DisplayName");
        }

        [HttpGet]
        public IActionResult BindMenus(int id)
        {
            var allmenus = _master.GetAllMenus().Result;
            var menus = _account.GetMenus(id).Result.Select(s => s.MenuId).ToList();
            if (menus.Count > 0)
                allmenus.Where(w => menus.Contains(w.MenuId)).ToList().ForEach(f => f.IsSelected = true);
            return Json(allmenus);
        }

        [HttpPost]
        public IActionResult ApproveRejectUser(UserApproveModel uModel)
        {
            var isupdated =_account.ApproveRejectUser(uModel).Result; 
            return Json(isupdated);
        }

        [HttpPost]
        public IActionResult AssignMenusToRole(RoleMenuMapping rolemenuRequest)
        {
            if (ModelState.IsValid && rolemenuRequest.RoleId!=0)
            {
                var result = _account.RoleMenuPermission(rolemenuRequest).Result;
                return Json(result);
            }
            return null;
        }


        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        private string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(2, true));
            builder.Append(RandomNumber(0, 9));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
    }
}
