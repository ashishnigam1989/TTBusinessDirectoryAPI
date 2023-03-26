using ApplicationService.IServices;
using AutoMapper;
using CommonService.RequestModel;
using CommonService.ViewModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NLog.Fluent;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
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
        public async Task<GetResults> GetAllCompanies()
        {
            int total = 0;
            var companylist = _dbContext.Company.Where(w=>w.IsDeleted==false).Select(s => new
            {
                NameEng = s.NameEng,
                id = s.Id
            }).ToListAsync().Result;


            GetResults uobj = new GetResults
            {
                Total = total,
                Data = companylist
            };
            return await Task.FromResult(uobj);

        }

        #region Categories

        public async Task<GetResults> GetAllCategories(int page, int limit, string searchValue)
        {
            List<CategoriesViewModel> listcategory = _dbContext.Category.Where(w => w.IsDeleted == false && (
           (!string.IsNullOrEmpty(searchValue) ? w.NameEng.ToLower().Contains(searchValue.ToLower()) : w.NameEng == w.NameEng ||
            !string.IsNullOrEmpty(searchValue) ? w.NameArb.ToLower().Contains(searchValue.ToLower()) : w.NameArb == w.NameArb)))
                .Select(s => new CategoriesViewModel
                {
                    Id = s.Id,
                    NameEng = s.NameEng,
                    NameArb = s.NameArb,
                    Unspsccode = s.Unspsccode,
                    IsPublished = s.IsPublished.HasValue ? s.IsPublished.Value : false,
                    IsDeleted = s.IsDeleted,
                    DeleterUserId = s.DeleterUserId,
                    DeletionTime = s.DeletionTime,
                    LastModificationTime = s.LastModificationTime,
                    LastModifierUserId = s.LastModifierUserId,
                    CreationTime = s.CreationTime,
                    CreatorUserId = s.CreatorUserId,
                    Keywords = s.Keywords,
                    SuggestionHits = s.SuggestionHits,
                    Slug = s.Slug,
                    SeoEnabled = s.SeoEnabled,
                    MetaTitleEng = s.MetaTitleEng,
                    MetaDescriptionEng = s.MetaDescriptionEng,
                    PageContentEng = s.PageContentEng,
                    MetaTitleArb = s.MetaTitleArb,
                    MetaDescriptionArb = s.MetaDescriptionArb,
                    PageContentArb = s.PageContentArb,
                    IsFeatured = s.IsFeatured.HasValue ? s.IsFeatured.Value : false

                }).Distinct().OrderByDescending(o => o.Id).Skip(limit * page).Take(limit).ToListAsync().Result;

            int total = _dbContext.Category.Where(w => w.IsDeleted == false && (
                                  !string.IsNullOrEmpty(searchValue) ? w.NameEng.ToLower().Contains(searchValue.ToLower()) : w.NameEng == w.NameEng ||
                                  !string.IsNullOrEmpty(searchValue) ? w.NameArb.ToLower().Contains(searchValue.ToLower()) : w.NameArb == w.NameArb)
                                  ).CountAsync().Result;

            GetResults result = new GetResults
            {
                Data = listcategory,
                Total = total
            };
            return await Task.FromResult(result);

        }
        public async Task<GetResults> CreateUpdateCategory(CategoriesRequestModel crModel)
        {
            GetResults resp = new GetResults();
            var cdata = _dbContext.Category.Where(w => w.Id == crModel.Id).FirstOrDefaultAsync().Result;
            if (cdata == null)
            {
                Category crobj = new Category()
                {
                    NameEng = crModel.NameEng,
                    NameArb = crModel.NameArb,
                    Unspsccode = crModel.Unspsccode,
                    IsPublished = crModel.IsPublished,
                    CreationTime = DateTime.Now,
                    CreatorUserId = crModel.CreatorUserId,
                    Keywords = crModel.Keywords,
                    SuggestionHits = crModel.SuggestionHits,
                    Slug = crModel.Slug,
                    SeoEnabled = crModel.SeoEnabled,
                    MetaTitleEng = crModel.MetaTitleEng,
                    MetaDescriptionEng = crModel.MetaDescriptionEng,
                    PageContentEng = crModel.PageContentEng,
                    MetaTitleArb = crModel.MetaTitleArb,
                    MetaDescriptionArb = crModel.MetaDescriptionArb,
                    PageContentArb = crModel.PageContentArb,
                    IsFeatured = crModel.IsFeatured
                };
                _dbContext.Category.Add(crobj);
                await _dbContext.SaveChangesAsync();
                resp.Message = "Category Added.";
                resp.IsSuccess = true;

            }
            else
            {
                cdata.NameEng = crModel.NameEng;
                cdata.NameArb = crModel.NameArb;
                cdata.Unspsccode = crModel.Unspsccode;
                cdata.IsPublished = crModel.IsPublished;
                cdata.LastModificationTime = DateTime.Now;
                cdata.LastModifierUserId = crModel.LastModifierUserId;
                cdata.Keywords = crModel.Keywords;
                cdata.SuggestionHits = crModel.SuggestionHits;
                cdata.Slug = crModel.Slug;
                cdata.SeoEnabled = crModel.SeoEnabled;
                cdata.MetaTitleEng = crModel.MetaTitleEng;
                cdata.MetaDescriptionEng = crModel.MetaDescriptionEng;
                cdata.PageContentEng = crModel.PageContentEng;
                cdata.MetaTitleArb = crModel.MetaTitleArb;
                cdata.MetaDescriptionArb = crModel.MetaDescriptionArb;
                cdata.PageContentArb = crModel.PageContentArb;
                cdata.IsFeatured = crModel.IsFeatured;
                await _dbContext.SaveChangesAsync();
                resp.Message = "Category Added.";
                resp.IsSuccess = true;
            }

            return await Task.FromResult(resp);
        }
        public async Task<GetResults> GetCategoryById(int id)
        {
            CategoriesViewModel category = _dbContext.Category.Where(w => w.Id == id && w.IsDeleted == false)
               .Select(s => new CategoriesViewModel
               {
                   Id = s.Id,
                   NameEng = s.NameEng,
                   NameArb = s.NameArb,
                   Unspsccode = s.Unspsccode,
                   IsPublished = s.IsPublished.HasValue ? s.IsPublished.Value : false,
                   IsDeleted = s.IsDeleted,
                   DeleterUserId = s.DeleterUserId,
                   DeletionTime = s.DeletionTime,
                   LastModificationTime = s.LastModificationTime,
                   LastModifierUserId = s.LastModifierUserId,
                   CreationTime = s.CreationTime,
                   CreatorUserId = s.CreatorUserId,
                   Keywords = s.Keywords,
                   SuggestionHits = s.SuggestionHits,
                   Slug = s.Slug,
                   SeoEnabled = s.SeoEnabled,
                   MetaTitleEng = s.MetaTitleEng,
                   MetaDescriptionEng = s.MetaDescriptionEng,
                   PageContentEng = s.PageContentEng,
                   MetaTitleArb = s.MetaTitleArb,
                   MetaDescriptionArb = s.MetaDescriptionArb,
                   PageContentArb = s.PageContentArb,
                   IsFeatured = s.IsFeatured.HasValue ? s.IsFeatured.Value : false

               }).FirstOrDefaultAsync().Result;
            GetResults result = new GetResults
            {
                Data = category,
                Total = 1
            };
            return await Task.FromResult(result);
        }

        public async Task<GetResults> DeleteCategory(int id)
        {
            GetResults result = new GetResults();
            var category = _dbContext.Category.Where(w => w.Id == id).FirstOrDefaultAsync().Result;
            if (category != null)
            {
                category.IsDeleted = true;
                category.DeletionTime = DateTime.Now;
                _dbContext.SaveChanges();
                result.IsSuccess = true;
                result.Message = "Category deleted.";
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "Category not found.";
            }

            return await Task.FromResult(result);
        }


        #endregion


        #region CompanyCategory
        public async Task<GetResults> AddEditCompanyCategory(CompanyCategoryRequestModel ccModel)
        {
            GetResults result = new GetResults();
            var ccObj = _dbContext.CompanyCategory.Where(w => w.Id == ccModel.Id && w.IsDeleted == true).FirstOrDefaultAsync().Result;
            if (ccObj == null)
            {
                CompanyCategory cc = new CompanyCategory()
                {
                    CompanyId = ccModel.CompanyId,
                    CategoryId = ccModel.CategoryId,
                    IsPublished = ccModel.IsPublished,
                    CreationTime = ccModel.CreationTime,
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
                ccObj.LastModificationTime = ccModel.LastModificationTime;
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
            var ccObj = _dbContext.CompanyCategory.Where(w => w.Id == Id && w.IsDeleted == true).FirstOrDefaultAsync().Result;
            if (ccObj != null)
            {
                ccObj.IsDeleted = true;
                ccObj.DeletionTime = DateTime.Now;
                result.Message = "Company Category Deleted.";
            }
            _dbContext.SaveChanges();
            result.IsSuccess = true;
            return await Task.FromResult(result);

        }
        public async Task<GetResults> GetAllCompanyCategory()
        {
            GetResults result = new GetResults();
            var ccList = _dbContext.CompanyCategory.Join(_dbContext.Company, cc => cc.CompanyId, cmny => cmny.Id, (cc, cmny) => new { cc, cmny })
                            .Join(_dbContext.Category, ccat => ccat.cc.CategoryId, cat => cat.Id, (ccat, cat) => new { ccat, cat }).
                            Where(w => w.ccat.cc.IsDeleted == false).
                            Select(s => new
                            {
                                Id = s.ccat.cc.Id,
                                Company = s.ccat.cmny.NameEng,
                                Category = s.cat.NameEng
                            }).ToListAsync().Result;
            result.IsSuccess = true;
            result.Message = "Company Category List.";
            result.Data = ccList;
            result.Total = ccList.Count;
            return await Task.FromResult(result);
        }
        public async Task<GetResults> GetCompanyCategoryById(int id)
        {
            GetResults result = new GetResults();
            var ccList = _dbContext.CompanyCategory.Where(w => w.Id == id && w.IsDeleted == false)
                                                   .Select(s => new CompanyCategoryViewModel
                                                   {
                                                       Id = s.Id,
                                                       CategoryId = s.CategoryId,
                                                       CompanyId = s.CompanyId,
                                                       IsPublished = s.IsPublished
                                                   }).FirstOrDefaultAsync().Result;
            result.IsSuccess = true;
            result.Message = "Company Found.";
            result.Data = ccList;
            result.Total = 1;
            return await Task.FromResult(result);
        }
        #endregion

        #region CompanyBrand
        public async Task<GetResults> AddEditCompanyBrand(CompanyBrandRequestModel cbModel)
        {
            GetResults result = new GetResults();
            var cbObj = _dbContext.CompanyBrand.Where(w => w.Id == cbModel.Id && w.IsDeleted == true).FirstOrDefaultAsync().Result;
            if (cbObj == null)
            {
                CompanyCategory cc = new CompanyCategory()
                {
                    CompanyId = cbModel.CompanyId,
                    CategoryId = cbModel.BrandId,
                    IsPublished = cbModel.IsPublished,
                    CreationTime = DateTime.Now,
                    CreatorUserId = cbModel.CreatorUserId
                };
                _dbContext.CompanyCategory.Add(cc);
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
            var cbObj = _dbContext.CompanyBrand.Where(w => w.Id == Id && w.IsDeleted == true).FirstOrDefaultAsync().Result;
            if (cbObj != null)
            {
                cbObj.IsDeleted = true;
                cbObj.DeletionTime = DateTime.Now;
                result.Message = "Company Category Deleted.";
            }
            _dbContext.SaveChanges();
            result.IsSuccess = true;
            return await Task.FromResult(result);

        }
        public async Task<GetResults> GetAllCompanyBrand()
        {
            GetResults result = new GetResults();
            var cbList = _dbContext.CompanyBrand.Join(_dbContext.Company, cb => cb.CompanyId, cmny => cmny.Id, (cb, cmny) => new { cb, cmny })
                            .Join(_dbContext.Brand, ccat => ccat.cb.BrandId, brnd => brnd.Id, (ccat, brnd) => new { ccat, brnd }).
                            Where(w => w.ccat.cb.IsDeleted == false).
                            Select(s => new
                            {
                                Id = s.ccat.cb.Id,
                                Company = s.ccat.cmny.NameEng,
                                Category = s.brnd.NameEng
                            }).ToListAsync().Result;
            result.IsSuccess = true;
            result.Message = "Company Brand List.";
            result.Data = cbList;
            result.Total = cbList.Count;
            return await Task.FromResult(result);
        }
        public async Task<GetResults> GetCompanyBrandById(int id)
        {
            GetResults result = new GetResults();
            var cb = _dbContext.CompanyBrand.Where(w => w.Id == id && w.IsDeleted == false)
                                                   .Select(s => new CompanyBrandViewModel
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

        #region CompanyProduct
        public async Task<GetResults> AddEditCompanyProduct(CompanyProductRequestModel cpModel)
        {
            GetResults result = new GetResults();
            var cpObj = _dbContext.CompanyProduct.Where(w => w.Id == cpModel.Id && w.IsDeleted == true).FirstOrDefaultAsync().Result;
            if (cpObj == null)
            {
                CompanyProduct cp = new CompanyProduct()
                {
                    NameEng = cpModel.NameEng,
                    NameArb = cpModel.NameArb,
                    CompanyId = cpModel.CompanyId,
                    ShortDescriptionEng = cpModel.ShortDescriptionEng,
                    ShortDescriptionArb = cpModel.ShortDescriptionArb,
                    DescriptionEng = cpModel.DescriptionEng,
                    DescriptionArb = cpModel.DescriptionArb,
                    PartNumber = cpModel.PartNumber,
                    WarrantyEng = cpModel.WarrantyEng,
                    WarrantyArb = cpModel.WarrantyArb,
                    Image = cpModel.Image,
                    SortOrder = cpModel.SortOrder,
                    IsPublished = cpModel.IsPublished,
                    HasOffers = cpModel.HasOffers,  
                    CreationTime = DateTime.Now,
                    CreatorUserId = cpModel.CreatorUserId,
                    Price = cpModel.Price,
                    OffersDescriptionEng = cpModel.OffersDescriptionEng,
                    OffersDescriptionArb = cpModel.OffersDescriptionArb,
                    CountryId = cpModel.CountryId,
                    OfferStartDate = cpModel.OfferStartDate,
                    OfferEndDate = cpModel.OfferEndDate,
                    OfferShortDescriptionEng = cpModel.OfferShortDescriptionEng,
                    OfferShortDescriptionArb = cpModel.OfferShortDescriptionArb,
                    OldPrice = cpModel.OldPrice,

                };
                _dbContext.CompanyProduct.Add(cp);
                result.Message = "Company Product Added.";
            }
            else
            { 
                cpObj.NameEng = cpModel.NameEng;
                cpObj.NameArb = cpModel.NameArb;
                cpObj.CompanyId = cpModel.CompanyId;
                cpObj.ShortDescriptionEng = cpModel.ShortDescriptionEng;
                cpObj.ShortDescriptionArb = cpModel.ShortDescriptionArb;
                cpObj.DescriptionEng = cpModel.DescriptionEng;
                cpObj.DescriptionArb = cpModel.DescriptionArb;
                cpObj.PartNumber = cpModel.PartNumber;
                cpObj.WarrantyEng = cpModel.WarrantyEng;
                cpObj.WarrantyArb = cpModel.WarrantyArb;
                cpObj.Image = cpModel.Image;
                cpObj.SortOrder = cpModel.SortOrder;
                cpObj.IsPublished = cpModel.IsPublished;
                cpObj.HasOffers = cpModel.HasOffers; 
                cpObj.LastModificationTime = DateTime.Now;
                cpObj.LastModifierUserId = cpModel.LastModifierUserId; 
                cpObj.Price = cpModel.Price;
                cpObj.OffersDescriptionEng = cpModel.OffersDescriptionEng;
                cpObj.OffersDescriptionArb = cpModel.OffersDescriptionArb;
                cpObj.CountryId = cpModel.CountryId;
                cpObj.OfferStartDate = cpModel.OfferStartDate;
                cpObj.OfferEndDate = cpModel.OfferEndDate;
                cpObj.OfferShortDescriptionEng = cpModel.OfferShortDescriptionEng;
                cpObj.OfferShortDescriptionArb = cpModel.OfferShortDescriptionArb;
                cpObj.OldPrice = cpModel.OldPrice;

                result.Message = "Company Product Updated.";
            }
            _dbContext.SaveChanges();
            result.IsSuccess = true;
            return await Task.FromResult(result);

        }
        public async Task<GetResults> DeleteCompanyProduct(int Id)
        {
            GetResults result = new GetResults();
            var cpObj = _dbContext.CompanyProduct.Where(w => w.Id == Id && w.IsDeleted == true).FirstOrDefaultAsync().Result;
            if (cpObj != null)
            {
                cpObj.IsDeleted = true;
                cpObj.DeletionTime = DateTime.Now;
                result.Message = "Company Product Deleted.";
            }
            _dbContext.SaveChanges();
            result.IsSuccess = true;
            return await Task.FromResult(result);

        }
        public async Task<GetResults> GetAllCompanyProduct()
        {
            GetResults result = new GetResults();
            var cpList = _dbContext.CompanyProduct.Join(_dbContext.Company, cp => cp.CompanyId, cmny => cmny.Id, (cp, cmny) => new { cp, cmny }).
                            Where(w => w.cp.IsDeleted == false).
                            Select(s => new
                            {
                                Id = s.cp.Id,
                                Company = s.cmny.NameEng,
                                ProductNameEng = s.cp.NameEng,
                                ProductNameArb = s.cp.NameArb,
                            }).ToListAsync().Result;
            result.IsSuccess = true;
            result.Message = "Company Product List.";
            result.Data = cpList;
            result.Total = cpList.Count;
            return await Task.FromResult(result);
        }
        public async Task<GetResults> GetCompanyProductById(int id)
        {
            GetResults result = new GetResults();
            var cb = _dbContext.CompanyProduct.Where(w => w.Id == id && w.IsDeleted == false)
                                                   .Select(s => new CompanyProductViewModel
                                                   {
                                                       Id = s.Id,
                                                       NameEng = s.NameEng,
                                                       NameArb = s.NameArb,
                                                       CompanyId = s.CompanyId,
                                                       ShortDescriptionEng = s.ShortDescriptionEng,
                                                       ShortDescriptionArb = s.ShortDescriptionArb,
                                                       DescriptionEng = s.DescriptionEng,
                                                       DescriptionArb = s.DescriptionArb,
                                                       PartNumber = s.PartNumber,
                                                       WarrantyEng = s.WarrantyEng,
                                                       WarrantyArb = s.WarrantyArb,
                                                       Image = s.Image,
                                                       SortOrder = s.SortOrder,
                                                       IsPublished = s.IsPublished,
                                                       HasOffers = s.HasOffers,
                                                       IsDeleted = s.IsDeleted,
                                                       DeleterUserId = s.DeleterUserId,
                                                       DeletionTime = s.DeletionTime,
                                                       LastModificationTime = s.LastModificationTime,
                                                       LastModifierUserId = s.LastModifierUserId,
                                                       CreationTime = s.CreationTime,
                                                       CreatorUserId = s.CreatorUserId,
                                                       Price = s.Price,
                                                       OffersDescriptionEng = s.OffersDescriptionEng,
                                                       OffersDescriptionArb = s.OffersDescriptionArb,
                                                       CountryId = s.CountryId,
                                                       OfferStartDate = s.OfferStartDate,
                                                       OfferEndDate = s.OfferEndDate,
                                                       OfferShortDescriptionEng = s.OfferShortDescriptionEng,
                                                       OfferShortDescriptionArb = s.OfferShortDescriptionArb,
                                                       OldPrice = s.OldPrice

                                                   }).FirstOrDefaultAsync().Result;
            result.IsSuccess = true;
            result.Message = "Company Product Found.";
            result.Data = cb;
            result.Total = 1;
            return await Task.FromResult(result);
        }
        #endregion

        #region CompanyServices
        public async Task<GetResults> AddEditCompanyService(CompanyServiceRequestModel csModel)
        {
            GetResults result = new GetResults();
            var csObj = _dbContext.CompanyService.Where(w => w.Id == csModel.Id && w.IsDeleted == true).FirstOrDefaultAsync().Result;
            if (csObj == null)
            {
                CompanyService cs = new CompanyService()
                {  
                    NameEng = csModel.NameEng,
                    NameArb = csModel.NameArb,
                    CompanyId = csModel.CompanyId,
                    ShortDescriptionEng = csModel.ShortDescriptionEng,
                    ShortDescriptionArb = csModel.ShortDescriptionArb,
                    DescriptionEng = csModel.DescriptionEng,
                    DescriptionArb = csModel.DescriptionArb,
                    Image = csModel.Image,
                    OldPrice = csModel.OldPrice,
                    Price = csModel.Price,
                    SortOrder = csModel.SortOrder,
                    IsPublished = csModel.IsPublished,
                    HasOffers = csModel.HasOffers,
                    OffersDescriptionEng = csModel.OffersDescriptionEng,
                    OffersDescriptionArb = csModel.OffersDescriptionArb,
                    CreationTime = DateTime.Now,
                    CreatorUserId = csModel.CreatorUserId,
                    OfferStartDate = csModel.OfferStartDate,
                    OfferEndDate = csModel.OfferEndDate,
                    OfferShortDescriptionEng = csModel.OfferShortDescriptionEng,
                    OfferShortDescriptionArb = csModel.OfferShortDescriptionArb

                };
                _dbContext.CompanyService.Add(cs);
                result.Message = "Company Service Added.";
            }
            else
            {
                csObj.NameEng = csModel.NameEng;
                csObj.NameArb = csModel.NameArb;
                csObj.CompanyId = csModel.CompanyId;
                csObj.ShortDescriptionEng = csModel.ShortDescriptionEng;
                csObj.ShortDescriptionArb = csModel.ShortDescriptionArb;
                csObj.DescriptionEng = csModel.DescriptionEng;
                csObj.DescriptionArb = csModel.DescriptionArb;
                csObj.Image = csModel.Image;
                csObj.OldPrice = csModel.OldPrice;
                csObj.Price = csModel.Price;
                csObj.SortOrder = csModel.SortOrder;
                csObj.IsPublished = csModel.IsPublished;
                csObj.HasOffers = csModel.HasOffers;
                csObj.OffersDescriptionEng = csModel.OffersDescriptionEng;
                csObj.OffersDescriptionArb = csModel.OffersDescriptionArb;
                csObj.LastModificationTime = csModel.LastModificationTime;
                csObj.LastModifierUserId = csModel.LastModifierUserId;
                csObj.OfferStartDate = csModel.OfferStartDate;
                csObj.OfferEndDate = csModel.OfferEndDate;
                csObj.OfferShortDescriptionEng = csModel.OfferShortDescriptionEng;
                csObj.OfferShortDescriptionArb = csModel.OfferShortDescriptionArb;

                result.Message = "Company Serivce Updated.";
            }
            _dbContext.SaveChanges();
            result.IsSuccess = true;
            return await Task.FromResult(result);

        }
        public async Task<GetResults> DeleteCompanyService(int Id)
        {
            GetResults result = new GetResults();
            var csObj = _dbContext.CompanyService.Where(w => w.Id == Id && w.IsDeleted == true).FirstOrDefaultAsync().Result;
            if (csObj != null)
            {
                csObj.IsDeleted = true;
                csObj.DeletionTime = DateTime.Now;
                result.Message = "Company Service Deleted.";
            }
            _dbContext.SaveChanges();
            result.IsSuccess = true;
            return await Task.FromResult(result);

        }
        public async Task<GetResults> GetAllCompanyService()
        {
            GetResults result = new GetResults();
            var cpList = _dbContext.CompanyService.Join(_dbContext.Company, cs => cs.CompanyId, cmny => cmny.Id, (cs, cmny) => new { cs, cmny }).
                            Where(w => w.cs.IsDeleted == false).
                            Select(s => new
                            {
                                Id = s.cs.Id,
                                Company = s.cmny.NameEng,
                                ServiceNameEng = s.cs.NameEng,
                                ServiceNameArb = s.cs.NameArb,
                            }).ToListAsync().Result;
            result.IsSuccess = true;
            result.Message = "Company Service List.";
            result.Data = cpList;
            result.Total = cpList.Count;
            return await Task.FromResult(result);
        }
        public async Task<GetResults> GetCompanyServiceById(int id)
        {
            GetResults result = new GetResults();
            var cb = _dbContext.CompanyService.Where(w => w.Id == id && w.IsDeleted == false)
                                                   .Select(s => new CompanyServiceViewModel
                                                   {
                                                       Id = s.Id,
                                                       NameEng = s.NameEng,
                                                       NameArb = s.NameArb,
                                                       CompanyId = s.CompanyId,
                                                       ShortDescriptionEng = s.ShortDescriptionEng,
                                                       ShortDescriptionArb = s.ShortDescriptionArb,
                                                       DescriptionEng = s.DescriptionEng,
                                                       DescriptionArb = s.DescriptionArb,
                                                       Image = s.Image,
                                                       OldPrice = s.OldPrice,
                                                       Price = s.Price,
                                                       SortOrder = s.SortOrder,
                                                       IsPublished = s.IsPublished,
                                                       HasOffers = s.HasOffers,
                                                       OffersDescriptionEng = s.OffersDescriptionEng,
                                                       OffersDescriptionArb = s.OffersDescriptionArb,
                                                       IsDeleted = s.IsDeleted,
                                                       DeleterUserId = s.DeleterUserId,
                                                       DeletionTime = s.DeletionTime,
                                                       LastModificationTime = s.LastModificationTime,
                                                       LastModifierUserId = s.LastModifierUserId,
                                                       CreationTime = s.CreationTime,
                                                       CreatorUserId = s.CreatorUserId,
                                                       OfferStartDate = s.OfferStartDate,
                                                       OfferEndDate = s.OfferEndDate,
                                                       OfferShortDescriptionEng = s.OfferShortDescriptionEng,
                                                       OfferShortDescriptionArb = s.OfferShortDescriptionArb
                                                   }).FirstOrDefaultAsync().Result;
            result.IsSuccess = true;
            result.Message = "Company Service Found.";
            result.Data = cb;
            result.Total = 1;
            return await Task.FromResult(result);
        }
        #endregion

        #region CompanyBanner
        public async Task<GetResults> AddEditCompanyBanner(CompanyBannerRequestModel cbModel)
        {
            GetResults result = new GetResults();
            var csObj = _dbContext.CompanyBanners.Where(w => w.Id == cbModel.Id && w.IsDeleted == true).FirstOrDefaultAsync().Result;
            if (csObj == null)
            {
                CompanyBanners cs = new CompanyBanners()
                {
                    CompanyId = cbModel.CompanyId,
                    BannerNameEng = cbModel.BannerNameEng,
                    BannerNameArb = cbModel.BannerNameArb,
                    EnglishUrl = cbModel.EnglishUrl,
                    ArabicUrl = cbModel.ArabicUrl,
                    ImageEng = cbModel.ImageEng,
                    ImageArb = cbModel.ImageArb,
                    Target = cbModel.Target,
                    SortOrder = cbModel.SortOrder,
                    CreationTime =DateTime.Now,
                    CreatorUserId = cbModel.CreatorUserId,
                    BannerExpiryDate = cbModel.BannerExpiryDate,
                    IsPublished = cbModel.IsPublished,
                    BannerStartDate = cbModel.BannerStartDate

                };
                _dbContext.CompanyBanners.Add(cs);
                result.Message = "Company Banner Added.";
            }
            else
            {
                csObj.CompanyId = cbModel.CompanyId;
                csObj.BannerNameEng = cbModel.BannerNameEng;
                csObj.BannerNameArb = cbModel.BannerNameArb;
                csObj.EnglishUrl = cbModel.EnglishUrl;
                csObj.ArabicUrl = cbModel.ArabicUrl;
                csObj.ImageEng = cbModel.ImageEng;
                csObj.ImageArb = cbModel.ImageArb;
                csObj.Target = cbModel.Target;
                csObj.SortOrder = cbModel.SortOrder;
                csObj.LastModificationTime = DateTime.Now;
                csObj.LastModifierUserId = cbModel.LastModifierUserId;
                csObj.BannerExpiryDate = cbModel.BannerExpiryDate;
                csObj.IsPublished = cbModel.IsPublished;
                csObj.BannerStartDate = cbModel.BannerStartDate;


                result.Message = "Company Banner Updated.";
            }
            _dbContext.SaveChanges();
            result.IsSuccess = true;
            return await Task.FromResult(result);

        }
        public async Task<GetResults> DeleteCompanyBanner(int Id)
        {
            GetResults result = new GetResults();
            var csObj = _dbContext.CompanyBanners.Where(w => w.Id == Id && w.IsDeleted == true).FirstOrDefaultAsync().Result;
            if (csObj != null)
            {
                csObj.IsDeleted = true;
                csObj.DeletionTime = DateTime.Now;
                result.Message = "Company Banner Deleted.";
            }
            _dbContext.SaveChanges();
            result.IsSuccess = true;
            return await Task.FromResult(result);

        }
        public async Task<GetResults> GetAllCompanyBanner()
        {
            GetResults result = new GetResults();
            var cpList = _dbContext.CompanyBanners.Join(_dbContext.Company, cb => cb.CompanyId, cmny => cmny.Id, (cb, cmny) => new { cb, cmny }).
                            Where(w => w.cb.IsDeleted == false).
                            Select(s => new
                            {
                                Id = s.cb.Id,
                                Company = s.cmny.NameEng,
                                BannerNameEng = s.cb.BannerNameEng,
                                BannerNameArb = s.cb.BannerNameArb,
                            }).ToListAsync().Result;
            result.IsSuccess = true;
            result.Message = "Company Banner List.";
            result.Data = cpList;
            result.Total = cpList.Count;
            return await Task.FromResult(result);
        }
        public async Task<GetResults> GetCompanyBannerById(int id)
        {
            GetResults result = new GetResults();
            var cb = _dbContext.CompanyBanners.Where(w => w.Id == id && w.IsDeleted == false)
                                                   .Select(s => new CompanyBannerViewModel
                                                   {
                                                       Id = s.Id,
                                                       CompanyId = s.CompanyId,
                                                       BannerNameEng = s.BannerNameEng,
                                                       BannerNameArb = s.BannerNameArb,
                                                       EnglishUrl = s.EnglishUrl,
                                                       ArabicUrl = s.ArabicUrl,
                                                       ImageEng = s.ImageEng,
                                                       ImageArb = s.ImageArb,
                                                       Target = s.Target,
                                                       SortOrder = s.SortOrder,
                                                       IsDeleted = s.IsDeleted,
                                                       DeleterUserId = s.DeleterUserId,
                                                       DeletionTime = s.DeletionTime,
                                                       LastModificationTime = s.LastModificationTime,
                                                       LastModifierUserId = s.LastModifierUserId,
                                                       CreationTime = s.CreationTime,
                                                       CreatorUserId = s.CreatorUserId,
                                                       BannerExpiryDate = s.BannerExpiryDate,
                                                       IsPublished = s.IsPublished,
                                                       BannerStartDate = s.BannerStartDate,
                                                   }).FirstOrDefaultAsync().Result;
            result.IsSuccess = true;
            result.Message = "Company Banner Found.";
            result.Data = cb;
            result.Total = 1;
            return await Task.FromResult(result);
        }
        #endregion


        #region Brand

        public async Task<GetResults> GetAllBrands(int page, int limit, string searchValue)
        {
            var brandlist = _dbContext.Brand.Where(w => (string.IsNullOrEmpty(searchValue) ? w.NameEng.ToLower().Contains(searchValue.ToLower()) : w.NameEng == w.NameEng ||
            !string.IsNullOrEmpty(searchValue) ? w.NameArb.ToLower().Contains(searchValue.ToLower()) : w.NameArb == w.NameArb) && w.IsDeleted == false
            ).Select(s => new BrandModel
            {
                Id = s.Id,
                NameEng = s.NameEng,
                NameArb = s.NameArb,
                Logo = s.Logo
            }).Distinct().OrderByDescending(o => o.Id).Skip(limit * page).Take(limit).ToListAsync().Result;
            int total = _dbContext.Brand.Where(w => (string.IsNullOrEmpty(searchValue) ? w.NameEng.ToLower().Contains(searchValue.ToLower()) : w.NameEng == w.NameEng ||
                                !string.IsNullOrEmpty(searchValue) ? w.NameArb.ToLower().Contains(searchValue.ToLower()) : w.NameArb == w.NameArb) && w.IsDeleted == false
                                ).CountAsync().Result;

            GetResults result = new GetResults()
            {
                Total = total,
                IsSuccess = true,
                Data = brandlist,
                Message = "Brand list found"
            };

            return await Task.FromResult(result);

        }

        public async Task<GetResults> GetBrandById(int brandid)
        {
            BrandModel brand = _dbContext.Brand.Where(w => w.Id == brandid && w.IsDeleted == false).Select(s => new BrandModel
            {
                Id = s.Id,
                NameEng = s.NameEng,
                NameArb = s.NameArb,
                SortOrder = s.SortOrder,
                Logo = s.Logo,
                IsDeleted = s.IsDeleted,
                DeleterUserId = s.DeleterUserId,
                DeletionTime = s.DeletionTime,
                LastModificationTime = s.LastModificationTime,
                LastModifierUserId = s.LastModifierUserId,
                CreationTime = s.CreationTime,
                CreatorUserId = s.CreatorUserId,
                IsPublished = s.IsPublished,
                Slug = s.Slug,
                SeoEnabled = s.SeoEnabled,
                MetaTitleEng = s.MetaTitleEng,
                KeywordsEng = s.KeywordsEng,
                MetaDescriptionEng = s.MetaDescriptionEng,
                PageContentEng = s.PageContentEng,
                MetaTitleArb = s.MetaTitleArb,
                KeywordsArb = s.KeywordsArb,
                MetaDescriptionArb = s.MetaDescriptionArb,
                PageContentArb = s.PageContentArb,

            }).FirstOrDefaultAsync().Result;

            GetResults result = new GetResults()
            {
                IsSuccess = true,
                Message = "Brand found",
                Data = brand,
                Total = 1
            };
            return await Task.FromResult(result);
        }

        public async Task<GetResults> AddUpdateBrand(BrandRequestModel breqmodel)
        {
            GetResults result = new GetResults();
            var brandobj = _dbContext.Brand.Where(w => w.Id == breqmodel.Id && w.IsDeleted == false).FirstOrDefaultAsync().Result;
            if (brandobj == null)
            {
                Brand bobj = new Brand()
                { 
                    NameEng = breqmodel.NameEng,
                    NameArb = breqmodel.NameArb,
                    SortOrder = breqmodel.SortOrder,
                    Logo = breqmodel.Logo,
                    IsDeleted = breqmodel.IsDeleted,
                    DeleterUserId = breqmodel.DeleterUserId,
                    DeletionTime = breqmodel.DeletionTime,
                    CreationTime = DateTime.Now,
                    CreatorUserId = breqmodel.CreatorUserId,
                    IsPublished = breqmodel.IsPublished,
                    Slug = breqmodel.Slug,
                    SeoEnabled = breqmodel.SeoEnabled,
                    MetaTitleEng = breqmodel.MetaTitleEng,
                    KeywordsEng = breqmodel.KeywordsEng,
                    MetaDescriptionEng = breqmodel.MetaDescriptionEng,
                    PageContentEng = breqmodel.PageContentEng,
                    MetaTitleArb = breqmodel.MetaTitleArb,
                    KeywordsArb = breqmodel.KeywordsArb,
                    MetaDescriptionArb = breqmodel.MetaDescriptionArb,
                    PageContentArb = breqmodel.PageContentArb
                };
                _dbContext.Brand.Add(bobj);
                result.Message = "Brand added";
            }
            else
            { 
                brandobj.NameEng = breqmodel.NameEng;
                brandobj.NameArb = breqmodel.NameArb;
                brandobj.SortOrder = breqmodel.SortOrder;
                brandobj.Logo = breqmodel.Logo;
                brandobj.IsDeleted = breqmodel.IsDeleted;
                brandobj.DeleterUserId = breqmodel.DeleterUserId;
                brandobj.DeletionTime = breqmodel.DeletionTime;
                brandobj.LastModificationTime = breqmodel.LastModificationTime;
                brandobj.LastModifierUserId = breqmodel.LastModifierUserId;
                brandobj.IsPublished = breqmodel.IsPublished;
                brandobj.Slug = breqmodel.Slug;
                brandobj.SeoEnabled = breqmodel.SeoEnabled;
                brandobj.MetaTitleEng = breqmodel.MetaTitleEng;
                brandobj.KeywordsEng = breqmodel.KeywordsEng;
                brandobj.MetaDescriptionEng = breqmodel.MetaDescriptionEng;
                brandobj.PageContentEng = breqmodel.PageContentEng;
                brandobj.MetaTitleArb = breqmodel.MetaTitleArb;
                brandobj.KeywordsArb = breqmodel.KeywordsArb;
                brandobj.MetaDescriptionArb = breqmodel.MetaDescriptionArb;
                brandobj.PageContentArb = breqmodel.PageContentArb;

                result.Message = "Brand updated";

            }
            await _dbContext.SaveChangesAsync();
            result.IsSuccess = true;
            return await Task.FromResult(result);

        }

        public async Task<GetResults> DeleteBrand(int brandid)
        {
            GetResults result = new GetResults();
            Brand brand = _dbContext.Brand.Where(w => w.Id == brandid && w.IsDeleted == false).FirstOrDefaultAsync().Result;

            if(brand!=null)
            {
                brand.IsDeleted = true;
                brand.DeletionTime = DateTime.Now;
                result.Message = "Brand deleted";
            }
            else
            {
                result.Message = "No brand found";
            }

            await _dbContext.SaveChangesAsync();
            result.IsSuccess = true;
            return await Task.FromResult(result);
        }



        #endregion


    }
}
