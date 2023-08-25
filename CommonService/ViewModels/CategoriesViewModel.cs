using CommonService.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels
{
    public partial class CategoriesViewModel
    {
        public long Id { get; set; }
        public string NameEng { get; set; }
        public string NameArb { get; set; }
        public string Unspsccode { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public string Keywords { get; set; }
        public long SuggestionHits { get; set; }
        public string Slug { get; set; }
        public bool SeoEnabled { get; set; }
        public string MetaTitleEng { get; set; }
        public string MetaDescriptionEng { get; set; }
        public string PageContentEng { get; set; }
        public string MetaTitleArb { get; set; }
        public string MetaDescriptionArb { get; set; }
        public string PageContentArb { get; set; }
        public bool IsFeatured { get; set; }
        public string Icon { get; set; }
        public string DisplayIcon => string.Concat(CommonConstants.S3BaseURL, Icon);
        public bool IsSelected { get; set; }
    }
}
