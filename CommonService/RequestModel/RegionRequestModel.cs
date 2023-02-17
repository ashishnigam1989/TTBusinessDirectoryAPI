using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public class RegionRequestModel
    {
        public int Id { get; set; }
        public string NameEng { get; set; }
        public string NameArb { get; set; }
        public int CountryId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
    }
}
