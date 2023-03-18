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
    public class Master : IMaster
    {
        private BusinessDirectoryDBContext _dbContext;
        private readonly IMapper _mapper;

        public Master(BusinessDirectoryDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public async Task<GetResults> GetRoles(int page, int limit, string searchValue)
        {
            int total = 0;
            List<RoleModel> rolelist = _dbContext.Roles.Where(w =>
           (!string.IsNullOrEmpty(searchValue) ? w.Name.ToLower().Contains(searchValue.ToLower()) : w.Name == w.Name) && w.IsDeleted == false
            ).Select(s => new RoleModel
            {
                Name = s.Name,
                DisplayName = s.DisplayName,
                Id = s.Id
            }).Distinct().OrderByDescending(o => o.Id).Skip(limit * page).Take(limit).ToListAsync().Result;

            total = _dbContext.Roles.Where(w => w.IsDeleted == false).Where(w =>
                                    !string.IsNullOrEmpty(searchValue) ? w.Name.ToLower().Contains(searchValue.ToLower()) : w.Name == w.Name
                                    ).CountAsync().Result;

            GetResults uobj = new GetResults
            {
                Total = total,
                Data = rolelist
            };
            return await Task.FromResult(uobj);

        }

        public async Task<List<RoleModel>> GetMasterRoles()
        {
            var roles = _dbContext.Roles
                .Where(w=>w.IsDeleted == false)
                .Select(s => new RoleModel
                {
                    Name = s.Name,
                    Id = s.Id,
                    DisplayName = s.DisplayName
                }).ToList();
            return await Task.FromResult(roles);
        }

        public async Task<List<MenuModel>> GetAllMenus()
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

            return await Task.FromResult(menus);

        }

        #region Brands
        public async Task<GetResults> GetBrands(int page, int limit, string searchValue)
        {
            var brands = _dbContext.Brand.ToListAsync().Result;
            var list = _mapper.Map<List<BrandModel>>(brands);
            int total = list.Count();
            if (!string.IsNullOrEmpty(searchValue.ToLower()))
            {
                list = (List<BrandModel>)list.Where(m => m.NameEng.Contains(searchValue));
                total = list.Count();
            }
            var finalData = list.OrderByDescending(o => o.Id).Skip(limit * page).Take(limit);
            GetResults uobj = new GetResults
            {
                Total = total,
                Data = finalData
            };
            return await Task.FromResult(uobj);
        }
        public async Task<BrandModel> GetBrandById(int id)
        {
            var brand = _dbContext.Brand.SingleAsync(b=>b.Id == id).Result;
            var finalData = _mapper.Map<BrandModel>(brand);
            return await Task.FromResult(finalData);
        }
        public async Task<bool> AddBrand(BrandRequestModel brandRequest)
        {
            bool result = false;
            var brandInfo = _dbContext.Brand.Where(w => w.NameEng.ToLower() == brandRequest.NameEng.ToLower()).FirstOrDefault();
            if(brandInfo != null)
            {
                Brand brand = _mapper.Map<Brand>(brandInfo);
                _dbContext.Brand.Add(brand);
                var brandId = await _dbContext.SaveChangesAsync();
                result = true;
            }
            return await Task.FromResult(result);
        }
        public async Task<bool> EditBrand(BrandRequestModel brandRequest)
        {
            bool result = false;
            var brandInfo = _dbContext.Brand.Where(w => w.NameEng.ToLower() == brandRequest.NameEng.ToLower() && w.Id != brandRequest.Id).FirstOrDefault();
            if (brandInfo != null)
            {
                var brand = _mapper.Map<Brand>(brandRequest);
                _dbContext.Brand.Update(brand);
                await _dbContext.SaveChangesAsync();
                result = true;
            }
            return await Task.FromResult(result);
        }
        #endregion
    }
}
