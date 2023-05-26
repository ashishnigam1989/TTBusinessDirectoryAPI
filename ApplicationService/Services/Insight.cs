using ApplicationService.IServices;
using AutoMapper;
using CommonService.Enums;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services
{
    public class Insight : IInsight
    {

        private BusinessDirectoryDBContext _dbContext;
        private readonly IMapper _mapper;

        public Insight(BusinessDirectoryDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public async Task<List<InsightModel>> GetInsights(EnumInsightType insightType)
        {
            var insights = await _dbContext.Insights.Where(w => !w.IsDeleted.Value && w.IsActivated.Value && w.InsightTypeId == (int)insightType)
                .ToListAsync();

            var insightModels = insights.Select(i => new InsightModel
            {
                InsightId = i.InsightId,
                EventAddress= i.EventAddress,
                EventInfo= i.EventInfo,
                EventLocationUrl= i.EventLocationUrl,   
                EventUrl= i.EventUrl,
                InsightContent= i.InsightContent,
                InsightImage= i.InsightImage,
                InsightTitle= i.InsightTitle,   
                InsightTypeId = i.InsightTypeId
            }).ToList();

            //var insightModels = _mapper.Map<List<InsightModel>>(insights);
            return insightModels;
        }
    }
}
