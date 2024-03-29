﻿using ApplicationService.IServices;
using AutoMapper;
using CommonService.Constants;
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
            var countries = _dbContext.Country.Join(_dbContext.CountryCode, c => c.Id, cc => cc.CountryId, (c, cc) => new { c, cc })
                .Where(w => w.c.IsDeleted == false)
                .Select(s => new CountryModel
                {
                    Id = s.c.Id,
                    CountryCode = s.c.CountryCode,
                    CountryNameEng = s.c.CountryNameEng,
                    CurrencyCode = s.c.CurrencyCode,
                    CountryCodeNumber = s.cc.CodeName

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
                var regionValid = _dbContext.Region.Where(w => w.NameEng == regionRequest.NameEng && w.CountryId == regionRequest.CountryId && w.IsDeleted == false).FirstOrDefaultAsync().Result;
                if (regionValid == null)
                {
                    Region cobj = new Region()
                    {
                        NameEng = regionRequest.NameEng,
                        NameArb = regionRequest.NameEng,
                        CountryId = regionRequest.CountryId,
                        SortOrder = 99,
                        IsDeleted = false,
                        CreationTime = DateTime.Now,
                        CreatorUserId = CommonConstants.LoggedInUser,

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
                    gobj = new GetResults()
                    {
                        IsSuccess = false,
                        Message = regionRequest.NameEng + " is already exists !!!"
                    };
                }
            }
            else
            {
                var regionValid = _dbContext.Region.Where(w => w.NameEng == regionRequest.NameEng && w.CountryId == regionRequest.CountryId && w.Id != regionRequest.Id && w.IsDeleted == false).FirstOrDefaultAsync().Result;
                if (regionValid == null)
                {
                    region.NameEng = regionRequest.NameEng;
                    region.NameArb = regionRequest.NameEng;
                    region.CountryId = regionRequest.CountryId;
                    region.SortOrder = 99;
                    region.IsDeleted = false;
                    region.LastModificationTime = DateTime.Now;
                    region.LastModifierUserId = CommonConstants.LoggedInUser;
                    gobj = new GetResults()
                    {
                        IsSuccess = true,
                        Message = "Region Updated Successfully"
                    };
                }
                else
                {
                    gobj = new GetResults()
                    {
                        IsSuccess = false,
                        Message = regionRequest.NameEng + " is already exists !!!"
                    };
                }
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
                region.DeleterUserId = CommonConstants.LoggedInUser;
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
                .Where(w => w.Region.IsDeleted == false && w.Region.Id == regionId
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
                .Where(w => !w.IsDeleted && w.CountryId == countryId)
                .Select(s => new RegionModel
                {
                    Id = s.Id,
                    NameEng = s.NameEng,
                    CountryId = s.CountryId
                }).ToList();
            return await Task.FromResult(regions);
        }

        public async Task<GetResults> GetRegions(int page, int limit, string searchValue)
        {
            int total = 0;
            var regionList = _dbContext.Region
                .Join(_dbContext.Country,
                    region => region.CountryId,
                    country => country.Id,
                    (region, country) => new { Region = region, Country = country })
                .Where(w =>
               (!string.IsNullOrEmpty(searchValue) ? w.Region.NameEng.ToLower().Contains(searchValue.ToLower()) : w.Region.NameEng == w.Region.NameEng) ||
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

        #region
        public async Task<List<CountryCodeModel>> GetCountryCodes()
        {
            var countryCodes = _dbContext.CountryCode
                .Where(w => w.IsActive.Value)
                .Select(s => new CountryCodeModel
                {
                    CountryCodeId = s.CountryCodeId,
                    CodeName = s.CodeName,
                    CodeIcon= s.CodeIcon,
                    CountryId = s.CountryId
                }).ToList();
            return await Task.FromResult(countryCodes);
        }
        #endregion

        #region
        public async Task<GetResults> GetDistricts(int regionId,int pageNo,int pageSize,string searchValue)
        {
            
            var districts = _dbContext.Districts
                .Where(w => !w.IsDeleted && w.RegionId == regionId && w.DistrictName.Contains(!string.IsNullOrEmpty(searchValue)?searchValue:w.DistrictName))
                .Select(s => new DistrictModel
                {
                    RegionId = s.RegionId,
                    Id = s.DistrictId,
                    DistrictName = s.DistrictName,
                }).Skip(pageSize * pageNo).Take(pageSize).ToList();


            int total = _dbContext.Districts.Where(w => w.IsDeleted == false).Where(w =>
                                   !w.IsDeleted && w.RegionId == regionId && w.DistrictName.Contains(!string.IsNullOrEmpty(searchValue) ? searchValue : w.DistrictName)
                                    ).CountAsync().Result;
            GetResults result = new GetResults()
            {
                Data = districts,
                IsSuccess = true,
                Total = total,
                Message = "Districts found"
            };
            return await Task.FromResult(result);
        }

        public async Task<bool> DeleteDistrict(int Id)
        {
            bool ischanged = false;
            var uinfo = _dbContext.Districts.Where(w => w.DistrictId == Id).FirstOrDefaultAsync().Result;
            if (uinfo != null)
            {
                uinfo.IsDeleted = true;
        
            }
            await _dbContext.SaveChangesAsync();
            ischanged = true;
            return await Task.FromResult(ischanged);
        }
        public async Task<GetResults> GetDistrictById(int id)
        {

            var districts = _dbContext.Districts.Join(_dbContext.Region, d => d.RegionId, r => r.Id, (d, r) => new {d,r })
                .Join(_dbContext.Country, rg => rg.r.CountryId, c => c.Id, (rg, c) => new {rg,c })
                .Where(w => w.rg.d.DistrictId==id && w.rg.d.IsDeleted==false)
                .Select(s => new DistrictModel
                {
                    RegionId = s.rg.d.RegionId,
                    Id = s.rg.d.DistrictId,
                    DistrictName = s.rg.d.DistrictName,
                    CountryId=s.c.Id

                }).FirstOrDefault(); 

            GetResults result = new GetResults()
            {
                Data = districts,
                IsSuccess = true,
                Total = 1,
                Message = "Districts found"
            };
            return await Task.FromResult(result);
        }

        public async Task<List<DistrictModel>> GetMasterDistricts(int regionid)
        {
            var regions = _dbContext.Districts
                .Where(w => !w.IsDeleted && w.RegionId == regionid)
                .Select(s => new DistrictModel
                {
                    Id = s.DistrictId,
                    DistrictName = s.DistrictName
                }).ToList();
            return await Task.FromResult(regions);
        }
        #endregion
    }
}
