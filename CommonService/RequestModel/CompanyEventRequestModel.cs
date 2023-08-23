using CommonService.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public class CompanyEventRequestModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string EventTitle { get; set; }
        public string EventDesc { get; set; }
        public string EventImage { get; set; }
        public DateTime? StartDate { get; set; }
        public string StartTime { get; set; }
        public DateTime? EndDate { get; set; }
        public string EndTime { get; set; }
        public string EventUrl { get; set; }
        public string EventLocationUrl { get; set; }
        public int? EventTypeId { get; set; }
        public bool? IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long CreatorUserId { get; set; }
        public bool? IsPublished { get; set; }
        public string DisplayEventImage => string.Format(CommonConstants.S3BaseURL + EventImage, Id).Replace("com//", "com/");

    }
}
