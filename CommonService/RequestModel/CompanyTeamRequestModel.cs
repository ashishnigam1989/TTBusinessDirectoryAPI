using CommonService.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public class CompanyTeamRequestModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string ProfilePic { get; set; }
        public bool? IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long CreatorUserId { get; set; }
        public bool IsPublished { get; set; }
        public string DisplayProfilePic => string.Format(CommonConstants.S3BaseURL + ProfilePic, Id).Replace("com//", "com/");

    }
}
