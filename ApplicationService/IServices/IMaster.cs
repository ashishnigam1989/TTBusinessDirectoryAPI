using System;
using System.Collections.Generic;
using System.Text;
using CommonService.RequestModel;
using CommonService.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationService.IServices
{
    public interface IMaster
    {
        List<RoleModel> GetRoles();
        Task<List<CountryModel>> GetCountries();
        List<MenuModel> GetAllMenus();
    }
}
