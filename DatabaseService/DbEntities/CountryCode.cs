using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseService.DbEntities
{
    public partial class CountryCode
    {
        public int CountryCodeId { get; set; }
        public int CountryId { get; set; }
        public string CodeName { get; set; }
        public string CodeIcon { get; set; }
        public bool? IsActive { get; set; }
    }
}
