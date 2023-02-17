using ApplicationService.IServices;
using CommonService.RequestModel;
using CommonService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TTBusinessDirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccount _account;
        protected Logger logger;
        public AccountController(IAccount account)
        {
            _account = account;
            logger = LogManager.GetLogger("Test");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<GetResults> Login(LoginRequestModel login)
        {
            GetResults getResults = new GetResults();
            try
            {
                var user = _account.Login(login).Result;
                getResults = new GetResults(true, "Login Successful", user, 1);
                logger.Info("Login Successful");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetMenus/{roleId}")]
        public async Task<GetResults> GetMenus(int roleId)
        {
            GetResults getResults = new GetResults();
            try
            {
                var menus = _account.GetMenus(roleId).Result;
                var total = menus.Count;
                getResults = new GetResults(true, "Get Menus", menus, total);
                logger.Info("Get Menus");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }



        [HttpGet]
        [Route("GetUsers")]
        public async Task<GetResults> GetUsers(int page = 0, int limit = 10)
        {
            GetResults getResults = new GetResults();
            try
            {
                var users = _account.GetUsers(page, limit, "").Result;
                var total = users.Count;
                getResults = new GetResults(true, "Get users list", users.UserList, total);
                logger.Info("Get users list");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex);
            }
            return await Task.FromResult(getResults);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<GetResults> CreateUser(UserRequestModel userRequest)
        {
            GetResults getResults = new GetResults();
            try
            {
                var result = _account.CreateUser(userRequest).Result;
                getResults = new GetResults(result, "User creation successful", null, 1);
                logger.Info("User creation successful");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpPost]
        [Route("EditUser")]
        public async Task<GetResults> EditUser(UserRequestModel userRequest)
        {
            GetResults getResults = new GetResults();
            try
            {
                var result = _account.EditUser(userRequest).Result;
                getResults = new GetResults(result, "User modification successful", null, 1);
                logger.Info("User modification successful");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpPost]
        [Route("RoleMenuPermission")]
        public async Task<GetResults> RoleMenuPermission(RoleMenuMapping rolemenuRequest)
        {
            GetResults getResults = new GetResults();
            try
            {
                var result = _account.RoleMenuPermission(rolemenuRequest).Result;
                getResults = new GetResults(result, "Role menu assignment successful", null, 0);
                logger.Info("Role menu assignment successful");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }


        [HttpGet]
        [Route("GetUserById")]
        public async Task<GetResults> GetUserById(int userid)
        {
            GetResults getResults = new GetResults();
            try
            {
                var user = _account.GetUserById(userid).Result;
                var total = 1;
                getResults = new GetResults(true, "Get user detail", user, total);
                logger.Info("Get users list");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }


        [HttpGet]
        [Route("ApproveRejectUser")]
        public async Task<GetResults> ApproveRejectUser(int userid)
        {
            GetResults getResults = new GetResults();
            try
            {
                var status = _account.ApproveRejectUser(userid).Result;
                var total = 1;
                getResults = new GetResults(true, "Get status changed", status, total);
                logger.Info("Get users list");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

    }
}
