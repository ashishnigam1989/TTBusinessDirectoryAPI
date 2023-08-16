using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels.Company
{
    public class CompanyNewsArticleViewModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDesc { get; set; }
        public string NewsUrl { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
