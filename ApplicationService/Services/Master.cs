using ApplicationService.IServices;
using AutoMapper;
using CommonService.Constants;
using CommonService.RequestModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using Microsoft.EntityFrameworkCore;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services
{
    public class Master : IMaster
    {
        private BusinessDirectoryDBContext _dbContext;
        private readonly IMapper _mapper;

        public Master(BusinessDirectoryDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SearchPageDBModel, SearchViewModel>();
            });
            _mapper = new Mapper(config);
        }

        public async Task<GetResults> GetRoles(int page, int limit, string searchValue)
        {
            int total = 0;
            List<RoleModel> rolelist = _dbContext.Roles.Where(w =>
           (!string.IsNullOrEmpty(searchValue) ? w.Name.ToLower().Contains(searchValue.ToLower()) : w.Name == w.Name) && w.IsDeleted == false
            ).Select(s => new RoleModel
            {
                Name = s.Name,
                DisplayName = s.DisplayName,
                Id = s.Id
            }).Distinct().OrderByDescending(o => o.Id).Skip(limit * page).Take(limit).ToListAsync().Result;

            total = _dbContext.Roles.Where(w => w.IsDeleted == false).Where(w =>
                                    !string.IsNullOrEmpty(searchValue) ? w.Name.ToLower().Contains(searchValue.ToLower()) : w.Name == w.Name
                                    ).CountAsync().Result;

            GetResults uobj = new GetResults
            {
                Total = total,
                Data = rolelist
            };
            return await Task.FromResult(uobj);

        }

        public async Task<List<RoleModel>> GetMasterRoles()
        {
            var roles = _dbContext.Roles
                .Where(w=>w.IsDeleted == false)
                .Select(s => new RoleModel
                {
                    Name = s.Name,
                    Id = s.Id,
                    DisplayName = s.DisplayName
                }).ToList();
            return await Task.FromResult(roles);
        }

        public async Task<List<MenuModel>> GetAllMenus()
        {
            var menus = _dbContext.Menus.Select(s=>new MenuModel { 
                MenuId= s.MenuId,
                MenuIcon= s.MenuIcon,
                MenuName=s.MenuName,
                MenuPath=s.MenuPath,
                ComponentName=s.ComponentName,
                Sequence=s.Sequence,
                IsActivated=s.IsActivated
            }).ToList();

            return await Task.FromResult(menus);

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
                    IsFeatured = s.IsFeatured.HasValue ? s.IsFeatured.Value : false,
                    Icon = s.Icon

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
                var validCategory = _dbContext.Category.Where(w => w.NameEng == crModel.NameEng && w.IsDeleted == false).FirstOrDefaultAsync().Result;
                if(validCategory == null)
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
                        IsFeatured = crModel.IsFeatured,
                        Icon = crModel.Icon
                    };
                    _dbContext.Category.Add(crobj);
                    await _dbContext.SaveChangesAsync();
                    resp.Data = crobj.Id;
                    crobj.Icon = string.Format(crModel.Icon, crobj.Id);
                    await _dbContext.SaveChangesAsync();
                    resp.IsSuccess = true;
                    resp.Message = "Category Added Successfully.";
                }
                else
                {
                    resp.Message = crModel.NameEng + " is already exists !!!";
                    resp.IsSuccess = false;
                }
            }
            else
            {
                var validCategory = _dbContext.Category.Where(w => w.NameEng == crModel.NameEng && w.Id != crModel.Id && w.IsDeleted == false).FirstOrDefaultAsync().Result;
                if (validCategory == null)
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
                    cdata.Icon = crModel.Icon;
                    await _dbContext.SaveChangesAsync();
                    resp.Data = cdata.Id;
                    cdata.Icon = string.Format(crModel.Icon, cdata.Id);
                    await _dbContext.SaveChangesAsync();
                    resp.IsSuccess = true;
                    resp.Message = "Category Updated Successfully.";
                }
                else
                {
                    resp.Message = crModel.NameEng + " is already exists !!!";
                    resp.IsSuccess = false;
                }
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
                   IsFeatured = s.IsFeatured.HasValue ? s.IsFeatured.Value : false,
                   Icon=s.Icon

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

        public async Task<GetResults> GetMasterCategories()
        {
            List<CategoriesViewModel> listcategory = _dbContext.Category.Where(w => w.IsDeleted == false)
                .Select(s => new CategoriesViewModel
                {
                    Id = s.Id,
                    NameEng = s.NameEng
                }).Distinct().OrderByDescending(o => o.Id).ToListAsync().Result;


            GetResults result = new GetResults
            {
                Data = listcategory,
                Total = listcategory.Count
            };
            return await Task.FromResult(result);

        }

        /// <summary>
        /// Category for UI app
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public async Task<GetResults> GetFeaturedCategories(bool isFeatured = true, int count = 0)
        {
            int total = 0;
            var categoriesQuery = _dbContext.Category.Where(w => !w.IsDeleted && w.IsPublished.Value);
            if (isFeatured)
            {
                categoriesQuery = categoriesQuery.Where(w => w.IsFeatured.Equals(isFeatured));
            }
            if (count > default(int))
            {
                categoriesQuery = categoriesQuery.Take(count);
            }
            var categories = await categoriesQuery.Select(s => new CategoriesViewModel
            {
                Id = s.Id,
                NameEng = s.NameEng,
                NameArb = s.NameArb,
                IsFeatured = isFeatured,
                Icon = s.Icon,
            })
            .Distinct()
            .OrderBy(o => o.NameEng)
            .ToListAsync();

            GetResults uobj = new GetResults
            {
                Total = total,
                Data = categories
            };
            return await Task.FromResult(uobj);

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
                var validBrand = _dbContext.Brand.Where(w => w.NameEng == breqmodel.NameEng && w.IsDeleted == false).FirstOrDefaultAsync().Result;
                if(validBrand == null)
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
                    await _dbContext.SaveChangesAsync();
                    result.Data = bobj.Id;
                    bobj.Logo = string.Format(breqmodel.Logo, bobj.Id);
                    await _dbContext.SaveChangesAsync();
                    result.IsSuccess = true;
                    result.Message = "Brand added successfully";
                }
                else
                {
                    result.Message = breqmodel.NameEng + " is already exists !!!";
                    result.IsSuccess = false;
                }
            }
            else
            {
                var validBrand = _dbContext.Brand.Where(w => w.NameEng == breqmodel.NameEng && w.Id != breqmodel.Id && w.IsDeleted == false).FirstOrDefaultAsync().Result;
                if (validBrand == null)
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
                    await _dbContext.SaveChangesAsync();
                    result.Data = brandobj.Id;
                    brandobj.Logo = string.Format(breqmodel.Logo, brandobj.Id);
                    await _dbContext.SaveChangesAsync();
                    result.IsSuccess = true;
                    result.Message = "Brand updated successfully";
                }
                else
                {
                    result.Message = breqmodel.NameEng + " is already exists !!!";
                    result.IsSuccess = false;
                }
            }
            
            return await Task.FromResult(result);
        }
        public async Task<GetResults> DeleteBrand(int brandid)
        {
            GetResults result = new GetResults();
            Brand brand = _dbContext.Brand.Where(w => w.Id == brandid && w.IsDeleted == false).FirstOrDefaultAsync().Result;

            if (brand != null)
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

        public async Task<GetResults> GetMasterBrand()
        {
            List<BrandModel> listcategory = _dbContext.Brand.Where(w => w.IsDeleted == false)
                .Select(s => new BrandModel
                {
                    Id = s.Id,
                    NameEng = s.NameEng,
                }).Distinct().OrderByDescending(o => o.Id).ToListAsync().Result;


            GetResults result = new GetResults
            {
                Data = listcategory,
                Total = listcategory.Count
            };
            return await Task.FromResult(result);

        }

        public async Task<List<long>> GetAllAssignedBrandCategory(int brandid)
        {
            var brandCat = _dbContext.BrandCategory.Where(w => w.BrandId== brandid).Select(s =>s.CategoryId).ToList();
            return await Task.FromResult(brandCat);
        }

        public async Task<bool> AddUpdateBrandCategory(BrandCategoryModel _rModel)
        {
            var brdcat = _dbContext.BrandCategory.Where(w => w.BrandId == _rModel.BrandId).ToList();
            if (brdcat.Count > 0)
            {
                _dbContext.BrandCategory.RemoveRange(brdcat);
                await _dbContext.SaveChangesAsync();
            }
            List<BrandCategory> rmlist = new List<BrandCategory>();
            foreach (var catid in _rModel.Categories)
            {
                BrandCategory rmModel = new BrandCategory
                {
                    BrandId = _rModel.BrandId,
                    CategoryId =catid,
                    CreationTime=DateTime.Now,
                    CreatorUserId=_rModel.CreatedBy
                    
                };
                rmlist.Add(rmModel);
            }
            _dbContext.BrandCategory.AddRange(rmlist);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(true);
        }
        #endregion
        public async Task<GetResults> GetSearchResults(string searchTerm, int countryId)
        {
            var queryResult = await _dbContext.SearchModel.FromSqlRaw("EXEC [usp_GetSearchResult] {0}, {1}", searchTerm, countryId).ToListAsync();

            GetResults result = new GetResults
            {
                Data = queryResult,
                Total = queryResult.Count
            };
            return await Task.FromResult(result);

        }

        public async Task<GetResults> GetSearchPageResult(SearchRequestModel searchRequest)
        {
            string type = string.Empty;
            if (searchRequest.Types.Count == 1 && searchRequest.Types.First() != "all")
                type = searchRequest.Types.First();
            var query = _dbContext.SearchPageModel.FromSqlRaw("EXEC [usp_GetSearchPageResult] {0}, {1}, {2}, {3}, {4}, {5}", 
                searchRequest.SearchTerm, 
                searchRequest.CountryId,
                searchRequest.RegionId,
                type,
                searchRequest.Sorting,
                searchRequest.Verified).AsEnumerable();

            if(searchRequest.Types.Count > 1) {
                query = query.Where(q => searchRequest.Types.Contains(q.Type));
            }
            //var queryResult = query.ToList();

            var searchResults = _mapper.Map<List<SearchViewModel>>(query
                .Skip(searchRequest.Skip).Take(searchRequest.Take).ToList());

            GetResults result = new GetResults
            {
                Data = new SearchResponseModel { SearchResults = searchResults, Total = query.Count() },
                Total = searchResults.Count
            };
            return await Task.FromResult(result);
        }

        public async Task<GetResults> AddUpdateDistrict(DistrictRequestModel dreqmodel)
        {
            GetResults result = new GetResults();
            var district = _dbContext.Districts.Where(w => w.IsDeleted == false && w.DistrictId == dreqmodel.DistrictId).FirstOrDefault();
            if(district==null)
            {
                Districts ds = new Districts()
                {
                    DistrictName = dreqmodel.District,
                    RegionId = dreqmodel.RegionId,
                    CreationTime = DateTime.Now,
                    CreatorUserId = dreqmodel.CreatedBy
                };
                _dbContext.Districts.Add(ds);
                result.IsSuccess = true;
                result.Message = "District added successfully";
            }
            else
            {
                district.RegionId = dreqmodel.RegionId;
                district.DistrictName = dreqmodel.District;
                result.IsSuccess = true;
                result.Message = "District updated successfully";
            }
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(result);
        }

        public async Task<GetResults> GetAllDistricts(int regionId)
        {
            GetResults result = new GetResults();
            var alldistrict = _dbContext.Districts.Join(_dbContext.Region, d => d.RegionId, r => r.Id, (d, r) => new { d,r})
                    .Where(w => w.d.IsDeleted == false && w.d.RegionId==regionId).Select(s => new DistrictModel {
                DistrictName=s.d.DistrictName,
                RegionName=s.r.NameEng,
                RegionId=s.d.RegionId,
                Id=s.d.DistrictId
            }).ToList();
            result = new GetResults { Data = alldistrict, Message = "Districts found", IsSuccess = true, Total = alldistrict.Count() };
            return await Task.FromResult(result);
        }
   
    }
}
