using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels.Company
{
    public class CompanyAwardViewModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string AwardTitle { get; set; }
        public string AwardDesc { get; set; }
        public string AwardFile { get; set; }
    }
}
