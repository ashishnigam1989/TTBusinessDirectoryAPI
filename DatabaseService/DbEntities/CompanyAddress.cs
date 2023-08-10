﻿using System;
using System.Collections.Generic;

namespace DatabaseService.DbEntities
{
    public partial class CompanyAddress
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public string AddressDesc { get; set; }
        public string Contact { get; set; }
        public string Website { get; set; }
        public string GoogleLocation { get; set; }
        public bool? IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long CreatorUserId { get; set; }
        public bool? IsPublished { get; set; }
    }
}
