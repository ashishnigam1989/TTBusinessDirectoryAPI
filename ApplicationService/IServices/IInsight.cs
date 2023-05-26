using CommonService.Enums;
using CommonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.IServices
{
    public interface IInsight
    {
        public Task<List<InsightModel>> GetInsights(EnumInsightType insightType);
    }
}
