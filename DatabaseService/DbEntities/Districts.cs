using System;
using System.Collections.Generic;

namespace DatabaseService.DbEntities
{
    public partial class Districts
    {
        public int DistrictId { get; set; }
        public int RegionId { get; set; }
        public string DistrictName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
    }
}
