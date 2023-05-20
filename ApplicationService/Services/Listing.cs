using ApplicationService.IServices;
using AutoMapper;
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
    public class Listing : IListing
    {
        private BusinessDirectoryDBContext _dbContext;
        private readonly IMapper _mapper;
        public Listing(BusinessDirectoryDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }
        public async Task AddFreeListing(FreeListingModel freeListingModel)
        {
            var freeListing = await _dbContext.FreeListing.FirstOrDefaultAsync(w => w.Id == freeListingModel.Id);
            if (freeListing == null)
            {
                freeListing = new FreeListing
                {
                    CompanyName= freeListingModel.CompanyName,
                    CompanyAddress= freeListingModel.CompanyAddress,
                    CompanyPhone = freeListingModel.CompanyPhone,
                    Pobox = freeListingModel.Pobox,
                    DistrictId = freeListingModel.DistrictId,
                    FoundedYear = freeListingModel.FoundedYear,
                    FounderName = freeListingModel.FounderName,
                    Logo = freeListingModel.Logo,
                    EmployeeNum = freeListingModel.EmployeeNumber,
                    PrimaryWebsite = freeListingModel.PrimaryWebsite,
                    CreatorUserId= freeListingModel.CreatorUserId,
                    CreationTime = DateTime.Now,
                    IsActive = true,
                    PrimaryEmail= freeListingModel.PrimaryEmail,
                };
                _dbContext.FreeListing.Add(freeListing);
                await _dbContext.SaveChangesAsync();
                var listingId = freeListing.Id;

                foreach (var productDetail in freeListingModel.FreeListingProductDetails)
                {
                    FreeListingDetails freeListingDetails = new FreeListingDetails
                    {
                        CategoryId = productDetail.CategoryId,
                        CreationTime = DateTime.Now,
                        CreatorUserId = freeListing.CreatorUserId,
                        IsDeleted = false,
                        FreeListingId = listingId,
                        RelatedProduct = productDetail.RelatedProduct,
                        RelatedService = productDetail.RelatedService,
                    };
                    _dbContext.FreeListingDetails.Add(freeListingDetails);
                }
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
