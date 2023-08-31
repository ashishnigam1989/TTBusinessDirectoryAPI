using CommonService.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public class CompanyGalleryRequestModel
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public string YoutubeVideoUrl { get; set; }
        public string File { get; set; }
        public int SortOrder { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long CompanyMenuId { get; set; }
        public string TitleEng { get; set; }
        public string TitleArb { get; set; }
        public string ShortDescriptionEng { get; set; }
        public string ShortDescriptionArb { get; set; }
        public string DescriptionEng { get; set; }
        public string DescriptionArb { get; set; }
        public string Target { get; set; }
        public string TargetUrl { get; set; }
        public string DisplayImage => string.Format(CommonConstants.S3BaseURL + Image, Id).Replace("com//", "com/").Replace("Gallery//", "Gallery/");
        public string DisplayFile => string.Format(CommonConstants.S3BaseURL + File, Id).Replace("com//", "com/").Replace("Gallery//", "Gallery/");
        public string CompanyName { get; set; }
    }
}
