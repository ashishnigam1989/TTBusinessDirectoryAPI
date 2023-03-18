using ApplicationService.IServices;
using AutoMapper;
using CommonService.RequestModel;
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
