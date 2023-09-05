using CommonService.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels.Company
{
    public class CompanyBannerViewModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string BannerNameEng { get; set; }
        public string BannerNameArb { get; set; }
        public string EnglishUrl { get; set; }
        public string ArabicUrl { get; set; }
        public string ImageEng { get; set; }
        public string ImageArb { get; set; }
        public string Target { get; set; }
        public int? SortOrder { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? BannerExpiryDate { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? BannerStartDate { get; set; }
        //public string DisplayEngImage => string.Concat(CommonConstants.S3BaseURL + ImageEng).Replace("com//", "com/");
        public string DisplayEngImage => string.Concat(CommonConstants.S3BaseURL + ImageEng).Replace("com//", "com/");
        public string CompanyName { get; set; }
    }
}
