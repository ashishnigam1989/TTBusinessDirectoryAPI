﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels
{
    public class CompanyFreeListingViewModel
    {
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public int? CountryId { get; set; }
        public int? RegionId { get; set; }
        public int? DistrictId { get; set; }
        public string Pobox { get; set; }
        public int FoundedYear { get; set; }
        public string FounderName { get; set; }
        public int? EmployeeNum { get; set; }
        public string PrimaryWebsite { get; set; }
        public string Logo { get; set; }
        public string PrimaryEmail { get; set; }
        public string CompanyPhone { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public bool? IsActive { get; set; }
    }
}
