using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels.Company
{
    public class CompanyAddressViewModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public string AddressDesc { get; set; }
        public string Contact { get; set; }
        public string Website { get; set; }
        public string GoogleLocation { get; set; }
    }
}
