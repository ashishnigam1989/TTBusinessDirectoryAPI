using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseService.DbEntities
{
    public partial class Country
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryNameEng { get; set; }
        public string CountryNameArb { get; set; }
        public string CurrencyCode { get; set; }
        public string Population { get; set; }
        public string FipsCode { get; set; }
        public string IsoNumeric { get; set; }
        public string North { get; set; }
        public string South { get; set; }
        public string East { get; set; }
        public string West { get; set; }
        public string Capital { get; set; }
        public string ContinentNameEng { get; set; }
        public string ContinentNameArb { get; set; }
        public string Continent { get; set; }
        public string AreaInSqKm { get; set; }
        public string IsoAlpha3 { get; set; }
        public int GeonameId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
    }
}
