using ApplicationService.IServices;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using CommonService.RequestModel;
using CommonService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TTBusinessAdminPanel.Models;

namespace TTBusinessAdminPanel.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private Logger _logger;
        private IMaster _master;
        private ILocation _location;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyfService;

        public HomeController(IMaster master, ILocation location, IMapper mapper, INotyfService notyfService)
        {
            _logger = LogManager.GetLogger("Home");
            _master = master;
            _location = location;
            _mapper = mapper;
            _notyfService = notyfService;
        }

        //Dashboard
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        #region Roles
        public IActionResult Role()
        {
            return View();
        }

        public IActionResult GetRoles()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int pageNo = (skip / pageSize);
                int recordsTotal = 0;
                var allData = _master.GetRoles(pageNo, pageSize, searchValue).Result;
                var cData = (List<RoleModel>)allData.Data;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    cData = cData.OrderBy(o => sortColumn + " " + sortColumnDirection).ToList();
                }
                recordsTotal = allData.Total;
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = cData };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return Ok(null);
        }
        #endregion

        #region Countries
        public IActionResult Country()
        {
            return View();
        }
        public IActionResult GetCountries()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int pageNo = (skip / pageSize);
                int recordsTotal = 0;
                var allData = _location.GetCountries(pageNo, pageSize, searchValue).Result;
                var cData = (List<CountryModel>)allData.Data;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    cData = cData.OrderBy(o => sortColumn + " " + sortColumnDirection).ToList();
                }
                recordsTotal = allData.Total;
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = cData };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return Ok(null);
        }
        #endregion

        #region Region
        public IActionResult Region()
        {
            return View();
        }
        public IActionResult GetRegions()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int pageNo = (skip / pageSize);
                int recordsTotal = 0;
                var allData = _location.GetRegions(pageNo, pageSize, searchValue).Result;
                var cData = (List<RegionModel>)allData.Data;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    cData = cData.OrderBy(o => sortColumn + " " + sortColumnDirection).ToList();
                }
                recordsTotal = allData.Total;
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = cData };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return Ok(null);
        }

        private void BindCountry()
        {
            try
            {
                List<CountryModel> countries = new List<CountryModel>();
                countries = _location.GetMasterCountries().Result;
                ViewBag.countries = new SelectList(countries, "Id", "CountryNameEng");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
        public IActionResult RegionAdd()
        {
            try
            {
                BindCountry();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message.ToString());
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult RegionAdd(RegionRequestModel region)
        {
            GetResults result = new GetResults();
            try
            {
                if (ModelState.IsValid)
                {
                    result = _location.AddUpdateRegion(region).Result;
                    if(result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Region", "Home");
                    }
                    else
                    {
                        _notyfService.Warning(result.Message);
                    }
                }
                else
                {
                    _notyfService.Error("Validation Error !!!");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message.ToString());
            }

            BindCountry();
            return View(region);
        }

        public IActionResult RegionEdit(int id)
        {
            RegionRequestModel regionRequestModel = new RegionRequestModel();
            try
            {
                var region = _location.GetRegionById(id).Result.Data;
                if(region != null)
                {
                   regionRequestModel = _mapper.Map<RegionRequestModel>(region);
                }
                else
                {
                    _notyfService.Error("Region is not found !!!");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message.ToString());
            }

            BindCountry();
            return View(regionRequestModel);
        }

        [HttpPost]
        public IActionResult RegionEdit(RegionRequestModel region)
        {
            GetResults result = new GetResults();
            try
            {
                if (ModelState.IsValid)
                {
                    result = _location.AddUpdateRegion(region).Result;
                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Region", "Home");
                    }
                    else
                    {
                        _notyfService.Warning(result.Message);
                    }
                }
                else
                {
                    _notyfService.Error("Validation Error !!!");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message.ToString());
            }

            BindCountry();
            return View(region);
        }

        public IActionResult DeleteRegion(int id)
        {
            GetResults cmodel = new GetResults();
            try
            {
                cmodel = _location.DeleteRegion(id).Result;

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return Json(cmodel);
        }
        #endregion

        #region Category
        public IActionResult Category()
        {
            return View("Category");
        }
        public IActionResult GetAllCategories()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int pageNo = (skip / pageSize);
                int recordsTotal = 0;
                var allData = _master.GetAllCategories(pageNo, pageSize, searchValue).Result;
                var cData = (List<CategoriesViewModel>)allData.Data;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    cData = cData.OrderBy(o => sortColumn + " " + sortColumnDirection).ToList();
                }
                recordsTotal = allData.Total;
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = cData };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return Ok(null);
        }
        public IActionResult CategoryAdd()
        {
            return View("CategoryAdd");
        }

        [HttpPost]
        public IActionResult AddEditCategory(CategoriesRequestModel rceqmodel)
        {
            GetResults result = new GetResults();
            try
            {
                if (ModelState.IsValid)
                {
                    result = _master.CreateUpdateCategory(rceqmodel).Result;
                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Category", "Home");
                    }
                    else
                    {
                        _notyfService.Warning(result.Message);
                    }
                }
                else
                {
                    _notyfService.Error("Validation Error !!!");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message.ToString());
            }
            return View(rceqmodel);
        }
        public IActionResult EditCategory(int id)
        {
            CategoriesRequestModel cmodel = new CategoriesRequestModel();
            try
            {
                var company = _master.GetCategoryById(id).Result;
                var s = (CategoriesViewModel)company.Data;
                if (s != null)
                {
                    cmodel = new CategoriesRequestModel()
                    {
                        Id = s.Id,
                        NameEng = s.NameEng,
                        NameArb = s.NameArb,
                        Unspsccode = s.Unspsccode,
                        IsPublished = s.IsPublished,
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
                        IsFeatured = s.IsFeatured,
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message.ToString());
            }
            return View(cmodel);
        }
        public IActionResult DeleteCategory(int id)
        {
            GetResults cmodel = new GetResults();
            try
            {
                cmodel = _master.DeleteCategory(id).Result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return Json(cmodel);
        }
        #endregion

        #region Brand
        public IActionResult Brand()
        {
            return View();
        }
        public IActionResult GetAllBrand()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int pageNo = (skip / pageSize);
                int recordsTotal = 0;
                var allData = _master.GetAllBrands(pageNo, pageSize, searchValue).Result;
                var cData = (List<BrandModel>)allData.Data;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    cData = cData.OrderBy(o => sortColumn + " " + sortColumnDirection).ToList();
                }
                recordsTotal = allData.Total;
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = cData };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return Ok(null);
        }
        public IActionResult BrandAdd()
        {
            return View();
        }
        public IActionResult AddEditBrand(BrandRequestModel breqmodel)
        {
            GetResults result = new GetResults();
            try
            {
                if (ModelState.IsValid)
                {
                    result = _master.AddUpdateBrand(breqmodel).Result;
                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Brand", "Home");
                    }
                    else
                    {
                        _notyfService.Warning(result.Message);
                    }
                }
                else
                {
                    _notyfService.Error("Validation Error !!!");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message.ToString());
            }
            return View(breqmodel);
        }
        public IActionResult EditBrand(int id)
        {
            BrandRequestModel bmodel = new BrandRequestModel();
            try
            {
                var brand = _master.GetBrandById(id).Result;
                var bobj = (BrandModel)brand.Data;
                if (bobj != null)
                {
                    bmodel = new BrandRequestModel()
                    {
                        Id = bobj.Id,
                        NameEng = bobj.NameEng,
                        NameArb = bobj.NameArb,
                        SortOrder = bobj.SortOrder,
                        Logo = bobj.Logo,
                        IsDeleted = bobj.IsDeleted,
                        DeleterUserId = bobj.DeleterUserId,
                        DeletionTime = bobj.DeletionTime,
                        LastModificationTime = bobj.LastModificationTime,
                        LastModifierUserId = bobj.LastModifierUserId,
                        CreationTime = bobj.CreationTime,
                        CreatorUserId = bobj.CreatorUserId,
                        IsPublished = bobj.IsPublished,
                        Slug = bobj.Slug,
                        SeoEnabled = bobj.SeoEnabled,
                        MetaTitleEng = bobj.MetaTitleEng,
                        KeywordsEng = bobj.KeywordsEng,
                        MetaDescriptionEng = bobj.MetaDescriptionEng,
                        PageContentEng = bobj.PageContentEng,
                        MetaTitleArb = bobj.MetaTitleArb,
                        KeywordsArb = bobj.KeywordsArb,
                        MetaDescriptionArb = bobj.MetaDescriptionArb,
                        PageContentArb = bobj.PageContentArb
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message.ToString());
            }
            return View(bmodel);
        }
        public IActionResult DeleteBrand(int id)
        {
            GetResults bmodel = new GetResults();
            try
            {
                bmodel = _master.DeleteBrand(id).Result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return Json(bmodel);
        }
        #endregion

        #region Design Demo
        public IActionResult Form()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
