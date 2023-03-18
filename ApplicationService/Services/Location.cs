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
    public class Location : ILocation
    {
        private BusinessDirectoryDBContext _dbContext;
        private readonly IMapper _mapper;

        public Location(BusinessDirectoryDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        #region Country
        public async Task<List<CountryModel>> GetMasterCountries()
        {
            var countries = _dbContext.Country
                .Where(w => w.IsDeleted == false)
                .Select(s => new CountryModel
                {
                    Id = s.Id,
                    CountryCode = s.CountryCode,
                    CountryNameEng = s.CountryNameEng,
                    CurrencyCode = s.CurrencyCode
                }).ToList();
            return await Task.FromResult(countries);
        }

        public async Task<GetResults> GetCountries(int page, int limit, string searchValue)
        {
            int total = 0;
            List<CountryModel> countrylist = _dbContext.Country.Where(w =>
           (!string.IsNullOrEmpty(searchValue) ? w.CountryNameEng.ToLower().Contains(searchValue.ToLower()) : w.CountryNameEng == w.CountryNameEng) && w.IsDeleted == false
            ).Select(s => new CountryModel
            {
                Id = s.Id,
                CountryCode = s.CountryCode,
                CountryNameEng = s.CountryNameEng,
                CurrencyCode = s.CurrencyCode
            }).Distinct().OrderByDescending(o => o.Id).Skip(limit * page).Take(limit).ToListAsync().Result;

            total = _dbContext.Country.Where(w => w.IsDeleted == false).Where(w =>
                                    !string.IsNullOrEmpty(searchValue) ? w.CountryNameEng.ToLower().Contains(searchValue.ToLower()) : w.CountryNameEng == w.CountryNameEng
                                    ).CountAsync().Result;

            GetResults uobj = new GetResults
            {
                Total = total,
                Data = countrylist
            };
            return await Task.FromResult(uobj);
        }
        #endregion

        #region Region
        public async Task<GetResults> AddUpdateRegion(RegionRequestModel regionRequest)
        {
            GetResults gobj = new GetResults();
            var region = _dbContext.Region.Where(w => w.Id == regionRequest.Id && w.IsDeleted == false).FirstOrDefaultAsync().Result;
            if (region == null)
            {
                Region cobj = new Region()
                {
                    NameEng = regionRequest.NameEng,
                    CountryId = regionRequest.CountryId,
                    IsDeleted = false,
                    CreationTime = DateTime.Now,
                    CreatorUserId = regionRequest.CreatorUserId,

                };
                _dbContext.Region.Add(cobj);

                gobj = new GetResults()
                {
                    IsSuccess = true,
                    Message = "Region Saved Successfully"
                };
            }
            else
            {
                region.NameEng = regionRequest.NameEng;
                region.CountryId = regionRequest.CountryId;
                region.IsDeleted = false;
                region.LastModificationTime = DateTime.Now;
                region.LastModifierUserId = regionRequest.LastModifierUserId;
                gobj = new GetResults()
                {
                    IsSuccess = true,
                    Message = "Region Updated Successfully"
                };
            }
            try
            {
                var i = _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }

            return await Task.FromResult(gobj);
        }

        public async Task<GetResults> DeleteRegion(int id)
        {
            GetResults gobj = new GetResults();
            var region = _dbContext.Region.Where(w => w.Id == id).FirstOrDefaultAsync().Result;
            if (region != null)
            {
                region.IsDeleted = true;
                region.DeletionTime = DateTime.Now;
                _dbContext.SaveChanges();
                gobj = new GetResults()
                {
                    IsSuccess = true,
                    Message = "Region Deleted Successfully."
                };
            }
            else
            {
                gobj = new GetResults()
                {
                    IsSuccess = false,
                    Message = "Region Details Not Found."
                };
            }
            return await Task.FromResult(gobj);
        }

        public async Task<GetResults> GetRegionById(int regionId)
        {
            var region = _dbContext.Region
                .Join(_dbContext.Country,
                    region => region.CountryId,
                    country => country.Id,
                    (region, country) => new { Region = region, Country = country })
                .Where(w =>w.Region.IsDeleted == false && w.Region.Id == regionId
                ).Select(s => new RegionModel
                {
                    Id = s.Region.Id,
                    NameEng = s.Region.NameEng,
                    CountryId = s.Region.CountryId,
                    CountryNameEng = s.Country.CountryNameEng
                }).FirstOrDefault();

            GetResults uobj = new GetResults
            {
                Total = 1,
                Data = region
            };
            return await Task.FromResult(uobj);
        }

        public async Task<List<RegionModel>> GetMasterRegions(int countryId)
        {
            var regions = _dbContext.Region
                .Where(w => w.IsDeleted == false)
                .Select(s => new RegionModel
                {
                    Id = s.Id,
                    NameEng = s.NameEng
                }).ToList();
            return await Task.FromResult(regions);
        }

        public async Task<GetResults> GetRegions(int page, int limit, string searchValue)
        {
            int total = 0;
            var regionList = _dbContext.Region
                .Join(_dbContext.Country,
                    region=>region.CountryId,
                    country=>country.Id,
                    (region, country) => new { Region = region, Country = country })
                .Where(w=> 
               (!string.IsNullOrEmpty(searchValue) ? w.Region.NameEng.ToLower().Contains(searchValue.ToLower()) : w.Region.NameEng == w.Region.NameEng) &&
               (!string.IsNullOrEmpty(searchValue) ? w.Country.CountryNameEng.ToLower().Contains(searchValue.ToLower()) : w.Country.CountryNameEng == w.Country.CountryNameEng) && 
               w.Region.IsDeleted == false
                ).Select(s => new RegionModel
                {
                    Id = s.Region.Id,
                    NameEng = s.Region.NameEng,
                    CountryId = s.Region.CountryId,
                    CountryNameEng = s.Country.CountryNameEng
                }).Distinct().OrderByDescending(o => o.Id).Skip(limit * page).Take(limit).ToListAsync().Result;

            total = _dbContext.Region
                .Join(_dbContext.Country,
                    region => region.CountryId,
                    country => country.Id,
                    (region, country) => new { Region = region, Country = country })
                .Where(w =>
               (!string.IsNullOrEmpty(searchValue) ? w.Region.NameEng.ToLower().Contains(searchValue.ToLower()) : w.Region.NameEng == w.Region.NameEng) &&
               (!string.IsNullOrEmpty(searchValue) ? w.Country.CountryNameEng.ToLower().Contains(searchValue.ToLower()) : w.Country.CountryNameEng == w.Country.CountryNameEng) &&
               w.Region.IsDeleted == false).CountAsync().Result;

            GetResults uobj = new GetResults
            {
                Total = total,
                Data = regionList
            };
            return await Task.FromResult(uobj);
        }
        #endregion
    }
}
