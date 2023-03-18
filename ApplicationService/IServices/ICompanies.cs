using CommonService.RequestModel;
using CommonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.IServices
{
    public interface ICompanies
    {
        Task<GetResults> GetAllCompanies(int page, int limit, string searchValue);
        Task<GetResults> GetCompanyById(int id);
        Task<GetResults> CreateUpdateCompany(CompanyRequestModel creqmodel);
        Task<GetResults> DeleteCompany(int id);
        Task<GetResults> VerifyCompany(int id);
        public Task<GetResults> GetAllCategories(int page, int limit, string searchValue);
        public Task<GetResults> CreateUpdateCategory(CategoriesRequestModel crModel);
        public Task<GetResults> GetCategoryById(int id);
        public Task<GetResults> DeleteCategory(int id);
        public Task<GetResults> GetAllBrands(int page, int limit, string searchValue);
        public Task<GetResults> GetBrandById(int brandid);
        public Task<GetResults> AddUpdateBrand(BrandRequestModel breqmodel);
        public Task<GetResults> DeleteBrand(int brandid);
    }
}
