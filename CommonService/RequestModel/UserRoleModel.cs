using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public class UserRoleModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime CreationTime { get; set; }
        public int CreatorUserId { get; set; }

    }
}
