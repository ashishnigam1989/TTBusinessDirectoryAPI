using System;
using System.Collections.Generic;

namespace DatabaseService.DbEntities
{
    public partial class Designations
    {
        public int Id { get; set; }
        public string DesignationName { get; set; }
        public string DesignationDesc { get; set; }
        public bool? IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long CreatorUserId { get; set; }
        public bool? IsPublished { get; set; }
    }
}
