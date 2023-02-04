﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseService.DbEntities
{
    public partial class CompanyDynamicMenu
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public int DynamicMenuId { get; set; }
        public string DescriptionEng { get; set; }
        public string DescriptionArb { get; set; }
        public int? SortOrder { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
    }
}
