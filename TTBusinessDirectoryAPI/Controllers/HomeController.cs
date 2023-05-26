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

namespace TTBusinessDirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        protected Logger logger;
        private IMaster _master;
        private IListing _listing;
        private IAccount _account;
        private readonly IInsight _insights;
        public HomeController(IMaster master, IListing listing, IAccount account, IInsight insights)
        {
            logger = LogManager.GetLogger("Home");
            _master = master;
            _listing = listing;
            _account = account;
            _insights = insights;
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<GetResults> GetAllCategories(bool isFeatured = true)
        {
            GetResults getResults = new GetResults();
            try
            {
                logger.Info("Going to get featured categories.");
                getResults = await _master.GetFeaturedCategories(isFeatured);
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
        [Route("GetSearchResults/{searchTerm}")]

        public async Task<GetResults> GetSearchResults(string searchTerm)
        {
            GetResults getResults = new GetResults();
            try
            {
                logger.Info("Going to get search results.");
                getResults = await _master.GetSearchResults(searchTerm);
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
    }
}
