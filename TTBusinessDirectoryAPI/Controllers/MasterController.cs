using ApplicationService.IServices;
using CommonService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Threading.Tasks;

namespace TTBusinessDirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private IMaster _master;
        private ILocation _location;
        protected Logger logger;

        public MasterController(IMaster master, ILocation location)
        {
            _master = master;
            logger = LogManager.GetLogger("Test");
            _location = location;
        }

        [HttpGet]
        [Route("GetRoles")]
        public async Task<GetResults> GetRoles()
        {
            GetResults getResults = new GetResults();
            try
            {
                var roles = _master.GetRoles();
                var total = roles.Count;
                getResults = new GetResults(true, "Get Roles", roles, total);
                logger.Info("Get Roles");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetCountries")]
        public async Task<GetResults> GetCountries()
        {
            GetResults getResults = new GetResults();
            try
            {
                var countries = _location.GetCountries().Result;
                var total = countries.Count;
                getResults = new GetResults(true, "Get Countries", countries, total);
                logger.Info("Get Countries");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetAllMenus")]
        public async Task<GetResults> GetAllMenus()
        {
            GetResults getResults = new GetResults();
            try
            {
                var menus = _master.GetAllMenus();
                var total = menus.Count;
                getResults = new GetResults(true, "Get Menus", menus, total);
                logger.Info("Get Menus");
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
