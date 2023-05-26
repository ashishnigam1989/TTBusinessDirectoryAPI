using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels
{
    public class InsightModel
    {
        public int InsightId { get; set; }
        public int InsightTypeId { get; set; }
        public string InsightTitle { get; set; }
        public string InsightContent { get; set; }
        public string InsightImage { get; set; }
        public string EventUrl { get; set; }
        public string EventLocationUrl { get; set; }
        public string EventAddress { get; set; }
        public string EventInfo { get; set; }
    }
}
