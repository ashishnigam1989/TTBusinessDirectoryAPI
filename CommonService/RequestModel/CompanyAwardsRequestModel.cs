using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public class CompanyAwardsRequestModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string AwardTitle { get; set; }
        public string AwardDesc { get; set; }
        public string AwardFile { get; set; }
        public bool? IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long CreatorUserId { get; set; }
        public bool IsPublished { get; set; }
    }
}
