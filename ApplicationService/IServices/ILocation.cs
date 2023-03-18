using CommonService.RequestModel;
using CommonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.IServices
{
    public interface ILocation
    {
        #region Country
        Task<List<CountryModel>> GetMasterCountries();
        Task<GetResults> GetCountries(int page, int limit, string searchValue);
        #endregion

        #region Regions
        Task<GetResults> AddUpdateRegion(RegionRequestModel regionRequest);
        Task<GetResults> DeleteRegion(int id);
        Task<GetResults> GetRegionById(int regionId);
        Task<List<RegionModel>> GetMasterRegions(int countryId);
        Task<GetResults> GetRegions(int page, int limit, string searchValue);
        #endregion
    }
}
