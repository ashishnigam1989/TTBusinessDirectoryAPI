using CommonService.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace TTBusinessAdminPanel.Extensions
{
    public static class Helper
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
