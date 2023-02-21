﻿using ApplicationService.IServices;
using AutoMapper;
using CommonService.Enums;
using CommonService.RequestModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApplicationService.Services
{
    public class Account : IAccount
    {
        private BusinessDirectoryDBContext _dbContext;
        private readonly IMapper _mapper;
        public Account(BusinessDirectoryDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }
        public async Task<UserModel> Login(LoginRequestModel login)
        {
            var user = _dbContext.Users.Join(_dbContext.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
                .Where(x => x.u.EmailAddress == login.EmailAddress && x.u.Password == login.Password)
                .Select(s => new UserModel
                {
                    Id = s.u.Id,
                    Name = s.u.Name,
                    RoleId = s.ur.RoleId,
                    Surname = s.u.Surname,
                    EmailAddress = s.u.EmailAddress,
                    IsEmailConfirmed = s.u.IsEmailConfirmed,
                    LastLoginTime = s.u.LastLoginTime,
                    IsDeleted = s.u.IsDeleted,
                    IsActive = s.u.IsActive,
                    Mobile = s.u.Mobile
                }).FirstOrDefaultAsync().Result;
            return await Task.FromResult(user);
        }

        public async Task<List<MenuModel>> GetMenus(int roleId)
        {
            var menus = _dbContext.MenuRolePermission.Join(_dbContext.Menus, mr => mr.MenuId, m => m.MenuId, (mr, m) => new { mr, m })
                .Where(r => r.mr.RoleId == roleId)
                .Select(s => new MenuModel
                {
                    MenuId = s.mr.MenuId,
                    ParentId = s.m.ParentId,
                    MenuName = s.m.MenuName,
                    ComponentName = s.m.ComponentName,
                    MenuPath = s.m.MenuPath,
                    MenuIcon = s.m.MenuIcon,
                    Sequence = s.m.Sequence,
                    IsActivated = s.m.IsActivated,
                    IsActive = true

                }).ToListAsync().Result;

            return await Task.FromResult(menus);
        }

        public async Task<GetResults> GetUsers(int page, int limit, string searchValue)
        {
            //var usersobj = _dbContext.Users
            //    .Join(_dbContext.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur });

            var usersobj = _dbContext.Users
                .Join(_dbContext.UserRoles, u => u.Id, ur => ur.UserId, (u, ur)
                => new { Users = u, UserRoles = ur })
                .Join(_dbContext.Roles, x => x.UserRoles.RoleId, r => r.Id, (x, r)
                => new { x.Users, x.UserRoles, Roles = r });

            int total = usersobj.CountAsync().Result;
            var users = usersobj
                .Select(s => new UserModel
                {
                    Id = s.Users.Id,
                    Name = s.Users.Name,
                    RoleId = s.UserRoles.RoleId,
                    Rolename = s.Roles.Name,
                    Surname = s.Users.Surname,
                    EmailAddress = s.Users.EmailAddress,
                    Username = s.Users.UserName,
                    IsEmailConfirmed = s.Users.IsEmailConfirmed,
                    LastLoginTime = s.Users.LastLoginTime,
                    IsDeleted = s.Users.IsDeleted,
                    IsActive = s.Users.IsActive,
                    Mobile = s.Users.Mobile
                });

            if (!string.IsNullOrEmpty(searchValue.ToLower()))
            {
                users = users.Where(m => m.Name.ToLower().Contains(searchValue)
                                || m.EmailAddress.ToLower().Contains(searchValue)
                                || m.Mobile.ToLower().Contains(searchValue)
                                || m.Rolename.ToLower().Contains(searchValue));

                total = users.Distinct().CountAsync().Result;
            }
            
            var finalData = users.Distinct().OrderByDescending(o => o.Id).Skip(limit * page).Take(limit).ToListAsync().Result;

            GetResults uobj = new GetResults
            {
                Total = total,
                Data = finalData
            };
            return await Task.FromResult(uobj);
        }
        public async Task<bool> CreateUser(UserRequestModel userRequest)
        {
            bool result = false;
            var userinfo = _dbContext.Users.Where(w => w.EmailAddress.ToLower() == userRequest.EmailAddress.ToLower()).FirstOrDefault();
            if (userinfo == null)
            {
                Users uobj = new Users
                {
                    Name = userRequest.Name,
                    EmailAddress = userRequest.EmailAddress,
                    Password = userRequest.Password,
                    Surname = userRequest.Surname,
                    UserName = userRequest.UserName,
                    Mobile = userRequest.Mobile,
                    IsEmailConfirmed = false,
                    EmailConfirmationCode = String.Empty,
                    PasswordResetCode = String.Empty,
                    LastLoginTime = DateTime.Now,
                    IsDeleted = false,
                    CreationTime = DateTime.Now,
                    ShouldChangePasswordOnNextLogin = true,
                    TenantId = 1

                };
                _dbContext.Users.Add(uobj);
                var userid = await _dbContext.SaveChangesAsync();
                var userRole = new UserRoles();
                userRole.UserId = uobj.Id;
                userRole.RoleId = userRequest.RoleId;
                userRole.CreatorUserId = uobj.Id;
                userRole.CreationTime = DateTime.Now;
                _dbContext.UserRoles.Add(userRole);
                await _dbContext.SaveChangesAsync();
                result = true;
            }
            else
            {
                userinfo.Name = userRequest.Name;
                userinfo.EmailAddress = userRequest.EmailAddress;
             //   userinfo.Password = userRequest.Password;
                userinfo.Surname = userRequest.Surname;
                userinfo.UserName = userRequest.UserName;
                userinfo.Mobile = userRequest.Mobile; 
                userinfo.EmailConfirmationCode = String.Empty;
                userinfo.PasswordResetCode = String.Empty;
                userinfo.LastLoginTime = DateTime.Now; 
                userinfo.LastModificationTime = DateTime.Now;
                userinfo.ShouldChangePasswordOnNextLogin = true;
                userinfo.TenantId = 1;

                var roleinfo = _dbContext.UserRoles.Where(w => w.RoleId == userRequest.RoleId && w.UserId == userinfo.Id).FirstOrDefault();
                if (roleinfo != null)
                {
                    var userRole = new UserRoles();
                    userRole.UserId = userinfo.Id;
                    userRole.RoleId = userRequest.RoleId;
                    userRole.CreatorUserId = userinfo.Id;
                    userRole.CreationTime = DateTime.Now;
                    _dbContext.UserRoles.Add(userRole);
                }
                else
                {
                    roleinfo.UserId = userinfo.Id;
                    roleinfo.RoleId = userRequest.RoleId;
                    roleinfo.CreatorUserId = userinfo.Id;
                    roleinfo.CreationTime = DateTime.Now;
                }
                await _dbContext.SaveChangesAsync();
                result = true;
            }
            
            return await Task.FromResult(result);
        }

        public async Task<bool> EditUser(UserRequestModel userRequest)
        {
            //transaction need to implement
            bool result = false;
            if (userRequest != null)
            {
                var user = _mapper.Map<Users>(userRequest);
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
                result = true;
            }
            return await Task.FromResult(result);
        }

        public async Task<bool> RoleMenuPermission(RoleMenuMapping _rModel)
        {
            var rolemenus = _dbContext.MenuRolePermission.Where(w => w.RoleId == _rModel.RoleId).ToList();
            if (rolemenus.Count > 0)
            {
                _dbContext.MenuRolePermission.RemoveRange(rolemenus);
                await _dbContext.SaveChangesAsync();
            }
            List<MenuRolePermission> rmlist = new List<MenuRolePermission>();
            foreach (var menuid in _rModel.Menus)
            {
                MenuRolePermission rmModel = new MenuRolePermission
                {
                    RoleId = _rModel.RoleId,
                    MenuId = menuid
                };
                rmlist.Add(rmModel);
            }
            _dbContext.MenuRolePermission.AddRange(rmlist);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(true);

        }

        public async Task<UserModel> GetUserById(int userid)
        {
            var udetail = _dbContext.Users.Join(_dbContext.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
                .Where(w => w.u.Id == userid).Select(s => new UserModel
                {
                    Id = s.u.Id,
                    Name = s.u.Name,
                    RoleId = s.ur.RoleId,
                    Surname = s.u.Surname,
                    EmailAddress = s.u.EmailAddress,
                    Username = s.u.UserName,
                    IsEmailConfirmed = s.u.IsEmailConfirmed,
                    LastLoginTime = s.u.LastLoginTime,
                    IsDeleted = s.u.IsDeleted,
                    IsActive = s.u.IsActive,
                    Mobile = s.u.Mobile
                }).FirstOrDefault();
            return await Task.FromResult(udetail);
        }

        public async Task<bool> ApproveRejectUser(UserApproveModel uModel)
        {
            bool ischanged=false;
            var uinfo = _dbContext.Users.Where(w => w.Id == uModel.UserId).FirstOrDefaultAsync().Result;
            if(uinfo!=null)
            {
                if (uModel.Status == EnumStatus.Approve)
                {
                    uinfo.IsActive = true;
                }
                if (uModel.Status == EnumStatus.Reject)
                {
                    uinfo.IsActive = false;
                }
            }
            await _dbContext.SaveChangesAsync();
            ischanged=true;
            return await Task.FromResult(ischanged);
        }
    }
}
