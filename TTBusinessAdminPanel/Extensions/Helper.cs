using CommonService.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace TTBusinessAdminPanel.Extensions
{
    public static class ExtensionHelper
    {
        private static IHttpContextAccessor httpContextAccessor;
        public static void Help(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }
        public static void SetUserSession(UserModel Userdetails)
        {
            var context = httpContextAccessor.HttpContext;

            var userObj = new UserSessionModel()
            {
                UserId = Userdetails.Id,
                Name = Userdetails.Name,
                EmailAddress = Userdetails.EmailAddress,
                RoleName = Userdetails.Rolename,
                RoleId = Convert.ToInt32(Userdetails.RoleId)

            };
            string jsonString = JsonConvert.SerializeObject(userObj);

            context.Session.SetString("UserDetail", jsonString);

        }

        public static void SetSession(string key, string value)
        {
            var context = httpContextAccessor.HttpContext;

            context.Session.SetString(key, value);

        }

        public static string GetSession(string key)
        {
            var context = httpContextAccessor.HttpContext;

            return Convert.ToString(context.Session.GetString(key));

        }
        public static UserSessionModel GetUserSession()
        {

            UserSessionModel obj = null;
            var context = httpContextAccessor.HttpContext;
            var UserDetailjson = context.Session.GetString("UserDetail");
            if (UserDetailjson != null)
            {
                obj = JsonConvert.DeserializeObject<UserSessionModel>(UserDetailjson);
            }
            return obj;

        }
    }


}
