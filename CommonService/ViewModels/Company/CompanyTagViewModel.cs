using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels.Company
{
    public class CompanyTagViewModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string TagName { get; set; }
    }
}
