using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public class DistrictRequestModel
    {
        public int DistrictId { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int RegionId { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public int CreatedBy { get; set; }
    }
}
