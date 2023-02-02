using ApplicationService.IServices;
using AutoMapper;
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
    public class Master : IMaster
    {
        private BusinessDirectoryDBContext _dbContext;
        private readonly IMapper _mapper;

        public Master(BusinessDirectoryDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public List<RoleModel> GetRoles()
        {
            var roles = _dbContext.Roles
                .Select(s => new RoleModel
                {
                    Name = s.Name,
                    Id = s.Id,
                    DisplayName = s.DisplayName
                }).ToList();


            return roles;
        }

        public async Task<List<CountryModel>> GetCountries()
        {
            var countries = _dbContext.Country
                .Select(s => new CountryModel
                {
                    CountryCode = s.CountryCode,
                    Id = s.Id,
                    CountryNameEng = s.CountryNameEng,
                    CurrencyCode = s.CurrencyCode
                }).ToListAsync().Result;

            return await Task.FromResult(countries);
        }

        public List<MenuModel> GetAllMenus()
        {
            var menus = _dbContext.Menus.Select(s=>new MenuModel { 
                MenuId= s.MenuId,
                MenuIcon= s.MenuIcon,
                MenuName=s.MenuName,
                MenuPath=s.MenuPath,
                ComponentName=s.ComponentName,
                Sequence=s.Sequence,
                IsActivated=s.IsActivated
            }).ToList();

            return menus;

        }
    }
}
