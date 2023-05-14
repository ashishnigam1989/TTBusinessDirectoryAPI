using CommonService.ViewModels;
using DatabaseService.DbEntities;
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
    public class HomeController : ControllerBase
    {
        private ICategories categories;
        protected Logger logger;
        private IMaster _master;
        public HomeController(ICategories category, IMaster master)
        {
            categories = category;
            logger = LogManager.GetLogger("Home");
            _master = master;
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<GetResults> GetAllCategories(bool isFeatured = true)
        {
            GetResults getResults = new GetResults();
            try
            {
                getResults = await categories.GetAllCategories(isFeatured);
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
        [Route("GetSearchResults/{searchTerm}")]
        public async Task<GetResults> GetSearchResults(string searchTerm)
        {
            GetResults getResults = new GetResults();
            try
            {
                getResults = await _master.GetSearchResults(searchTerm);
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

    }
}
