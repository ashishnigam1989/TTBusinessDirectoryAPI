using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseService.DbEntities
{
    public partial class FreeListing
    {
        public long Id { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMobile { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string Logo { get; set; }
        public string Pobox { get; set; }
        public int? RegionId { get; set; }
        public string CompanyPhone { get; set; }
        public string Fax { get; set; }
        public string PrimaryEmail { get; set; }
        public string PrimaryWebsite { get; set; }
        public string Iso { get; set; }
        public int EstablishmentYear { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
    }
}
