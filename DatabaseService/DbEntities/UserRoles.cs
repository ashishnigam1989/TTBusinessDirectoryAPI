using System;
using System.Collections.Generic;

namespace DatabaseService.DbEntities
{
    public partial class UserRoles
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
    }
}
