using CommonService.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels.Company
{
    public class CompanyEventViewModel
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
        public string EventType { get; set; }
        public string CompanyName { get; set; }
        public bool IsPublished { get; set; }
        public string DisplayEventImage => string.Concat(CommonConstants.S3BaseURL + EventImage).Replace("com//", "com/");

    }
}
