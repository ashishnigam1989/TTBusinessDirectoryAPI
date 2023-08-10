using CommonService.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public class CompanyOffersRequestModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string OfferNameEng { get; set; }
        public string OfferNameArb { get; set; }
        public string OfferShortDescriptionEng { get; set; }
        public string OfferShortDescriptionArb { get; set; }
        public string OfferDescriptionEng { get; set; }
        public string OfferDescriptionArb { get; set; }
        public decimal OldPrice { get; set; }
        public decimal Price { get; set; }
        public DateTime? OfferDisplayDate { get; set; }
        public DateTime? OfferStartDate { get; set; }
        public DateTime? OfferEndDate { get; set; }
        public int? SortOrder { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public bool IsPublished { get; set; }
        public string Image { get; set; }
        public string CompanyName { get; set; }
        public string DisplayImage => string.Format(CommonConstants.S3BaseURL + Image, Id).Replace("com//", "com/");
    }
}
