using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels
{
    public class CountryModel
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryNameEng { get; set; }
        public string CurrencyCode { get; set; }
    }

    public class CountryCodeModel
    {
        public int CountryCodeId { get; set; }
        public int CountryId { get; set; }
        public string CodeName { get; set; }
        public string CodeIcon { get; set; }
    }
}
