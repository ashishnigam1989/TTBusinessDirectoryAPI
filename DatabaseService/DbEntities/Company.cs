using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseService.DbEntities
{
    public partial class Company
    {
        public long Id { get; set; }
        public string NameEng { get; set; }
        public string NameArb { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public int? DistrictId { get; set; }
        public string Pobox { get; set; }
        public int? FoundedYear { get; set; }
        public string FounderName { get; set; }
        public int? EmployeeNum { get; set; }
        public DateTime? EstablishmentDate { get; set; }
        public string PrimaryWebsite { get; set; }
        public string Logo { get; set; }
        public string PrimaryEmail { get; set; }
        public string PrimaryPhone { get; set; }
        public string ShortDescriptionEng { get; set; }
        public string ShortDescriptionArb { get; set; }
        public bool? IsVerified { get; set; }
        public Guid? VerifiedUserId { get; set; }
        public DateTime? VerifiedTime { get; set; }
        public bool? IsGreen { get; set; }
        public string FacebookUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string GooglePlusUrl { get; set; }
        public string InstagramUrl { get; set; }
        public bool? HasOffers { get; set; }
        public DateTime? OfferUpdatedTime { get; set; }
        public bool? HasCoupons { get; set; }
        public DateTime? CouponUpdatedTime { get; set; }
        public bool? HasVideos { get; set; }
        public string PrimaryGpsLocation { get; set; }
        public string DescriptionEng { get; set; }
        public string DescriptionArb { get; set; }
        public string TradeLicenceNumber { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string GooglePlaystoreUrl { get; set; }
        public string AppleStoreUrl { get; set; }
        public string WindowsStoreUrl { get; set; }
        public string BlackBerryStoreUrl { get; set; }
        public decimal OverallRating { get; set; }
        public long TotalReviews { get; set; }
        public string UniqueName { get; set; }
        public string BrochureLink { get; set; }
        public string ThemeColor { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public bool? IsPublished { get; set; }
        public string DomainName { get; set; }
        public string PrimaryMobile { get; set; }
        public string PrimaryFax { get; set; }
        public long TotalProfileViews { get; set; }
        public string Iso { get; set; }
        public bool? IsFeatured { get; set; }
    }
}
