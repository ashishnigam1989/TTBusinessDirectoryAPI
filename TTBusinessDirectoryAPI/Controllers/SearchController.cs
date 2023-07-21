using CommonService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using ApplicationService.IServices;
using NLog;

namespace TTBusinessDirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        protected Logger logger;
        private IMaster _master;
        public SearchController(IMaster master)
        {
            logger = LogManager.GetLogger("Search");
            _master = master;
        }

        [HttpGet]
        [Route("GetSearchPageResult/{searchStr}/{countryId}")]
        public async Task<GetResults> GetSearchPageResult(string searchStr, int countryId)
        {
            GetResults getResults = new GetResults();
            try
            {
                logger.Info("Going to get search results.");
                getResults = await _master.GetSearchPageResult(searchStr, countryId);
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
    }
}
