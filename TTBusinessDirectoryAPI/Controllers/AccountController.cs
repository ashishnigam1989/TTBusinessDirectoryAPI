using ApplicationService.IServices;
using CommonService.RequestModel;
using CommonService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Fluent;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TTBusinessDirectoryAPI.Extensions;

namespace TTBusinessDirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccount _account;
        protected Logger logger;
        IConfiguration _configuration;
        public AccountController(IAccount account, IConfiguration configuration)
        {
            _account = account;
            logger = LogManager.GetLogger("Test");
            _configuration= configuration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<GetResults> Login(LoginRequestModel login)
        {
            GetResults getResults = new GetResults();
            try
            {
                var hashedPassword = PasswordHelper.HashPassword(login.Password);
                login.Password = hashedPassword;
                var user = await _account.Login(login);
                if(user != null)
                {
                    var jwtAccessToken = GenerateAccessToken(login.EmailAddress);
                    var jwtRefreshToken = _configuration["Jwt:RefreshToken"];

                    getResults = new GetResults(true, "Login Successful", new LoginResponseModel { JwtAccessToken = jwtAccessToken, User = user, JwtRefreshToken = jwtRefreshToken}, 1);
                    logger.Info("Login Successful");
                    return getResults;
                }
                return new GetResults { IsSuccess = false, Message = "UnAuthorized", Data = null };
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
                return getResults;
            }
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
                getResults = _account.GetUsers(page, limit, "").Result;
                getResults.IsSuccess=true;
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


        [HttpPost]
        [Route("ApproveRejectUser")]
        public async Task<GetResults> ApproveRejectUser(ChangeStatusModel uModel)
        {
            GetResults getResults = new GetResults();
            try
            {
                var status = _account.ApproveRejectUser(uModel).Result;
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

        [HttpPost("Refresh/{refreshToken}/{userName}")]
        public async Task<GetResults> Refresh(string refreshToken, string userName)
        {
            GetResults getResults = new GetResults();

            if (ValidateRefreshToken(refreshToken))
            {
                var newAccessToken = GenerateAccessToken(userName);
                getResults.Message = "New AccessToken generated";
                getResults.Data = newAccessToken;
                return getResults;
            }

            getResults.IsSuccess = false;
            getResults.Message = "Invalid refresh token";
            return getResults;
        }


        #region private ,ethods for token generation

        private string GenerateAccessToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryInMinutes"])),
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            );

            var jwtAccessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtAccessToken;
        }

        private bool ValidateRefreshToken(string refreshToken)
        {
            return refreshToken == _configuration["Jwt:RefreshToken"];
        }

        #endregion
    }
}
