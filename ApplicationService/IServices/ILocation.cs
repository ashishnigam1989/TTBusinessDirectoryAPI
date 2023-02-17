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
        Task<List<CountryModel>> GetCountries();

        #region Regions
        Task<bool> AddRegion(RegionRequestModel regionRequest);
        Task<bool> EditRegion(RegionRequestModel regionRequest);
        Task<RegionModel> GetRegionById(int regionId);
        Task<List<RegionModel>> GetRegions(int countryId);
        #endregion
    }
}
