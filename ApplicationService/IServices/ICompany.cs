using CommonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.IServices
{
    public interface ICompany
    {
        Task<GetResults> GetAllCompanies(int page, int limit, string searchValue);
        Task<GetResults> GetCompanyById(int id);
    }
}
