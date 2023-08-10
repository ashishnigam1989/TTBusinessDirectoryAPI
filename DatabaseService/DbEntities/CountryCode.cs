using System;
using System.Collections.Generic;

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
