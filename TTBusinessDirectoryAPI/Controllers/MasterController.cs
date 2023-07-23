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
                logger.Info("Going to get roles.");
                var roles = _master.GetMasterRoles().Result;
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
                logger.Info("Going to get countries.");
                var countries = await _location.GetMasterCountries();
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
        [Route("GetRegions/{countryId:int}")]
        public async Task<GetResults> GetRegions(int countryId)
        {
            GetResults getResults = new GetResults();
            try
            {
                logger.Info("Going to get regionns.");
                var regions = await _location.GetMasterRegions(countryId);
                var total = regions.Count;
                getResults = new GetResults(true, "Get Regions", regions, total);
                logger.Info("Get Regions");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetCountryCodes")]
        public async Task<GetResults> GetCountryCodes()
        {
            GetResults getResults = new GetResults();
            try
            {
                logger.Info("Going to get country codes.");
                var regions = await _location.GetCountryCodes();
                var total = regions.Count;
                getResults = new GetResults(true, "Get Country Codes", regions, total);
                logger.Info("Get Country Codes");
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetDistricts/{regionId:int}")]
        public async Task<GetResults> GetDistricts(int regionId)
        {
            GetResults getResults = new GetResults();
            try
            {
                logger.Info("Going to get districts.");
                var districtModels = await _location.GetMasterDistricts(regionId);
                getResults = new GetResults(true, "Get Districts", districtModels, districtModels.Count);
                logger.Info("Get Districts");
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
                var menus = _master.GetAllMenus().Result;
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
