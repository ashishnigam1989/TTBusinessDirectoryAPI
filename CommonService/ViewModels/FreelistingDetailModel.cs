using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels
{
    public class FreelistingDetailModel
    {
        public long Id { get; set; }
        public long FreeListingId { get; set; }
        public string CompanyName { get; set; }
        public string CategoryName { get; set; }
        public long? CategoryId { get; set; }
        public string RelatedProduct { get; set; }
        public string RelatedService { get; set; }
        public string Brand { get; set; }
    }
}
