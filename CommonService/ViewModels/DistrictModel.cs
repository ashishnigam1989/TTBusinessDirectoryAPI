using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels
{
    public class DistrictModel
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public int CountryId { get; set; }
        public string RegionName { get; set; }
        public string DistrictName { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
