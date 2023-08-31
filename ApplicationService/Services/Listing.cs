using ApplicationService.IServices;
using AutoMapper;
using CommonService.Enums;
using CommonService.Helpers;
using CommonService.RequestModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<GetResults> AddFreeListing(NewBusinessModel newBusinessDetails)
        {
            GetResults getResults = new GetResults { IsSuccess= false, Message = string.Empty };

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var role = await _dbContext.Roles.FirstOrDefaultAsync(r => !r.IsDeleted && r.Name.ToLower().Equals("free"));
                newBusinessDetails.NewUserDetails.RoleId = role.Id;
                long userId = 0;

                var userRequest = newBusinessDetails.NewUserDetails;
                var userinfo = _dbContext.Users.FirstOrDefault(w => w.EmailAddress.ToLower() == userRequest.EmailAddress.ToLower());
                if(userinfo!= null)
                {
                    getResults.Message = "User already registered. Please login to create listing.";
                    return getResults;
                }

                Users uobj = new Users
                {
                    Name = userRequest.Name,
                    EmailAddress = userRequest.EmailAddress,
                    Designation = userRequest.Designation,
                    Password = userRequest.Password,
                    Mobile = userRequest.Mobile,
                    CountryCode = userRequest.CountryCode,
                    IsEmailConfirmed = false,
                    LastLoginTime = DateTime.Now,
                    IsDeleted = false,
                    CreationTime = DateTime.Now,
                    IsActive = true,
                };
                _dbContext.Users.Add(uobj);
                await _dbContext.SaveChangesAsync();
                userId = uobj.Id;
                _dbContext.UserRoles.Add(new UserRoles
                {
                    UserId = userId,
                    RoleId = userRequest.RoleId,
                    CreatorUserId = userId,
                    CreationTime = DateTime.Now,
                });
                await _dbContext.SaveChangesAsync();

                var freeListingModel = newBusinessDetails.FreeListingDetails;
                newBusinessDetails.FreeListingDetails.CreatorUserId = userId;

                var freeListing = await _dbContext.FreeListing.FirstOrDefaultAsync(w => w.CompanyName == freeListingModel.CompanyName && w.PrimaryEmail == freeListingModel.PrimaryEmail);
                if (freeListing == null)
                {
                    freeListing = new FreeListing
                    {
                        CompanyName = freeListingModel.CompanyName,
                        CompanyAddress = freeListingModel.CompanyAddress,
                        CompanyPhone = freeListingModel.CompanyPhone,
                        Pobox = freeListingModel.Pobox,
                        FoundedYear = Int32.Parse(freeListingModel.FoundedYear),
                        FounderName = freeListingModel.FounderName,
                        Logo = freeListingModel.Logo,
                        EmployeeNum = freeListingModel.EmployeeNumber,
                        PrimaryWebsite = freeListingModel.PrimaryWebsite,
                        CreatorUserId = freeListingModel.CreatorUserId,
                        CreationTime = DateTime.Now,
                        IsActive = true,
                        PrimaryEmail = freeListingModel.PrimaryEmail,
                        CountryId = freeListingModel.CountryId,
                        RegionId = freeListingModel.RegionId,
                        DistrictId = freeListingModel.DistrictId, // Only for iIndia
                    };
                    _dbContext.FreeListing.Add(freeListing);
                    await _dbContext.SaveChangesAsync();
                    var listingId = freeListing.Id;
                    Helper.MoveFileToS3Server(EnumImageType.FreeListingLogo, listingId, freeListingModel.Logo);


                    foreach (var productDetail in freeListingModel.FreeListingProductDetails)
                    {
                        FreeListingDetails freeListingDetails = new FreeListingDetails
                        {
                            CategoryId = long.Parse( productDetail.CategoryId),
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
                transaction.Commit();
                getResults.IsSuccess = true;
                getResults.Message = "Free listing added successfully.";
                getResults.Data = "Free listing added successfully.";
                return getResults;
            }
        }
        
    }
}
