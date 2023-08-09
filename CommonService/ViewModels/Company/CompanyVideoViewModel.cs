using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels.Company
{
    public class CompanyVideoViewModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string VideoNameEng { get; set; }
        public string VideoNameArb { get; set; }
        public string EnglishUrl { get; set; }
        public string ArabicUrl { get; set; }
        public int? SortOrder { get; set; }
    }
}
