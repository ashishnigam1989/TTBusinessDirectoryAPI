using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels.Company
{
    //public class CompanyModel
    //{
    //    public long Id { get; set; }
    //    public string NameEng { get; set; }
    //    public string NameArb { get; set; }
    //    public string ShortDescriptionEng { get; set; }
    //    public string ShortDescriptionArb { get; set; }
    //    public string PrimaryPhone { get; set; }
    //    public string PrimaryEmail { get; set; }
    //    public string PrimaryWebsite { get; set; }
    //    public bool? IsVerified { get; set; }
    //    public Guid? VerifiedUserId { get; set; }
    //    public DateTime? VerifiedTime { get; set; }
    //    public bool? IsGreen { get; set; }
    //    public string FacebookUrl { get; set; }
    //    public string LinkedInUrl { get; set; }
    //    public string TwitterUrl { get; set; }
    //    public string GooglePlusUrl { get; set; }
    //    public string InstagramUrl { get; set; }
    //    public bool? HasOffers { get; set; }
    //    public DateTime? OfferUpdatedTime { get; set; }
    //    public bool? HasCoupons { get; set; }
    //    public DateTime? CouponUpdatedTime { get; set; }
    //    public bool? HasVideos { get; set; }
    //    public string PrimaryGpsLocation { get; set; }
    //    public string DescriptionEng { get; set; }
    //    public string DescriptionArb { get; set; }
    //    public string Logo { get; set; }
    //    public string TradeLicenceNumber { get; set; }
    //    public string MetaKeywords { get; set; }
    //    public string MetaDescription { get; set; }
    //    public string MetaTitle { get; set; }
    //    public string GooglePlaystoreUrl { get; set; }
    //    public string AppleStoreUrl { get; set; }
    //    public string WindowsStoreUrl { get; set; }
    //    public string BlackBerryStoreUrl { get; set; }
    //    public decimal OverallRating { get; set; }
    //    public long TotalReviews { get; set; }
    //    public int RegionId { get; set; }
    //    public string UniqueName { get; set; }
    //    public string BrochureLink { get; set; }
    //    public string ThemeColor { get; set; }
    //    public bool IsDeleted { get; set; }
    //    public long? DeleterUserId { get; set; }
    //    public DateTime? DeletionTime { get; set; }
    //    public DateTime? LastModificationTime { get; set; }
    //    public long? LastModifierUserId { get; set; }
    //    public DateTime CreationTime { get; set; }
    //    public long? CreatorUserId { get; set; }
    //    public bool? IsPublished { get; set; }
    //    public string DomainName { get; set; }
    //    public string PrimaryMobile { get; set; }
    //    public string Address { get; set; }
    //    public string Pobox { get; set; }
    //    public string PrimaryFax { get; set; }
    //    public long TotalProfileViews { get; set; }
    //    public string Iso { get; set; }
    //    public DateTime? EstablishmentDate { get; set; }
    //    public bool? IsFeatured { get; set; }
    //}

    public class CompanyModel
    {
        public long id { get; set; }
        public string NameEng { get; set; }
        public string NameArb { get; set; }
        public string PrimaryPhone { get; set; }
        public string PrimaryEmail { get; set; }
        public bool? IsVerified { get; set; }
        public DateTime? EstablishmentDate { get; set; }
        public string PrimaryWebsite { get; set; }
        public string Logo { get; set; }


    }

    public class CompanyDetailModel
    {
        public CompanyDetailModel()
        {
            CompanyProducts = new List<CompanyProductViewModel>();
            CompanyServices = new List<CompanyServiceViewModel>();
            CompanyBrands = new List<CompanyBrandViewModel>();
            CompanyTeams = new List<CompanyTeamViewModel>();
            CompanyAwards = new List<CompanyAwardViewModel> ();
            CompanyAddresses = new List<CompanyAddressViewModel>();
            CompanyEvents = new List<CompanyEventViewModel>();
            CompanyNewsArticles = new List<CompanyNewsArticleViewModel>();
            CompanyVideos = new List<CompanyVideoViewModel>();
            CompanyTags= new List<CompanyTagViewModel>();
            CompanyBanners = new List<CompanyBannerViewModel>();
            CompanyKeywords = new List<CompanyKeywordsViewModel>();
        }
        public long Id { get; set; }
        public string NameEng { get; set; }
        public string NameArb { get; set; }
        public string PrimaryPhone { get; set; }
        public string PrimaryEmail { get; set; }
        public string ShortDescriptionEng { get; set; }
        public string DescriptionEng { get; set; }
        public DateTime? EstablishmentDate { get; set; }
        public string PrimaryWebsite { get; set; }
        public string Logo { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public decimal OverallRating { get; set; }
        public long TotalReviews { get; set; }
        public string PrimaryFax { get; set; }
        public bool? IsVerified { get; set; }
        public string FacebookUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string GooglePlusUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string MetaTitle { get; set; }
        public List<CompanyProductViewModel> CompanyProducts { get; set; }
        public List<CompanyServiceViewModel> CompanyServices { get; set; }
        public List<CompanyBrandViewModel> CompanyBrands { get; set; }
        public List<CompanyTeamViewModel> CompanyTeams { get; set; }
        public List<CompanyAwardViewModel> CompanyAwards { get; set; }
        public List<CompanyAddressViewModel> CompanyAddresses { get; set; }
        public List<CompanyEventViewModel> CompanyEvents { get; set; }
        public List<CompanyNewsArticleViewModel> CompanyNewsArticles { get; set; }
        public List<CompanyVideoViewModel> CompanyVideos { get; set; }
        public List<CompanyTagViewModel> CompanyTags { get; set; }
        public List<CompanyKeywordsViewModel> CompanyKeywords { get; set; }

        public List<CompanyBannerViewModel> CompanyBanners { get; set; }

    }
}
