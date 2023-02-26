using CommonService.Enums;
using CommonService.RequestModel;
using CommonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.IServices
{
    public interface IAccount
    {
        Task<UserModel> Login(LoginRequestModel login);
        Task<List<MenuModel>> GetMenus(int roleId);
        Task<GetResults> GetUsers(int page, int limit, string searchValue);
        Task<bool> CreateUser(UserRequestModel userRequest);
        Task<bool> EditUser(UserRequestModel userRequest);
        Task<bool> RoleMenuPermission(RoleMenuMapping _rModel);
        Task<UserModel> GetUserById(int userid);
        Task<bool> ApproveRejectUser(ChangeStatusModel uModel);
    }
}
