using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public  class BrandCategoryModel
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public List<int> Categories { get; set; }
        public int CreatedBy { get; set; }
    }
}
