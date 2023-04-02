using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels
{
    public class UserSessionModel
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
