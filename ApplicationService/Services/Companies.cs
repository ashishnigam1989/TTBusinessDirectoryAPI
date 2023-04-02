using ApplicationService.IServices;
using AutoMapper;
using CommonService.RequestModel;
using CommonService.ViewModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using Microsoft.EntityFrameworkCore;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services
{
    public class Companies : ICompanies
    {
        private BusinessDirectoryDBContext _dbContext;
        private readonly IMapper _mapper;
        public Companies(BusinessDirectoryDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }
        #region Company
        public async Task<GetResults> GetAllCompanies(int page, int limit, string searchValue)
        {
            int total = 0;
            List<CompanyModel> companylist = _dbContext.Company.Where(w =>
           (!string.IsNullOrEmpty(searchValue) ? w.NameEng.ToLower().Contains(searchValue.ToLower()) : w.NameEng == w.NameEng ||
            !string.IsNullOrEmpty(searchValue) ? w.NameArb.ToLower().Contains(searchValue.ToLower()) : w.NameArb == w.NameArb ||
            !string.IsNullOrEmpty(searchValue) ? w.PrimaryEmail.ToLower().Contains(searchValue.ToLower()) : w.PrimaryEmail == w.PrimaryEmail ||
            !string.IsNullOrEmpty(searchValue) ? w.PrimaryPhone.ToLower().Contains(searchValue.ToLower()) : w.PrimaryPhone == w.PrimaryPhone) && w.IsDeleted == false
            ).Select(s => new CompanyModel
            {
                NameEng = s.NameEng,
                NameArb = s.NameArb,
                PrimaryEmail = s.PrimaryEmail,
                PrimaryPhone = s.PrimaryPhone,
                IsVerified = s.IsVerified,
                id = s.Id
            }).Distinct().OrderByDescending(o => o.id).Skip(limit * page).Take(limit).ToListAsync().Result;

            total = _dbContext.Company.Where(w => w.IsDeleted == false).Where(w =>
                                    !string.IsNullOrEmpty(searchValue) ? w.NameEng.ToLower().Contains(searchValue.ToLower()) : w.NameEng == w.NameEng ||
                                    !string.IsNullOrEmpty(searchValue) ? w.NameArb.ToLower().Contains(searchValue.ToLower()) : w.NameArb == w.NameArb ||
                                    !string.IsNullOrEmpty(searchValue) ? w.PrimaryEmail.ToLower().Contains(searchValue.ToLower()) : w.PrimaryEmail == w.PrimaryEmail ||
                                    !string.IsNullOrEmpty(searchValue) ? w.PrimaryPhone.ToLower().Contains(searchValue.ToLower()) : w.PrimaryPhone == w.PrimaryPhone
                                    ).CountAsync().Result;

            GetResults uobj = new GetResults
            {
                Total = total,
                Data = companylist
            };
            return await Task.FromResult(uobj);

        }
        public async Task<GetResults> GetCompanyById(int id)
        {
            CompanyRequestModel company = _dbContext.Company.Where(w => w.Id == id && w.IsDeleted == false).Select(s => new CompanyRequestModel
            {

                Id = s.Id,
                NameEng = s.NameEng,
                NameArb = s.NameArb,
                ShortDescriptionEng = s.ShortDescriptionEng,
                ShortDescriptionArb = s.ShortDescriptionArb,
                PrimaryPhone = s.PrimaryPhone,
                PrimaryEmail = s.PrimaryEmail,
                PrimaryWebsite = s.PrimaryWebsite,
                IsVerified = s.IsVerified.HasValue ? s.IsVerified.Value : false,
                VerifiedUserId = s.VerifiedUserId,
                VerifiedTime = s.VerifiedTime,
                IsGreen = s.IsGreen.HasValue ? s.IsGreen.Value : false,
                FacebookUrl = s.FacebookUrl,
                LinkedInUrl = s.LinkedInUrl,
                TwitterUrl = s.TwitterUrl,
                GooglePlusUrl = s.GooglePlusUrl,
                InstagramUrl = s.InstagramUrl,
                HasOffers = s.HasOffers.HasValue ? s.HasOffers.Value : false,
                OfferUpdatedTime = s.OfferUpdatedTime,
                HasCoupons = s.HasCoupons.HasValue ? s.HasCoupons.Value : false,
                CouponUpdatedTime = s.CouponUpdatedTime,
                HasVideos = s.HasVideos.HasValue ? s.HasVideos.Value : false,
                PrimaryGpsLocation = s.PrimaryGpsLocation,
                DescriptionEng = s.DescriptionEng,
                DescriptionArb = s.DescriptionArb,
                Logo = s.Logo,
                TradeLicenceNumber = s.TradeLicenceNumber,
                MetaKeywords = s.MetaKeywords,
                MetaDescription = s.MetaDescription,
                MetaTitle = s.MetaTitle,
                GooglePlaystoreUrl = s.GooglePlaystoreUrl,
                AppleStoreUrl = s.AppleStoreUrl,
                WindowsStoreUrl = s.WindowsStoreUrl,
                BlackBerryStoreUrl = s.BlackBerryStoreUrl,
                OverallRating = s.OverallRating,
                TotalReviews = s.TotalReviews,
                RegionId = s.RegionId,
                UniqueName = s.UniqueName,
                BrochureLink = s.BrochureLink,
                ThemeColor = s.ThemeColor,
                IsDeleted = s.IsDeleted,
                DeleterUserId = s.DeleterUserId,
                DeletionTime = s.DeletionTime,
                LastModificationTime = s.LastModificationTime,
                LastModifierUserId = s.LastModifierUserId,
                CreationTime = s.CreationTime,
                CreatorUserId = s.CreatorUserId,
                IsPublished = s.IsPublished.HasValue ? s.IsPublished.Value : false,
                DomainName = s.DomainName,
                PrimaryMobile = s.PrimaryMobile,
                Address = s.Address,
                Pobox = s.Pobox,
                PrimaryFax = s.PrimaryFax,
                TotalProfileViews = s.TotalProfileViews,
                Iso = s.Iso,
                EstablishmentDate = s.EstablishmentDate,
                IsFeatured = s.IsFeatured.HasValue ? s.IsFeatured.Value : false,


            }).FirstOrDefaultAsync().Result;

            GetResults uobj = new GetResults
            {
                Total = 1,
                Data = company
            };
            return await Task.FromResult(uobj);
        }
        public async Task<GetResults> CreateUpdateCompany(CompanyRequestModel creqmodel)
        {
            GetResults gobj = new GetResults();
            var cdetail = _dbContext.Company.Where(w => w.Id == creqmodel.Id && w.IsDeleted == false).FirstOrDefaultAsync().Result;
            if (cdetail == null)
            {
                var validCompany = _dbContext.Company.Where(w => w.NameEng == creqmodel.NameEng && w.IsDeleted == false).FirstOrDefaultAsync().Result;
                if (validCompany == null)
                {
                    Company cobj = new Company()
                    {
                        NameEng = creqmodel.NameEng,
                        NameArb = creqmodel.NameArb,
                        ShortDescriptionEng = creqmodel.ShortDescriptionEng,
                        ShortDescriptionArb = creqmodel.ShortDescriptionArb,
                        PrimaryPhone = creqmodel.PrimaryPhone,
                        PrimaryEmail = creqmodel.PrimaryEmail,
                        PrimaryWebsite = creqmodel.PrimaryWebsite,
                        IsGreen = creqmodel.IsGreen,
                        FacebookUrl = creqmodel.FacebookUrl,
                        LinkedInUrl = creqmodel.LinkedInUrl,
                        TwitterUrl = creqmodel.TwitterUrl,
                        GooglePlusUrl = creqmodel.GooglePlusUrl,
                        InstagramUrl = creqmodel.InstagramUrl,
                        HasOffers = creqmodel.HasOffers,
                        //OfferUpdatedTime = creqmodel.OfferUpdatedTime,
                        HasCoupons = creqmodel.HasCoupons,
                        //CouponUpdatedTime = creqmodel.CouponUpdatedTime,
                        HasVideos = creqmodel.HasVideos,
                        PrimaryGpsLocation = creqmodel.PrimaryGpsLocation,
                        DescriptionEng = creqmodel.DescriptionEng,
                        DescriptionArb = creqmodel.DescriptionArb,
                        Logo = creqmodel.Logo,
                        TradeLicenceNumber = creqmodel.TradeLicenceNumber,
                        MetaKeywords = creqmodel.MetaKeywords,
                        MetaDescription = creqmodel.MetaDescription,
                        MetaTitle = creqmodel.MetaTitle,
                        GooglePlaystoreUrl = creqmodel.GooglePlaystoreUrl,
                        AppleStoreUrl = creqmodel.AppleStoreUrl,
                        WindowsStoreUrl = creqmodel.WindowsStoreUrl,
                        BlackBerryStoreUrl = creqmodel.BlackBerryStoreUrl,
                        OverallRating = creqmodel.OverallRating,
                        TotalReviews = creqmodel.TotalReviews,
                        RegionId = creqmodel.RegionId,
                        UniqueName = creqmodel.UniqueName,
                        BrochureLink = creqmodel.BrochureLink,
                        ThemeColor = creqmodel.ThemeColor,
                        IsDeleted = false,
                        CreationTime = DateTime.Now,
                        CreatorUserId = creqmodel.CreatorUserId,
                        IsPublished = creqmodel.IsPublished,
                        DomainName = creqmodel.DomainName,
                        PrimaryMobile = creqmodel.PrimaryMobile,
                        Address = creqmodel.Address,
                        Pobox = creqmodel.Pobox,
                        PrimaryFax = creqmodel.PrimaryFax,
                        TotalProfileViews = creqmodel.TotalProfileViews,
                        Iso = creqmodel.Iso,
                        EstablishmentDate = creqmodel.EstablishmentDate,
                        IsFeatured = creqmodel.IsFeatured,
                        IsVerified = creqmodel.IsVerified

                    };
                    _dbContext.Company.Add(cobj);
                    gobj = new GetResults()
                    {
                        IsSuccess = true,
                        Message = "Company Saved Successfully"
                    };
                }
                else
                {
                    gobj = new GetResults()
                    {
                        IsSuccess = true,
                        Message = creqmodel.NameEng + " is already exists !!!"
                    };
                }
            }
            else
            {
                var validCompany = _dbContext.Company.Where(w => w.NameEng == creqmodel.NameEng && w.Id != creqmodel.Id && w.IsDeleted == false).FirstOrDefaultAsync().Result;
                if (validCompany == null)
                {
                    cdetail.NameEng = creqmodel.NameEng;
                    cdetail.NameArb = creqmodel.NameArb;
                    cdetail.ShortDescriptionEng = creqmodel.ShortDescriptionEng;
                    cdetail.ShortDescriptionArb = creqmodel.ShortDescriptionArb;
                    cdetail.PrimaryPhone = creqmodel.PrimaryPhone;
                    cdetail.PrimaryEmail = creqmodel.PrimaryEmail;
                    cdetail.PrimaryWebsite = creqmodel.PrimaryWebsite;
                    cdetail.IsVerified = false;
                    cdetail.IsGreen = creqmodel.IsGreen;
                    cdetail.FacebookUrl = creqmodel.FacebookUrl;
                    cdetail.LinkedInUrl = creqmodel.LinkedInUrl;
                    cdetail.TwitterUrl = creqmodel.TwitterUrl;
                    cdetail.GooglePlusUrl = creqmodel.GooglePlusUrl;
                    cdetail.InstagramUrl = creqmodel.InstagramUrl;
                    cdetail.HasOffers = creqmodel.HasOffers;
                    cdetail.OfferUpdatedTime = creqmodel.OfferUpdatedTime;
                    cdetail.HasCoupons = creqmodel.HasCoupons;
                    cdetail.CouponUpdatedTime = creqmodel.CouponUpdatedTime;
                    cdetail.HasVideos = creqmodel.HasVideos;
                    cdetail.PrimaryGpsLocation = creqmodel.PrimaryGpsLocation;
                    cdetail.DescriptionEng = creqmodel.DescriptionEng;
                    cdetail.DescriptionArb = creqmodel.DescriptionArb;
                    cdetail.Logo = creqmodel.Logo;
                    cdetail.TradeLicenceNumber = creqmodel.TradeLicenceNumber;
                    cdetail.MetaKeywords = creqmodel.MetaKeywords;
                    cdetail.MetaDescription = creqmodel.MetaDescription;
                    cdetail.MetaTitle = creqmodel.MetaTitle;
                    cdetail.GooglePlaystoreUrl = creqmodel.GooglePlaystoreUrl;
                    cdetail.AppleStoreUrl = creqmodel.AppleStoreUrl;
                    cdetail.WindowsStoreUrl = creqmodel.WindowsStoreUrl;
                    cdetail.BlackBerryStoreUrl = creqmodel.BlackBerryStoreUrl;
                    cdetail.OverallRating = creqmodel.OverallRating;
                    cdetail.TotalReviews = creqmodel.TotalReviews;
                    cdetail.RegionId = creqmodel.RegionId;
                    cdetail.UniqueName = creqmodel.UniqueName;
                    cdetail.BrochureLink = creqmodel.BrochureLink;
                    cdetail.ThemeColor = creqmodel.ThemeColor;
                    cdetail.IsDeleted = false;
                    cdetail.LastModificationTime = DateTime.Now;
                    cdetail.LastModifierUserId = creqmodel.LastModifierUserId;
                    cdetail.IsPublished = creqmodel.IsPublished;
                    cdetail.DomainName = creqmodel.DomainName;
                    cdetail.PrimaryMobile = creqmodel.PrimaryMobile;
                    cdetail.Address = creqmodel.Address;
                    cdetail.Pobox = creqmodel.Pobox;
                    cdetail.PrimaryFax = creqmodel.PrimaryFax;
                    cdetail.TotalProfileViews = creqmodel.TotalProfileViews;
                    cdetail.Iso = creqmodel.Iso;
                    cdetail.EstablishmentDate = creqmodel.EstablishmentDate;
                    cdetail.IsFeatured = creqmodel.IsFeatured;
                    cdetail.IsVerified = creqmodel.IsVerified;
                    gobj = new GetResults()
                    {
                        IsSuccess = true,
                        Message = "Company Updated Successfully"
                    };
                }
                else
                {
                    gobj = new GetResults()
                    {
                        IsSuccess = true,
                        Message = creqmodel.NameEng + " is already exists !!!"
                    };
                }
            }
            try
            {

                var i = _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
            return await Task.FromResult(gobj);
        }
        public async Task<GetResults> DeleteCompany(int id)
        {
            GetResults gobj = new GetResults();
            var cdetail = _dbContext.Company.Where(w => w.Id == id).FirstOrDefaultAsync().Result;
            if (cdetail != null)
            {
                cdetail.IsDeleted = true;
                cdetail.DeletionTime = DateTime.Now;
                _dbContext.SaveChanges();
                gobj = new GetResults()
                {
                    IsSuccess = true,
                    Message = "Company Deleted Successfully."
                };
            }
            else
            {
                gobj = new GetResults()
                {
                    IsSuccess = false,
                    Message = "Company Details Not Found."
                };
            }
            return await Task.FromResult(gobj);
        }
        public async Task<GetResults> VerifyCompany(int id)
        {
            GetResults gobj = new GetResults();
            var cdetail = _dbContext.Company.Where(w => w.Id == id).FirstOrDefaultAsync().Result;
            if (cdetail != null)
            {
                cdetail.IsVerified = true;
                cdetail.VerifiedTime = DateTime.Now;
                _dbContext.SaveChanges();
                gobj = new GetResults()
                {
                    IsSuccess = true,
                    Message = "Company Verified Successfully."
                };
            }
            else
            {
                gobj = new GetResults()
                {
                    IsSuccess = false,
                    Message = "Company Details Not Found."
                };
            }
            return await Task.FromResult(gobj);
        }

        public async Task<GetResults> GetMasterCompanies()
        {
            List<CompanyModel> companylist = _dbContext.Company.Where(w => w.IsDeleted == false).Select(s => new CompanyModel
            {
                NameEng = s.NameEng,
                id = s.Id
            }).Distinct().OrderByDescending(o => o.id).Take(10).ToListAsync().Result;


            GetResults uobj = new GetResults
            {
                Total = companylist.Count,
                Data = companylist
            };
            return await Task.FromResult(uobj);

        }
        #endregion

        #region CompanyBrand
        public async Task<GetResults> AddEditCompanyBrand(CompanyBrandRequestModel cbModel)
        {
            GetResults result = new GetResults();
            var cbObj = _dbContext.CompanyBrand.Where(w => w.Id == cbModel.Id && w.IsDeleted == false).FirstOrDefaultAsync().Result;
            if (cbObj == null)
            {
                CompanyBrand cc = new CompanyBrand()
                {
                    CompanyId = cbModel.CompanyId,
                    BrandId = cbModel.BrandId,
                    IsPublished = cbModel.IsPublished,
                    CreationTime = DateTime.Now,
                    CreatorUserId = cbModel.CreatorUserId
                };
                _dbContext.CompanyBrand.Add(cc);
                result.Message = "Company Brand Added.";
            }
            else
            {
                cbObj.CompanyId = cbModel.CompanyId;
                cbObj.BrandId = cbModel.BrandId;
                cbObj.IsPublished = cbModel.IsPublished;
                cbObj.LastModificationTime = DateTime.Now;
                cbObj.LastModifierUserId = cbModel.LastModifierUserId;
                result.Message = "Company Brand Updated.";
            }
            _dbContext.SaveChanges();
            result.IsSuccess = true;
            return await Task.FromResult(result);

        }
        public async Task<GetResults> DeleteCompanyBrand(int Id)
        {
            GetResults result = new GetResults();
            var cbObj = _dbContext.CompanyBrand.Where(w => w.Id == Id && w.IsDeleted == false).FirstOrDefaultAsync().Result;
            if (cbObj != null)
            {
                cbObj.IsDeleted = true;
                cbObj.DeletionTime = DateTime.Now;
                result.Message = "Company Brand Deleted.";
                result.IsSuccess = true;
            }
            _dbContext.SaveChanges();
            return await Task.FromResult(result);

        }
        public async Task<GetResults> GetAllCompanyBrand(int page, int limit, string searchValue)
        {
            GetResults result = new GetResults();
            var cbList = _dbContext.CompanyBrand.Join(_dbContext.Company, cb => cb.CompanyId, cmny => cmny.Id, (cb, cmny) => new { cb, cmny })
                            .Join(_dbContext.Brand, ccat => ccat.cb.BrandId, brnd => brnd.Id, (ccat, brnd) => new { ccat, brnd }).
                            Where(w => w.ccat.cb.IsDeleted == false && (
                            (!string.IsNullOrEmpty(searchValue) ? w.ccat.cmny.NameEng.Contains(searchValue) : w.ccat.cmny.NameEng == w.ccat.cmny.NameEng)
                            || (!string.IsNullOrEmpty(searchValue) ? w.brnd.NameEng.Contains(searchValue) : w.brnd.NameEng == w.brnd.NameEng)
                            )).
                            Select(s => new CompanyBrandViewModel
                            {
                                Id = s.ccat.cb.Id,
                                Company = s.ccat.cmny.NameEng,
                                Brand = s.brnd.NameEng
                            }).Distinct().OrderByDescending(o => o.Id).Skip(limit * page).Take(limit).ToListAsync().Result;


            var tot = _dbContext.CompanyBrand.Join(_dbContext.Company, cb => cb.CompanyId, cmny => cmny.Id, (cb, cmny) => new { cb, cmny })
                            .Join(_dbContext.Brand, ccat => ccat.cb.BrandId, brnd => brnd.Id, (ccat, brnd) => new { ccat, brnd }).
                            Where(w => w.ccat.cb.IsDeleted == false && (
                            (!string.IsNullOrEmpty(searchValue) ? w.ccat.cmny.NameEng.Contains(searchValue) : w.ccat.cmny.NameEng == w.ccat.cmny.NameEng)
                            || (!string.IsNullOrEmpty(searchValue) ? w.brnd.NameEng.Contains(searchValue) : w.brnd.NameEng == w.brnd.NameEng)
                            )).CountAsync().Result;

            result.IsSuccess = true;
            result.Message = "Company Brand List.";
            result.Data = cbList;
            result.Total = tot;
            return await Task.FromResult(result);
        }
        public async Task<GetResults> GetCompanyBrandById(int id)
        {
            GetResults result = new GetResults();
            var cb = _dbContext.CompanyBrand.Where(w => w.Id == id && w.IsDeleted == false)
                                                   .Select(s => new CompanyBrandRequestModel
                                                   {
                                                       Id = s.Id,
                                                       BrandId = s.BrandId,
                                                       CompanyId = s.CompanyId,
                                                       IsPublished = s.IsPublished
                                                   }).FirstOrDefaultAsync().Result;
            result.IsSuccess = true;
            result.Message = "Brand Found.";
            result.Data = cb;
            result.Total = 1;
            return await Task.FromResult(result);
        }
        #endregion

        #region CompanyCategory
        public async Task<GetResults> AddEditCompanyCategory(CompanyCategoryRequestModel ccModel)
        {
            GetResults result = new GetResults();
            var ccObj = _dbContext.CompanyCategory.Where(w => w.Id == ccModel.Id && w.IsDeleted == false).FirstOrDefaultAsync().Result;
            if (ccObj == null)
            {
                CompanyCategory cc = new CompanyCategory()
                {
                    CompanyId = ccModel.CompanyId,
                    CategoryId = ccModel.CategoryId,
                    IsPublished = ccModel.IsPublished,
                    CreationTime = DateTime.Now,
                    CreatorUserId = ccModel.CreatorUserId
                };
                _dbContext.CompanyCategory.Add(cc);
                result.Message = "Company Category Added.";
            }
            else
            {
                ccObj.CompanyId = ccModel.CompanyId;
                ccObj.CategoryId = ccModel.CategoryId;
                ccObj.IsPublished = ccModel.IsPublished;
                ccObj.LastModificationTime = DateTime.Now;
                ccObj.LastModifierUserId = ccModel.LastModifierUserId;
                result.Message = "Company Category Updated.";
            }
            _dbContext.SaveChanges();
            result.IsSuccess = true;
            return await Task.FromResult(result);

        }
        public async Task<GetResults> DeleteCompanyCategory(int Id)
        {
            GetResults result = new GetResults();
            var ccObj = _dbContext.CompanyCategory.Where(w => w.Id == Id && w.IsDeleted == false).FirstOrDefaultAsync().Result;
            if (ccObj != null)
            {
                ccObj.IsDeleted = true;
                ccObj.DeletionTime = DateTime.Now;
                result.Message = "Company Category Deleted.";
                result.IsSuccess = true;
            }
            _dbContext.SaveChanges();
            return await Task.FromResult(result);

        }
        public async Task<GetResults> GetAllCompanyCategory(int page, int limit, string searchValue)
        {
            GetResults result = new GetResults();
            var ccList = _dbContext.CompanyCategory.Join(_dbContext.Company, cc => cc.CompanyId, cmny => cmny.Id, (cc, cmny) => new { cc, cmny })
                            .Join(_dbContext.Category, ccat => ccat.cc.CategoryId, cat => cat.Id, (ccat, cat) => new { ccat, cat }).
                            Where(w => w.ccat.cc.IsDeleted == false && (
                            (!string.IsNullOrEmpty(searchValue) ? w.ccat.cmny.NameEng.Contains(searchValue) : w.ccat.cmny.NameEng == w.ccat.cmny.NameEng)
                            || (!string.IsNullOrEmpty(searchValue) ? w.cat.NameEng.Contains(searchValue) : w.cat.NameEng == w.cat.NameEng))).
                            Select(s => new CompanyCategoryViewModel
                            {
                                Id = s.ccat.cc.Id,
                                Company = s.ccat.cmny.NameEng,
                                Category = s.cat.NameEng
                            }).Distinct().OrderByDescending(o => o.Id).Skip(limit * page).Take(limit).ToListAsync().Result;

            var tot = _dbContext.CompanyCategory.Join(_dbContext.Company, cc => cc.CompanyId, cmny => cmny.Id, (cc, cmny) => new { cc, cmny })
                           .Join(_dbContext.Category, ccat => ccat.cc.CategoryId, cat => cat.Id, (ccat, cat) => new { ccat, cat }).
                           Where(w => w.ccat.cc.IsDeleted == false && (
                           (!string.IsNullOrEmpty(searchValue) ? w.ccat.cmny.NameEng.Contains(searchValue) : w.ccat.cmny.NameEng == w.ccat.cmny.NameEng)
                           || (!string.IsNullOrEmpty(searchValue) ? w.cat.NameEng.Contains(searchValue) : w.cat.NameEng == w.cat.NameEng))).CountAsync().Result;


            result.IsSuccess = true;
            result.Message = "Company Category List.";
            result.Data = ccList;
            result.Total = tot;
            return await Task.FromResult(result);
        }
        public async Task<GetResults> GetCompanyCategoryById(int id)
        {
            GetResults result = new GetResults();
            var ccList = _dbContext.CompanyCategory.Where(w => w.Id == id && w.IsDeleted == false)
                                                   .Select(s => new CompanyCategoryRequestModel
                                                   {
                                                       Id = s.Id,
                                                       CategoryId = s.CategoryId,
                                                       CompanyId = s.CompanyId,
                                                       IsPublished =s.IsPublished.HasValue?s.IsPublished.Value:false
                                                   }).FirstOrDefaultAsync().Result;
            result.IsSuccess = true;
            result.Message = "Company Found.";
            result.Data = ccList;
            result.Total = 1;
            return await Task.FromResult(result);
        }
        #endregion
    }
}
