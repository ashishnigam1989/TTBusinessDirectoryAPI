using System;
using System.Collections.Generic;

namespace DatabaseService.DbEntities
{
    public partial class CompanyReviewLike
    {
        public long Id { get; set; }
        public long UserComapnyReviewId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
    }
}
