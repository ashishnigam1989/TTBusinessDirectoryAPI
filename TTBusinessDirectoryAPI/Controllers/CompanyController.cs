using ApplicationService.IServices;
using CommonService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Threading.Tasks;
using System;
using CommonService.RequestModel;
using DatabaseService.DbEntities;
using CommonService.Constants;
using CommonService.Enums;
using CommonService.Helpers;
using System.Collections;
using System.IO;
using System.Collections.Generic;

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
                getResults = await _company.GetFeaturedCompanies();
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

        [HttpPost]
        [Route("UploadImage")]
        public async Task<GetResults> UploadImage()
        {
            var imageList = new List<string>();
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    var imgType = (EnumImageType)Enum.Parse(typeof(EnumImageType), Convert.ToString(Request.Form["imgtype"]));
                    string filetempPath = Helper.GetFileUploadDetails(imgType);
                    string tempPath = CommonConstants.FileTempPath + "/" + filetempPath;
                    if (!Directory.Exists(tempPath))
                    {
                        Directory.CreateDirectory(tempPath);
                    }
                    var postedFile = Request.Form.Files[0];
                    if (postedFile != null)
                    {
                        string newName = string.Empty;
                        newName = DateTime.Now.ToString("ddMMyyyyhhmmss") + "_" + postedFile.FileName;
                        string newPath = Path.Combine(tempPath, newName);
                        using (var stream = new FileStream(newPath, FileMode.Create))
                        {
                            await postedFile.CopyToAsync(stream);
                            imageList.Add(filetempPath + newName);
                            stream.Flush();
                        }
                    }
                }
                return new GetResults { IsSuccess = true, Data = imageList, Message = "Success" };
            }
            catch (Exception ex)
            {
                logger.Info("UploadController--> Upload: An error occured ");
                logger.Error(ex);
                return new GetResults { IsSuccess = false, Message = "Failed" };
            }
        }
    }
}
