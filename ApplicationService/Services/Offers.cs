using ApplicationService.IServices;
using AutoMapper;
using CommonService.Constants;
using CommonService.RequestModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services
{
    public class Offers:IOffers
    {
        private BusinessDirectoryDBContext _dbContext;
        private readonly IMapper _mapper;

        public Offers(BusinessDirectoryDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public async Task<GetResults> GetAllOffers(int page, int limit, string searchValue)
        {
            var offerlist = _dbContext.CompanyOffers.Where(w => (string.IsNullOrEmpty(searchValue) ? w.OfferNameEng.ToLower().Contains(searchValue.ToLower()) : w.OfferNameEng == w.OfferNameEng ||
            !string.IsNullOrEmpty(searchValue) ? w.OfferNameArb.ToLower().Contains(searchValue.ToLower()) : w.OfferNameArb == w.OfferNameArb) && w.IsDeleted == false
            ).Select(s => new OffersViewModel
            {
                Id = s.Id,
                OfferNameEng = s.OfferNameEng,
                OfferNameArb = s.OfferNameArb,
                OldPrice = s.OldPrice,
                Price=s.Price
            }).Distinct().OrderByDescending(o => o.Id).Skip(limit * page).Take(limit).ToListAsync().Result;
            int total = _dbContext.CompanyOffers.Where(w => (string.IsNullOrEmpty(searchValue) ? w.OfferNameEng.ToLower().Contains(searchValue.ToLower()) : w.OfferNameEng == w.OfferNameEng ||
                                !string.IsNullOrEmpty(searchValue) ? w.OfferNameArb.ToLower().Contains(searchValue.ToLower()) : w.OfferNameArb == w.OfferNameArb) && w.IsDeleted == false
                                ).CountAsync().Result;

            GetResults result = new GetResults()
            {
                Total = total,
                IsSuccess = true,
                Data = offerlist,
                Message = "Offer list found"
            };

            return await Task.FromResult(result);

        }

        public async Task<GetResults> GetOfferById(int offerid)
        {
            OffersViewModel brand = _dbContext.CompanyOffers.Where(w => w.Id ==offerid && w.IsDeleted == false).Select(s => new OffersViewModel
            {
                Id = s.Id,
                CompanyId = s.CompanyId,
                OfferNameEng = s.OfferNameEng,
                OfferNameArb = s.OfferNameArb,
                OfferShortDescriptionEng = s.OfferShortDescriptionEng,
                OfferShortDescriptionArb = s.OfferShortDescriptionArb,
                OfferDescriptionEng = s.OfferDescriptionEng,
                OfferDescriptionArb = s.OfferDescriptionArb,
                OldPrice = s.OldPrice,
                Price = s.Price,
                OfferDisplayDate = s.OfferDisplayDate,
                OfferStartDate = s.OfferStartDate,
                OfferEndDate = s.OfferEndDate,
                SortOrder = s.SortOrder,
                IsDeleted = s.IsDeleted,
                CreationTime = s.CreationTime,
                CreatorUserId = s.CreatorUserId,
                IsPublished = s.IsPublished.HasValue?s.IsPublished.Value:false,
                Image = s.Image


            }).FirstOrDefaultAsync().Result;

            GetResults result = new GetResults()
            {
                IsSuccess = true,
                Message = "Offer found",
                Data = brand,
                Total = 1
            };
            return await Task.FromResult(result);
        }

        public async Task<GetResults> AddUpdateOffer(OffersRequestModel oreqmodel)
        {
            GetResults result = new GetResults();
            try
            {
                var offerobj = _dbContext.CompanyOffers.Where(w => w.Id == oreqmodel.Id && w.IsDeleted == false).FirstOrDefaultAsync().Result;
                if (offerobj == null)
                {
                    CompanyOffers Oobj = new CompanyOffers()
                    {
                        CompanyId = oreqmodel.CompanyId,
                        OfferNameEng = oreqmodel.OfferNameEng,
                        OfferNameArb = oreqmodel.OfferNameArb,
                        OfferShortDescriptionEng = oreqmodel.OfferShortDescriptionEng,
                        OfferShortDescriptionArb = oreqmodel.OfferShortDescriptionArb,
                        OfferDescriptionEng = oreqmodel.OfferDescriptionEng,
                        OfferDescriptionArb = oreqmodel.OfferDescriptionArb,
                        OldPrice = oreqmodel.OldPrice,
                        Price = oreqmodel.Price,
                        OfferDisplayDate = oreqmodel.OfferDisplayDate,
                        OfferStartDate = oreqmodel.OfferStartDate,
                        OfferEndDate = oreqmodel.OfferEndDate,
                        SortOrder = oreqmodel.SortOrder,
                        IsDeleted = false,
                        //DeleterUserId = oreqmodel.DeleterUserId,
                        //DeletionTime = oreqmodel.DeletionTime,
                        //LastModificationTime = oreqmodel.LastModificationTime,
                        //LastModifierUserId = oreqmodel.LastModifierUserId,
                        CreationTime = oreqmodel.CreationTime,
                        CreatorUserId = CommonConstants.LoggedInUser,
                        IsPublished = oreqmodel.IsPublished,
                        Image = oreqmodel.Image
                    };
                    _dbContext.CompanyOffers.Add(Oobj);
                    result.Message = "Offer added";
                }
                else
                {

                    offerobj.CompanyId = oreqmodel.CompanyId;
                    offerobj.OfferNameEng = oreqmodel.OfferNameEng;
                    offerobj.OfferNameArb = oreqmodel.OfferNameArb;
                    offerobj.OfferShortDescriptionEng = oreqmodel.OfferShortDescriptionEng;
                    offerobj.OfferShortDescriptionArb = oreqmodel.OfferShortDescriptionArb;
                    offerobj.OfferDescriptionEng = oreqmodel.OfferDescriptionEng;
                    offerobj.OfferDescriptionArb = oreqmodel.OfferDescriptionArb;
                    offerobj.OldPrice = oreqmodel.OldPrice;
                    offerobj.Price = oreqmodel.Price;
                    offerobj.OfferDisplayDate = oreqmodel.OfferDisplayDate;
                    offerobj.OfferStartDate = oreqmodel.OfferStartDate;
                    offerobj.OfferEndDate = oreqmodel.OfferEndDate;
                    offerobj.SortOrder = oreqmodel.SortOrder;
                    offerobj.IsDeleted = false;
                    //offerobj.DeleterUserId = oreqmodel.DeleterUserId;
                    //offerobj.DeletionTime = oreqmodel.DeletionTime;
                    offerobj.LastModificationTime = oreqmodel.LastModificationTime;
                    offerobj.LastModifierUserId = CommonConstants.LoggedInUser;
                    //offerobj.CreationTime = oreqmodel.CreationTime;
                    //offerobj.CreatorUserId = oreqmodel.CreatorUserId;
                    offerobj.IsPublished = oreqmodel.IsPublished;
                    offerobj.Image = oreqmodel.Image;


                    result.Message = "Offer updated";

                }
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
            result.IsSuccess = true;
            return await Task.FromResult(result);

        }

        public async Task<GetResults> DeleteOffer(int offerid)
        {
            GetResults result = new GetResults();
            CompanyOffers offer = _dbContext.CompanyOffers.Where(w => w.Id == offerid && w.IsDeleted == false).FirstOrDefaultAsync().Result;

            if (offer != null)
            {
                offer.IsDeleted = true;
                offer.DeletionTime = DateTime.Now;
                offer.DeleterUserId = CommonConstants.LoggedInUser;
                result.Message = "Offer deleted";
            }
            else
            {
                result.Message = "No offer found";
            }

            await _dbContext.SaveChangesAsync();
            result.IsSuccess = true;
            return await Task.FromResult(result);
        }

    }
}
