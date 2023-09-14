using CommonService.ViewModels;
using DatabaseService.DbEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using ApplicationService.IServices;
using NLog;
using CommonService.RequestModel;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using TTBusinessDirectoryAPI.Extensions;
using ApplicationService.Services;
using CommonService.Enums;
using CommonService.Constants;
using CommonService.Helpers;
using System.Collections.Generic;
using System.IO;

namespace TTBusinessDirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class HomeController : ControllerBase
    {
        protected Logger logger;
        private IMaster _master;
        private IListing _listing;
        private IAccount _account;
        private ICompanies _company;
        private readonly IInsight _insights;
        public HomeController(IMaster master, IListing listing, IAccount account, IInsight insights, ICompanies company)
        {
            logger = LogManager.GetLogger("Home");
            _master = master;
            _listing = listing;
            _account = account;
            _insights = insights;
            _company = company;
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<GetResults> GetAllCategories(bool isFeatured = true, int count = 0)
        {
            GetResults getResults = new GetResults();
            try
            {
                logger.Info("Going to get featured categories.");
                getResults = await _master.GetFeaturedCategories(isFeatured, count);
                getResults.IsSuccess = true;
                getResults.Message = "Featured Categories";
                logger.Info("Get Featured Categories.");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetSearchResults/{searchTerm}/{countryId}")]

        public async Task<GetResults> GetSearchResults(string searchTerm, int countryId)
        {
            GetResults getResults = new GetResults();
            try
            {
                logger.Info("Going to get search results.");
                getResults = await _master.GetSearchResults(searchTerm, countryId);
                getResults.IsSuccess = true;
                getResults.Message = "Search Result";
                logger.Info("Get Search Results");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpPost]
        [Route("AddNewBusiness")]
        public async Task<GetResults> AddNewBusiness(NewBusinessModel newBusinessDetails)
        {
            GetResults getResults = new GetResults();
            try
            {
                logger.Info("Going to add free listing and products.");

                if(newBusinessDetails.NewUserDetails != null)
                {
                    newBusinessDetails.NewUserDetails.Password = PasswordHelper.HashPassword(newBusinessDetails.NewUserDetails.Password);
                }

                getResults = await _listing.AddFreeListing(newBusinessDetails);

                logger.Info("Business added successfully.");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetAllInsights/{insightType}")]
        public async Task<GetResults> GetAllInsights(string insightType)
        {
            GetResults getResults = new GetResults();
            try
            {
                logger.Info($"Going to get insights for insight type : {insightType}.");
                if (!Enum.TryParse<EnumInsightType>(insightType, true, out var insight))
                {
                    getResults.IsSuccess = false;
                    getResults.Message = "Incorrect Insight Types provided.";
                    return await Task.FromResult(getResults);
                }
                getResults.Data = await _insights.GetInsights(insight);
                getResults.IsSuccess = true;
                getResults.Message = "Insights";
                logger.Info("Get Insights.");
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
                    string filetempPath = "/" + Helper.GetFileUploadDetails(imgType);
                    string tempPath = CommonConstants.FileTempPath + "/" + filetempPath;
                    if (!Directory.Exists(tempPath))
                    {
                        Directory.CreateDirectory(tempPath);
                    }
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {

                        var postedFile = Request.Form.Files[i];
                        if (postedFile != null)
                        {
                            string newName = string.Empty;
                            FileInfo fi = new FileInfo(postedFile.FileName);
                            //newName = DateTime.Now.ToString("ddMMyyyyhhmmss")+"_"+ postedFile.FileName;
                            newName = DateTime.Now.ToString("ddMMyyyyhhmmss") + fi.Extension;
                            string newPath = Path.Combine(tempPath, newName);
                            using (var stream = new FileStream(newPath, FileMode.Create))
                            {
                                postedFile.CopyTo(stream);
                                imageList.Add(filetempPath + newName);
                                stream.Flush();
                            }
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

        [HttpGet]
        [Route("GetAllKeywords")]
        public async Task<GetResults> GetAllKeywords()
        {
            GetResults getResults = new GetResults();
            try
            {
                logger.Info($"Going to get keywords.");
                getResults = await _company.GetAllKeywords();
                return getResults;
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
