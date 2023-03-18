using CommonService.RequestModel;
using CommonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.IServices
{
    public interface IOffers
    {
        public Task<GetResults> GetAllOffers(int page, int limit, string searchValue);
        public Task<GetResults> GetOfferById(int offerid);
        public Task<GetResults> AddUpdateOffer(OffersRequestModel oreqmodel);
        public Task<GetResults> DeleteOffer(int offerid);
    }
}
