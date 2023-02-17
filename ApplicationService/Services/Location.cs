using ApplicationService.IServices;
using AutoMapper;
using CommonService.RequestModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services
{
    public class Location : ILocation
    {
        private BusinessDirectoryDBContext _dbContext;
        private readonly IMapper _mapper;

        public Location(BusinessDirectoryDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public async Task<List<CountryModel>> GetCountries()
        {
            var countries = _dbContext.Country.ToListAsync().Result;
            var list = _mapper.Map<List<CountryModel>>(countries);
            return await Task.FromResult(list);
        }

        public Task<bool> AddRegion(RegionRequestModel regionRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditRegion(RegionRequestModel regionRequest)
        {
            throw new NotImplementedException();
        }


        public Task<RegionModel> GetRegionById(int regionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<RegionModel>> GetRegions(int countryId)
        {
            throw new NotImplementedException();
        }
    }
}
