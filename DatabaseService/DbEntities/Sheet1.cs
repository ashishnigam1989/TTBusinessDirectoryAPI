using System;
using System.Collections.Generic;

namespace DatabaseService.DbEntities
{
    public partial class Sheet1
    {
        public double? MenuId { get; set; }
        public double? ParentId { get; set; }
        public string MenuName { get; set; }
        public string ComponentName { get; set; }
        public string MenuPath { get; set; }
        public string MenuIcon { get; set; }
        public double? Sequence { get; set; }
        public double? IsActivated { get; set; }
    }
}
