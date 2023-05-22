using ApplicationService.IServices;
using AutoMapper;
using CommonService.RequestModel;
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
    public class Categories : ICategories
    {
        private BusinessDirectoryDBContext _dbContext;
        private readonly IMapper _mapper;
        public Categories(BusinessDirectoryDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public async Task<GetResults> GetAllCategories(bool isFeatured = true)
        {
            int total = 0;
            var categories = await _dbContext.Category.Where(w => w.IsPublished.Value && w.IsFeatured.Equals(isFeatured)).Select(s => new CategoriesViewModel
            {
                Id = s.Id,
                NameEng = s.NameEng,
                NameArb = s.NameArb,
                IsFeatured = isFeatured,
                Icon = s.Icon,
            })
            .Distinct()
            .OrderBy(o => o.NameEng)
            .ToListAsync();

            GetResults uobj = new GetResults
            {
                Total = total,
                Data = categories
            };
            return await Task.FromResult(uobj);

        }
    }
}
