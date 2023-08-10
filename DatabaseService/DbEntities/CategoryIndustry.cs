using System;
using System.Collections.Generic;

namespace DatabaseService.DbEntities
{
    public partial class CategoryIndustry
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public int IndustryId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
    }
}
