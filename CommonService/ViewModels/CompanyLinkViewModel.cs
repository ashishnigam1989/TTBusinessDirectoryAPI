using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels
{
    public class CompanyLinkViewModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string LinkNameEng { get; set; }
        public string LinkNameArb { get; set; }
        public string EnglishUrl { get; set; }
        public string ArabicUrl { get; set; }
        public string Target { get; set; }
        public int? SortOrder { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public bool? IsPublished { get; set; }
        public string CompanyName { get; set; }
    }
}
