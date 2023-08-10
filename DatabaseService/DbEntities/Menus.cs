using System;
using System.Collections.Generic;

namespace DatabaseService.DbEntities
{
    public partial class Menus
    {
        public int MenuId { get; set; }
        public int ParentId { get; set; }
        public string MenuName { get; set; }
        public string ComponentName { get; set; }
        public string MenuPath { get; set; }
        public string MenuIcon { get; set; }
        public int? Sequence { get; set; }
        public bool IsActivated { get; set; }
        public DateTime CreationTime { get; set; }
        public long CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}
