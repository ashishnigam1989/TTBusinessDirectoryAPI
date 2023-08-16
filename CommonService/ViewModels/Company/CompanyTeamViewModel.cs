using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels.Company
{
    public class CompanyTeamViewModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string ProfilePic { get; set; }
    }
}
