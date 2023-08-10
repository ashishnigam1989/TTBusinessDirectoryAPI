using System;
using System.Collections.Generic;

namespace DatabaseService.DbEntities
{
    public partial class MenuRolePermission
    {
        public int MenuPermissionId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public DateTime CreationTime { get; set; }
        public long CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
