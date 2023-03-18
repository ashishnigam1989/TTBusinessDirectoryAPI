using ApplicationService.IServices;
using CommonService.RequestModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Threading.Tasks;
using System;

namespace TTBusinessDirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private IOffers _offers;
        protected Logger logger;
        public OffersController(IOffers offers)
        {
            _offers = offers;
            logger = LogManager.GetLogger("Offers");
        }

        [HttpGet]
        [Route("GetAllOffers")]
        public async Task<GetResults> GetAllOffers(int page = 0, int limit = 10)
        {
            GetResults getResults = new GetResults();
            try
            {
                getResults = _offers.GetAllOffers(page, limit, "").Result;
                getResults.IsSuccess = true;
                getResults.Message = "Offers List";
                logger.Info(getResults.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpPost]
        [Route("AddUpdateOffer")]
        public async Task<GetResults> AddUpdateOffer(OffersRequestModel oRequest)
        {
            GetResults getResults = new GetResults();
            try
            {
                var result = _offers.AddUpdateOffer(oRequest).Result;
                logger.Info(result.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("GetOfferById")]
        public async Task<GetResults> GetOfferById(int offerid)
        {
            GetResults getResults = new GetResults();
            try
            {
                getResults = _offers.GetOfferById(offerid).Result;
                getResults.IsSuccess = true;
                getResults.Message = "Offer Details found";
                logger.Info(getResults.Message);
            }
            catch (Exception ex)
            {
                getResults = new GetResults(false, ex.Message);
                logger.Error(ex.Message);
            }
            return await Task.FromResult(getResults);
        }

        [HttpGet]
        [Route("DeleteOffer")]
        public async Task<GetResults> DeleteOffer(int offerid)
        {
            GetResults getResults = new GetResults();
            try
            {
                getResults = _offers.DeleteOffer(offerid).Result;
                logger.Info(getResults.Message);
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
