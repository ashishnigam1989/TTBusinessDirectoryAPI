using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels.Company
{
    public class CompanyCategoryViewModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long CategoryId { get; set; }
        public bool? IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public string Company { get; set; }
        public string Category { get; set; }
    }
}
