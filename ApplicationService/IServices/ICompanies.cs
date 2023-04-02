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
        Task<GetResults> GetMasterCompanies();


        Task<GetResults> AddEditCompanyBrand(CompanyBrandRequestModel cbModel);
        Task<GetResults> GetAllCompanyBrand(int page, int limit, string searchValue);
        Task<GetResults> GetCompanyBrandById(int id);
        Task<GetResults> DeleteCompanyBrand(int Id);


        Task<GetResults> GetAllCompanyCategory(int pageNo, int pageSize, string searchValue);
        Task<GetResults> GetCompanyCategoryById(int id);
        Task<GetResults> DeleteCompanyCategory(int Id);
        Task<GetResults> AddEditCompanyCategory(CompanyCategoryRequestModel ccModel);
    }
}
