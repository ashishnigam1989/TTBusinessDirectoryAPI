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

        public async Task<List<RoleModel>> GetRoles()
        {
            var roles = _dbContext.Roles
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
    }
}
