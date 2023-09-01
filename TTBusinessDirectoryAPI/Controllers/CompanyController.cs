using ApplicationService.IServices;
using CommonService.ViewModels;
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
        private IMaster _master;
        public CompanyController(ICompanies company, IMaster master)
        {
            _company = company;
            logger = LogManager.GetLogger("Company");
            _master = master;
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
                logger.Info("Get Companies");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetCompanyById")]
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

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<GetResults> GetAllCategories(int page = 0, int limit = 10)
        {
            GetResults getResults = new GetResults();
            try
            {
                getResults = _master.GetAllCategories(page, limit, "").Result;
                getResults.IsSuccess = true;
                getResults.Message = "Category List";
                logger.Info(" getResults.Message");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpPost]
        [Route("CreateUpdateCategory")]
        public async Task<GetResults> CreateUpdateCategory(CategoriesRequestModel cRequest)
        {
            GetResults getResults = new GetResults();
            try
            {
                var result = _master.CreateUpdateCategory(cRequest).Result;
                logger.Info(result.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetCategoryById")]
        public async Task<GetResults> GetCategoryById(int categoryid)
        {
            GetResults getResults = new GetResults();
            try
            {
                getResults = _master.GetCategoryById(categoryid).Result;
                getResults.IsSuccess = true;
                getResults.Message = "Category Details found";
                logger.Info(getResults.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("DeleteCategory")]
        public async Task<GetResults> DeleteCategory(int categoryid)
        {
            GetResults getResults = new GetResults();
            try
            {
                getResults = _master.DeleteCategory(categoryid).Result;
                logger.Info(getResults.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        //////

        [HttpGet]
        [Route("GetAllBrands")]
        public async Task<GetResults> GetAllBrands(int page = 0, int limit = 10)
        {
            GetResults getResults = new GetResults();
            try
            {
                getResults = _master.GetAllBrands(page, limit, "").Result;
                getResults.IsSuccess = true;
                getResults.Message = "Brand List";
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
        [Route("AddUpdateBrand")]
        public async Task<GetResults> AddUpdateBrand(BrandRequestModel bRequest)
        {
            GetResults getResults = new GetResults();
            try
            {
                var result = _master.AddUpdateBrand(bRequest).Result;
                logger.Info(result.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetBrandById")]
        public async Task<GetResults> GetBrandById(int brandid)
        {
            GetResults getResults = new GetResults();
            try
            {
                getResults = _master.GetBrandById(brandid).Result;
                getResults.IsSuccess = true;
                getResults.Message = "Brand Details found";
                logger.Info(getResults.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("DeleteBrand")]
        public async Task<GetResults> DeleteBrand(int brandid)
        {
            GetResults getResults = new GetResults();
            try
            {
                getResults = _master.DeleteBrand(brandid).Result;
                logger.Info(getResults.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetFeaturedCompanies")]
        public async Task<GetResults> GetFeaturedCompanies(int limit = 0)
        {
            GetResults getResults = new GetResults();
            try
            {
                getResults = await _company.GetFeaturedCompanies(limit);
                logger.Info(getResults.Message);
                getResults.IsSuccess = true;
                getResults.Message = "Featured Company List";
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetCompanyDetailsById/{companyid}")]
        public async Task<GetResults> GetCompanyDetailsById(long companyid)
        {
            GetResults getResults = new GetResults();
            try
            {
                getResults = await _company.GetCompanyDetailsById(companyid);
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

        [HttpGet]
        [Route("GetCompanyProducts/{companyId:long}/{limit:int}/{skip:int}")]
        public async Task<GetResults> GetCompanayProducts(long companyId, int limit , int skip)
        {
            GetResults getResults = new GetResults();

            try
            {
                var data = await _company.GetProductsByCompanyId(companyId, skip, limit);
                getResults.Data= data;
                getResults.IsSuccess = true;
                getResults.Message = "Fetched company products.";
                logger.Info(getResults.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }


        [HttpGet]
        [Route("GetCompanyServices/{companyId:long}/{limit:int}/{skip:int}")]
        public async Task<GetResults> GetCompanyServices(long companyId, int limit, int skip)
        {
            GetResults getResults = new GetResults();

            try
            {
                var data = await _company.GetServicesByCompanyId(companyId, skip, limit);
                getResults.Data = data;
                getResults.IsSuccess = true;
                getResults.Message = "Fetched company services.";
                logger.Info(getResults.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetCompanyNewsArticles/{companyId:long}/{limit:int}/{skip:int}")]
        public async Task<GetResults> GetCompanyNewsArticles(long companyId, int limit, int skip)
        {
            GetResults getResults = new GetResults();

            try
            {
                var data = await _company.GetCompanyNewsArticles(companyId, skip, limit);
                getResults.Data = data;
                getResults.IsSuccess = true;
                getResults.Message = "Fetched company services.";
                logger.Info(getResults.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetCompanyEvents/{companyId:long}/{limit:int}/{skip:int}")]
        public async Task<GetResults> GetCompanyEvents(long companyId, int limit, int skip)
        {
            GetResults getResults = new GetResults();

            try
            {
                var data = await _company.GetCompanyEvents(companyId, skip, limit);
                getResults.Data = data;
                getResults.IsSuccess = true;
                getResults.Message = "Fetched company services.";
                logger.Info(getResults.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [Route("GetCompanyProductById/{productId:int}")]
        public async Task<GetResults> GetCompanyProductById(int productId)
        {
            GetResults getResults = new GetResults();

            try
            {
                getResults = await _company.GetCompanyProductById(productId);
                logger.Info(getResults.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }


        [Route("GetCompanyServiceById/{serviceId:int}")]
        public async Task<GetResults> GetCompanyServiceById(int serviceId)
        {
            GetResults getResults = new GetResults();

            try
            {
                getResults = await _company.GetCompanyServiceById(serviceId);
                logger.Info(getResults.Message);
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
