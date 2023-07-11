using ApplicationService.IServices;
using AspNetCoreHero.ToastNotification.Abstractions;
using CommonService.RequestModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Text;

namespace TTBusinessAdminPanel.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private Logger _logger;
        private IAccount _account;
        private IMaster _master;
        private readonly INotyfService _notyfService;
        private int LoggedInUser = 0;
        public UserController( IAccount account, IMaster master, INotyfService notyfService)
        {
            _logger = LogManager.GetLogger("User");
            _account = account;
            _master = master;
            _notyfService = notyfService;
         //   LoggedInUser = User.Claims.Where(w => w.Type == ClaimTypes.PrimarySid).Select(s => Convert.ToInt32(s.Value)).FirstOrDefault();
        }

        public IActionResult Index()
        {
            _logger.Info("Opening user list page.");
            return View();
        }

        [HttpPost]
        public IActionResult LoadUser()
        {
            try
            {
                _logger.Info("Loading user list.");
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
                recordsTotal = allData.Total;
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = userData };

                _logger.Info("User list loaded.");
                return Ok(jsonData);

            }
            catch(Exception ex)
            {
                _logger.Error(ex);  

            }
            return Ok(null);
        }

        public IActionResult Add()
        {
            try
            {
                _logger.Info("Add User Page Open.");
                BindRoles();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message.ToString());
            }
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserRequestModel userRequest)
        {
            try
            {
                _logger.Info("Add user detail submitted->", JsonConvert.SerializeObject(userRequest));
                if (ModelState.IsValid)
                {

                    userRequest.Password = RandomPassword();
                    userRequest.UserName = userRequest.EmailAddress;
                    userRequest.CreatorUserId = LoggedInUser;
                    var isadded = _account.CreateUser(userRequest).Result;
                    if (isadded)
                    {
                        _logger.Info("User added succussfully.");
                        _notyfService.Success("User added successfully !!!");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        _logger.Info("User adding failed.");
                        _notyfService.Error("User creation failed !!!");
                    }
                }
                else
                {
                    _logger.Info("Add user request model validation failed.");
                    _notyfService.Error("Validation Error !!!");
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message.ToString());
            }
            return View(userRequest);
        }

        public IActionResult Edit(int id)
        {
            UserRequestModel umodel = new UserRequestModel();
            try
            {
                _logger.Info("Getting user data for edit. UserId->"+id);
                var user = _account.GetUserById(id).Result;
                if (user != null)
                {
                    umodel = new UserRequestModel()
                    {
                        Name = user.Name,
                        EmailAddress = user.EmailAddress,
                        Mobile = user.Mobile,
                        RoleId = user.RoleId,
                        Designation=user.Designation
                    };
                }
                BindRoles();
                _logger.Info("User detail found.");
            }
            catch(Exception ex)
            {
                _logger.Info("Exception while getting user details. Userid:-"+id);
                _logger.Error(ex);
            }
            return View(umodel);
        }

        public IActionResult RoleMenuMap()
        {
            BindRoles(); 
            return View();
        }

        private void BindRoles()
        {
            try
            {
                List<RoleModel> roles = new List<RoleModel>();
                roles = _master.GetMasterRoles().Result;
                ViewBag.Roles = new SelectList(roles, "Id", "DisplayName");
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
            }
        }

        [HttpGet]
        public IActionResult BindMenus(int id)
        {
            List<MenuModel> allmenus = new List<MenuModel>();
            try
            {
                allmenus = _master.GetAllMenus().Result;
                var menus = _account.GetMenus(id).Result.Select(s => s.MenuId).ToList();
                if (menus.Count > 0)
                    allmenus.Where(w => menus.Contains(w.MenuId)).ToList().ForEach(f => f.IsSelected = true);
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
            }
            return Json(allmenus);
        }

        [HttpPost]
        public IActionResult ApproveRejectUser(ChangeStatusModel uModel)
        {
            bool isupdated = false;
            try
            {
                isupdated = _account.ApproveRejectUser(uModel).Result;
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
            }
            return Json(isupdated);
        }

        [HttpPost]
        public IActionResult AssignMenusToRole(RoleMenuMapping rolemenuRequest)
        {
            try
            {
                if (ModelState.IsValid && rolemenuRequest.RoleId != 0)
                {
                    var result = _account.RoleMenuPermission(rolemenuRequest).Result;
                    return Json(result);
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
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

        [HttpPost]
        public IActionResult DeleteUser(int userid)
        {
            bool isupdated = false;
            try
            {
                _logger.Info("Deleting user for userid->"+userid);
                isupdated = _account.DeleteUser(userid,LoggedInUser).Result;
                _logger.Info("User deletion->"+isupdated);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return Json(isupdated);
        }
    }
}
