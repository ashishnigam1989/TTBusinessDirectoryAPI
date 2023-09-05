using CommonService.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public class CompanyNewsArticleRequestModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDesc { get; set; }
        public string NewsUrl { get; set; }
        public bool? IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long CreatorUserId { get; set; }
        public bool IsPublished { get; set; }
        public string DisplayNewsImage=> string.Concat(CommonConstants.S3BaseURL + NewsUrl).Replace("com//", "com/");
        public string CompanyName { get; set; }

    }
}
