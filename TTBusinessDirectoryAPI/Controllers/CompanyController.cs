using ApplicationService.IServices;
using CommonService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Threading.Tasks;
using System;
using CommonService.RequestModel;

namespace TTBusinessDirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ICompanies _company;
        protected Logger logger;
        public CompanyController(ICompanies company)
        {
            _company = company;
            logger = LogManager.GetLogger("Company");
        }
        [HttpGet]
        [Route("GetAllCompanies")]
        public async Task<GetResults> GetAllCompanies(int page = 0, int limit = 10)
        {
            GetResults getResults = new GetResults();
            try
            {
                getResults = _company.GetAllCompanies(page,limit,"").Result;
                getResults.IsSuccess = true;
                getResults.Message = "Company List";
                logger.Info("Get Menus");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<GetResults> GetCompanyById(int companyid)
        {
            GetResults getResults = new GetResults();
            try
            {
                getResults = _company.GetCompanyById(companyid).Result;
                getResults.IsSuccess = true;
                getResults.Message = "Company Details found";
                logger.Info(getResults.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }


        [HttpPost]
        [Route("CreateUpdateCompany")]
        public async Task<GetResults> CreateUpdateCompany(CompanyRequestModel cRequest)
        {
            GetResults getResults = new GetResults();
            try
            {
                var result = _company.CreateUpdateCompany(cRequest).Result;
                logger.Info(result.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpPost]
        [Route("DeleteCompany")]
        public async Task<GetResults> DeleteCompany(ChangeStatusModel cModel)
        {
            GetResults getResults = new GetResults();
            try
            {
                var result = _company.DeleteCompany(cModel.Id).Result;
                logger.Info(result.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }


        [HttpPost]
        [Route("VerifyCompany")]
        public async Task<GetResults> VerifyCompany(ChangeStatusModel cModel)
        {
            GetResults getResults = new GetResults();
            try
            {
                var result = _company.VerifyCompany(cModel.Id).Result;
                logger.Info(result.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }
    }
}
