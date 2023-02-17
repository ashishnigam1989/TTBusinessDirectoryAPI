using AutoMapper;
using CommonService.RequestModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using System;

namespace ApplicationService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Users, UserModel>().ReverseMap();
            CreateMap<Roles, RoleModel>().ReverseMap();
            CreateMap<Menus, MenuModel>().ReverseMap();
            CreateMap<MenuRolePermission, MenuModel>().ReverseMap();
            CreateMap<UserRequestModel, Users>().ReverseMap();
            CreateMap<CountryModel, Country>().ReverseMap();
        }
    }
}
