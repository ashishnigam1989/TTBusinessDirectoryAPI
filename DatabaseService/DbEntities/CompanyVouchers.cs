using System;
using System.Collections.Generic;

namespace DatabaseService.DbEntities
{
    public partial class CompanyVouchers
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string VoucherNameEng { get; set; }
        public string VoucherNameArb { get; set; }
        public string VoucherShortDescriptionEng { get; set; }
        public string VoucherShortDescriptionArb { get; set; }
        public string VoucherDescriptionEng { get; set; }
        public string VoucherDescriptionArb { get; set; }
        public decimal OldPrice { get; set; }
        public decimal Price { get; set; }
        public DateTime VoucherDisplayDate { get; set; }
        public DateTime VoucherStartDate { get; set; }
        public DateTime VoucherEndDate { get; set; }
        public bool? IsPublished { get; set; }
        public int? SortOrder { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
    }
}
