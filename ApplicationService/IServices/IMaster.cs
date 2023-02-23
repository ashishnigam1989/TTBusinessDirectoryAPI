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
        Task<List<RoleModel>> GetRoles();
        Task<List<MenuModel>> GetAllMenus();

        #region Brands
        Task<GetResults> GetBrands(int page, int limit, string searchValue);
        Task<BrandModel> GetBrandById(int id);
        Task<bool> AddBrand(BrandRequestModel brandRequest);
        Task<bool> EditBrand(BrandRequestModel brandRequest);
        #endregion
    }
}
