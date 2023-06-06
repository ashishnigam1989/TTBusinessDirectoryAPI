using System;
using System.Collections.Generic;
using System.Text;
using CommonService.RequestModel;
using CommonService.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApplicationService.IServices
{
    public interface IMaster
    {
        Task<GetResults> GetRoles(int page, int limit, string searchValue);
        Task<List<RoleModel>> GetMasterRoles();
        Task<List<MenuModel>> GetAllMenus();

        #region Category
        public Task<GetResults> GetAllCategories(int page, int limit, string searchValue);
        public Task<GetResults> CreateUpdateCategory(CategoriesRequestModel crModel);
        public Task<GetResults> GetCategoryById(int id);
        public Task<GetResults> DeleteCategory(int id);
        public Task<GetResults> GetMasterCategories();
        Task<GetResults> GetFeaturedCategories(bool isFeatured = true, int count = 0);

        #endregion

        #region Brands
        public Task<GetResults> GetAllBrands(int page, int limit, string searchValue);
        public Task<GetResults> GetBrandById(int brandid);
        public Task<GetResults> AddUpdateBrand(BrandRequestModel breqmodel);
        public Task<GetResults> DeleteBrand(int brandid);
        Task<GetResults> GetMasterBrand();
        #endregion

        Task<GetResults> GetSearchResults(string searchTerm, int countryId);
    }
}
