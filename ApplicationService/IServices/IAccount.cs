using CommonService.RequestModel;
using CommonService.ViewModels;
using System.Collections.Generic;
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
        Task<bool> EnableDisableUser(EnableDisableModel uModel);
        Task<long> CreateUserForListing(UserRequestModel userRequest);
        Task<bool> CreateUserRole(UserRoleModel urModel);
        Task<bool> DeleteUser(int userid, int deletedby);
    }
}
