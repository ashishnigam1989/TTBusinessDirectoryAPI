using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public class SearchRequestModel
    {
        public string CountryId { get; set; }
        public string SearchTerm { get; set; }
        public string RegionId { get; set; }
        public List<string> Types { get; set; }
        public string Sorting { get; set; }
        public string Verified { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
