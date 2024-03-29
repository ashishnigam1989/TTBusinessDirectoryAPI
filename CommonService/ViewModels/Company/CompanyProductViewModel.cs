﻿using CommonService.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels.Company
{
    public class CompanyProductViewModel
    {
        public long Id { get; set; }
        public string NameEng { get; set; }
        public string NameArb { get; set; }
        public long CompanyId { get; set; }
        public string ShortDescriptionEng { get; set; }
        public string ShortDescriptionArb { get; set; }
        public string DescriptionEng { get; set; }
        public string DescriptionArb { get; set; }
        public string PartNumber { get; set; }
        public string WarrantyEng { get; set; }
        public string WarrantyArb { get; set; }
        public string Image { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsPublished { get; set; }
        public bool? HasOffers { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public decimal Price { get; set; }
        public string OffersDescriptionEng { get; set; }
        public string OffersDescriptionArb { get; set; }
        public int? CountryId { get; set; }
        public DateTime? OfferStartDate { get; set; }
        public DateTime? OfferEndDate { get; set; }
        public string OfferShortDescriptionEng { get; set; }
        public string OfferShortDescriptionArb { get; set; }
        public decimal OldPrice { get; set; }
        public string Company { get; set; }

        public string DisplayLogo => string.Concat(CommonConstants.S3BaseURL + Image);
    }
}
