using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public class FreeListingModel
    {
        public FreeListingModel() {
            FreeListingProductDetails = new List<FreeListingDetailsModel>();
        }
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public int? DistrictId { get; set; }
        public string Logo { get; set; }
        public string Pobox { get; set; }
        public int EmployeeNumber { get; set; }
        public string FounderName { get; set; }
        public string CompanyPhone { get; set; }
        public string PrimaryEmail { get; set; }
        public string PrimaryWebsite { get; set; }
        public string FoundedYear { get; set; }
        public long? CreatorUserId { get; set; }
        public int CountryId { get; set; }
        public int RegionId { get; set; }

        public List<FreeListingDetailsModel> FreeListingProductDetails { get; set; }
    }

    public class FreeListingDetailsModel
    {
        public long Id { get; set; }
        public string CategoryId { get; set; }
        public long FreeListingId { get; set; }
        public string RelatedProduct { get; set; }
        public long? CreatorUserId { get; set; }
        public string RelatedService { get; set; }
    }
}
