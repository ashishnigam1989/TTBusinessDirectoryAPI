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

namespace TTBusinessDirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private ICategories categories;
        protected Logger logger;
        private IMaster _master;
        private IListing _listing;
        private IAccount _account;
        public HomeController(ICategories category, IMaster master, IListing listing, IAccount account)
        {
            categories = category;
            logger = LogManager.GetLogger("Home");
            _master = master;
            _listing = listing;
            _account = account;
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<GetResults> GetAllCategories(bool isFeatured = true)
        {
            GetResults getResults = new GetResults();
            try
            {
                logger.Info("Going to get featured categories.");
                getResults = await categories.GetAllCategories(isFeatured);
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
                var roles = await _master.GetMasterRoles();
                newBusinessDetails.NewUserDetails.RoleId = roles.FirstOrDefault(r => r.Name.ToLower().Equals("free")).Id;
                logger.Info("Going to add user for free listing.");
                var userId = await _account.CreateUserForListing(newBusinessDetails.NewUserDetails);

                newBusinessDetails.FreeListingDetails.CreatorUserId = userId;
                logger.Info("Going to add free listing and products" +
                    ".");
                await _listing.AddFreeListing(newBusinessDetails.FreeListingDetails);

                getResults.IsSuccess = true;
                getResults.Message = "Add Business";
                logger.Info("Add Business");
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
