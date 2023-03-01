using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseService.DbEntities
{
    public partial class Insights
    {
        public int InsightId { get; set; }
        public int InsightTypeId { get; set; }
        public string InsightTitle { get; set; }
        public string InsightContent { get; set; }
        public string InsightImage { get; set; }
        public int? MainLevelId { get; set; }
        public DateTime? StartDate { get; set; }
        public string StartTime { get; set; }
        public DateTime? EndDate { get; set; }
        public string EndTime { get; set; }
        public int? EventTypeId { get; set; }
        public int? PassTypeId { get; set; }
        public int? RegionId { get; set; }
        public string EventUrl { get; set; }
        public string EventLocationUrl { get; set; }
        public string EventAddress { get; set; }
        public string EventInfo { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsActivated { get; set; }
        public bool? IsDeleted { get; set; }
        public string IpAddress { get; set; }
    }
}
