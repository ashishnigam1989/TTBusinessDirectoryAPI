﻿using CommonService.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace CommonService.ViewModels
{
    public class CompanyVideoViewModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string VideoNameEng { get; set; }
        public string VideoNameArb { get; set; }
        public string EnglishUrl { get; set; }
        public string ArabicUrl { get; set; }
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
