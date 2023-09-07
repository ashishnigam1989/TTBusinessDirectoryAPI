using ApplicationService.IServices;
using ApplicationService.Services;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using CommonService.Enums;
using CommonService.Helpers;
using CommonService.RequestModel;
using CommonService.ViewModels;
using CommonService.ViewModels.Company;
using DatabaseService.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using TTBusinessAdminPanel.Extensions;

namespace TTBusinessAdminPanel.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private Logger _logger;
        private ICompanies _company;
        private IMaster _master;
        private ILocation _location;
        private readonly INotyfService _notyfService;

        public CompanyController(ICompanies company, IMaster master, ILocation location, INotyfService notyfService)
        {
            _logger = LogManager.GetLogger("Company");
            _company = company;
            _master = master;
            _location = location;
            _notyfService = notyfService;
        }

        //View/Add/Edit
        #region Company
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllCompany()
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
                var allData = _company.GetAllCompanies(pageNo, pageSize, searchValue).Result;
                var cData = (List<CompanyModel>)allData.Data;
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
        public IActionResult Add()
        {
            BindCountries();
            return View();
        }
        public IActionResult AddEditCompany(CompanyRequestModel reqmodel)
        {
            GetResults result = new GetResults();
            try
            {
                if (ModelState.IsValid)
                {
                    result = _company.CreateUpdateCompany(reqmodel).Result;
                    Helper.MoveFileToS3Server(EnumImageType.CompanyLogo, Convert.ToInt64(result.Data), reqmodel.Logo);
                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Index", "Company");
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
            return View(reqmodel);
        }
        public IActionResult Edit(int id)
        {
            CompanyRequestModel cmodel = new CompanyRequestModel();
            try
            {
                if (id > 0)
                {
                    BindCountries();
                    var company = _company.GetCompanyById(id).Result;
                    cmodel = (CompanyRequestModel)company.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message.ToString());
            }
            BindCountries();
            return View(cmodel);
        }

       
        public IActionResult VerifyCompany(ChangeStatusModel cModel)
        {
            var resp = _company.VerifyCompany(cModel.Id).Result;
            return Json(resp);
        }
        public IActionResult DeleteCompany(ChangeStatusModel cModel)
        {
            var resp = _company.DeleteCompany(cModel.Id).Result;
            return Json(resp);
        }
        private void BindCompany()
        {
            var Companies = (List<CompanyModel>)_company.GetMasterCompanies().Result.Data;
            ViewBag.Companies = new SelectList(Companies, "id", "NameEng");
        }
        private void BindEventType()
        {
            var Companies = (List<EventViewModel>)_company.GetMasterEventType().Result.Data;
            ViewBag.EventType = new SelectList(Companies, "Id", "NameEng");
        }
        private void BindCountries()
        {
            try
            {
                var cntry = _location.GetMasterCountries().Result;
                ViewBag.Countries = new SelectList(cntry, "Id", "CountryNameEng");

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
        private void BindDesignation()
        {
            try
            {
                List<DesignationViewModel> roles = new List<DesignationViewModel>();
                roles = (List<DesignationViewModel>)_master.GetMasterDesignation().Result.Data;
                ViewBag.designation = new SelectList(roles, "Id", "Designation");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public IActionResult BindRegion(int Id)
        {
            try
            {
                var region = _location.GetMasterRegions(Id).Result;
                return Json(region);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return Json(null);
        }
        public IActionResult BindDistricts(int Id)
        {
            try
            {
                var districts = _location.GetMasterDistricts(Id).Result;
                return Json(districts);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return Json(null);
        }

        #endregion

        #region CompanyBrand
        private void BindCompanyAndBrand()
        {
            BindCompany();
            var brands = (List<BrandModel>)_master.GetMasterBrand().Result.Data;
            ViewBag.Brands = new SelectList(brands, "Id", "NameEng");

        }
        public IActionResult Brand()
        {
            BindCompanyAndBrand();
            return View();
        }
        public IActionResult AddUpdateCompanyBrand(CompanyBrandRequestModel reqmodel)
        {
            GetResults res = new GetResults();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _company.AddEditCompanyBrand(reqmodel).Result;
                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Brand", "Company");
                        //res = new GetResults()
                        //{
                        //    IsSuccess = true,
                        //    Message = "Company Brand Mapping Successfull."
                        //};
                    }
                    else
                    {
                         _notyfService.Warning(result.Message);
                        //res = new GetResults()
                        //{
                        //    IsSuccess = false,
                        //    Message = "Company Brand Mapping Failed."
                        //};
                    }
                }
                else
                {
                    //res = new GetResults()
                    //{
                    //    IsSuccess = false,
                    //    Message = "Company Brand Mapping Failed."
                    //};
                     _notyfService.Error("Validation Error !!!");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                   _notyfService.Error(ex.Message.ToString());
                //res = new GetResults()
                //{
                //    IsSuccess = false,
                //    Message = "Company Brand Mapping Failed"
                //};
            }
            BindCompanyAndBrand();
            _logger.Info(JsonConvert.SerializeObject(res));
            //return Json(res);
            return View("Brand", reqmodel);
        }
        public IActionResult GetAllCompanyBrand(int companyId)
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
                var allData = _company.GetAllCompanyBrand(pageNo, pageSize, searchValue,companyId).Result;
                var cData = (List<CompanyBrandViewModel>)allData.Data;
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
        public IActionResult EditCompanyBrand(int id)
        {
            CompanyBrandRequestModel cmodel = new CompanyBrandRequestModel();
            try
            {

                var company = _company.GetCompanyBrandById(id).Result;
                cmodel = (CompanyBrandRequestModel)company.Data;
                BindCompanyAndBrand();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message.ToString());
            }

            return View("Brand", cmodel);
        }
        public IActionResult DeleteCompanyBrand(int id)
        {
            try
            {
                var resp = _company.DeleteCompanyBrand(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("Brand", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }
            BindCompanyAndBrand();
            return View("Brand");
        }

        [HttpGet]
        public IActionResult BindCompanyBrand(int id)
        {
            try
            {
                var acat = _master.GetMasterBrand().Result;
                var bcat = _company.GetCompanyBrand(id).Result;
                var brands = (List<BrandModel>)acat.Data;
                if (bcat.Count > 0)
                    brands.Where(w => bcat.Contains(w.Id)).ToList().ForEach(f => f.IsSelected = true);
                return Json(brands);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return null;
        }
        #endregion

        #region CompanyCategory
        private void BindCompanyAndCategory()
        {
            var Companies = (List<CompanyModel>)_company.GetMasterCompanies().Result.Data;
            ViewBag.Companies = new SelectList(Companies, "id", "NameEng");

            var Categories = (List<CategoriesViewModel>)_master.GetMasterCategories().Result.Data;
            ViewBag.Categories = new SelectList(Categories, "Id", "NameEng");

        }
        public IActionResult Category()
        {
             BindCompanyAndCategory(); 
            return View();
        }
        public IActionResult CompanySuggestion()
        {
            var Companies = (List<CompanyModel>)_company.GetMasterCompanies().Result.Data;
            return Json(Companies);

        }
        public IActionResult GetAllCompanyCategory()
        {
            try
            {
                var cid = Convert.ToInt32(ExtensionHelper.GetSession("CompanyMasterId"));
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
                var allData = _company.GetAllCompanyCategory(pageNo, pageSize, searchValue, cid).Result;
                var cData = (List<CompanyCategoryViewModel>)allData.Data;
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
        public IActionResult EditCompanyCategory(int id)
        {
            CompanyCategoryRequestModel cmodel = new CompanyCategoryRequestModel();
            try
            {

                var company = _company.GetCompanyCategoryById(id).Result;
                cmodel = (CompanyCategoryRequestModel)company.Data;
                BindCompanyAndCategory();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message.ToString());
            }

            return View("Category", cmodel);
        }
        [HttpPost]
        public IActionResult AddUpdateCompanyCategory(CompanyCategoryRequestModel reqmodel)
        {
            GetResults res = new GetResults();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _company.AddEditCompanyCategory(reqmodel).Result;
                    if (result.IsSuccess)
                    {
                        //res = new GetResults()
                        //{
                        //    IsSuccess = true,
                        //    Message = "Company Category Mapping Successfull"
                        //};
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Category", "Company", new { id = reqmodel.CompanyId });
                    }
                    else
                    {
                        //res = new GetResults()
                        //{
                        //    IsSuccess = false,
                        //    Message = "Company Category Mapping failed"
                        //};
                        _notyfService.Warning(result.Message);
                    }
                }
                else
                {
                     _notyfService.Error("Validation Error !!!");
                    //res = new GetResults()
                    //{
                    //    IsSuccess = false,
                    //    Message = "Company Category Mapping failed"
                    //};
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                //res = new GetResults()
                //{
                //    IsSuccess = false,
                //    Message = "Company Category Mapping failed"
                //};
                 _notyfService.Error(ex.Message.ToString());
            }
            BindCompanyAndCategory();
            _logger.Info(JsonConvert.SerializeObject(res));
        //    return Json(res);
            return View("Category", reqmodel);
        }
        public IActionResult DeleteCompanyCategory(int id)
        {
            try
            {
                var resp = _company.DeleteCompanyCategory(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("Category", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }

            return View("Category");
        }

        [HttpGet]
        public IActionResult BindComapanyCategory(int id)
        {
            try
            {
                var acat = _master.GetMasterCategories().Result;
                var bcat = _company.GetCompanyCategory(id).Result;
                var categories = (List<CategoriesViewModel>)acat.Data;
                if (bcat.Count > 0)
                    categories.Where(w => bcat.Contains(w.Id)).ToList().ForEach(f => f.IsSelected = true);
                return Json(categories);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return null;
        }

        #endregion

        #region CompanyProduct
        public IActionResult Product()
        {
            BindCompany();
            BindCountries();
            return View();
        }
        public IActionResult GetAllCompanyProducts()
        {
            try
            {
                var cid =Convert.ToInt32(ExtensionHelper.GetSession("CompanyMasterId"));
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
                var allData = _company.GetAllCompanyProduct(pageNo, pageSize, searchValue, cid).Result;
                var cData = (List<CompanyProductViewModel>)allData.Data;
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
        public IActionResult AddCompanyProduct()
        {
            BindCompany();
            BindCountries();
            return View();
        }
        public IActionResult AddUpdateCompanyProduct(CompanyProductRequestModel reqmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cid = Convert.ToInt32(ExtensionHelper.GetSession("CompanyMasterId"));
                    reqmodel.CompanyId = cid;
                    var result = _company.AddEditCompanyProduct(reqmodel).Result;
                    Helper.MoveFileToS3Server(EnumImageType.ProductLogo, Convert.ToInt64(result.Data), reqmodel.Image);
                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Product", "Company");
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
            return View("AddCompanyProduct", reqmodel);
        }
        public IActionResult EditCompanyProduct(int id)
        {
            CompanyProductRequestModel cmodel = new CompanyProductRequestModel();
            try
            {

                BindCompany();
                BindCountries();
                if (id > 0)
                {
                    var company = _company.GetCompanyProductById(id).Result;
                    var s = (CompanyProductViewModel)company.Data;
                    cmodel = new CompanyProductRequestModel
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
                        IsPublished = s.IsPublished.HasValue?s.IsPublished.Value:false,
                        HasOffers = s.HasOffers.Value == true ? true : false,
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
                        OldPrice = s.OldPrice,
                        CompanyName=s.Company
                        
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
        public IActionResult DeleteCompanyProduct(int id)
        {
            try
            {
                var resp = _company.DeleteCompanyProduct(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("Product", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }

            return View("Product");
        }

        #endregion

        #region CompanyServices
        public IActionResult Service()
        {
            return View();
        }
        public IActionResult GetAllCompanyServices()
        {
            try
            {
                var cid = Convert.ToInt32(ExtensionHelper.GetSession("CompanyMasterId"));
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
                var allData = _company.GetAllCompanyService(pageNo, pageSize, searchValue, cid).Result;
                var cData = (List<CompanyServiceViewModel>)allData.Data;
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
        public IActionResult AddCompanyService()
        {
            BindCompany();
            return View();
        }
        public IActionResult AddUpdateCompanyService(CompanyServiceRequestModel reqmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _company.AddEditCompanyService(reqmodel).Result;
                    Helper.MoveFileToS3Server(EnumImageType.ServiceLogo, Convert.ToInt64(result.Data), reqmodel.Image);
                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Service", "Company");
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
            return View("AddCompanyService", reqmodel);
        }

        public IActionResult EditCompanyService(int id)
        {
            CompanyServiceRequestModel cmodel = new CompanyServiceRequestModel();
            try
            {
                BindCompany();
                if (id > 0)
                {

                    var company = _company.GetCompanyServiceById(id).Result;
                    var s = (CompanyServiceViewModel)company.Data;
                    cmodel = new CompanyServiceRequestModel
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
                        IsPublished = s.IsPublished.HasValue ? s.IsPublished.Value : false,
                        HasOffers = s.HasOffers.HasValue ? s.HasOffers.Value : false,
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
                        OfferShortDescriptionArb = s.OfferShortDescriptionArb,
                        CompanyName=s.Company


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
        public IActionResult DeleteCompanyService(int id)
        {
            try
            {
                var resp = _company.DeleteCompanyService(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("Service", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }

            return View("Product");
        }

        #endregion

        #region CompanyBanner
        public IActionResult Banner()
        {
            return View();
        }
        public IActionResult GetAllCompanyBanner()
        {
            try
            {
                var cid = Convert.ToInt32(ExtensionHelper.GetSession("CompanyMasterId"));
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
                var allData = _company.GetAllCompanyBanners(pageNo, pageSize, searchValue, cid).Result;
                var cData = (List<CompanyBannerViewModel>)allData.Data;
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
        public IActionResult AddCompanyBanner()
        {
            BindCompany();
            return View();
        }
        public IActionResult AddUpdateCompanyBanner(CompanyBannerRequestModel reqmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _company.AddEditCompanyBanner(reqmodel).Result;
                    Helper.MoveFileToS3Server(EnumImageType.BannerEng, Convert.ToInt64(result.Data), reqmodel.ImageEng);
                    //Helper.MoveFileToS3Server(EnumImageType.BannerArb, Convert.ToInt64(result.Data), reqmodel.ImageArb);
                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Banner", "Company");
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
            return View("AddCompanyService", reqmodel);
        }
        public IActionResult EditCompanyBanner(int id)
        {
            CompanyBannerRequestModel cmodel = new CompanyBannerRequestModel();
            try
            {
                BindCompany();
                if (id > 0)
                {

                    var company = _company.GetCompanyBannerById(id).Result;
                    var s = (CompanyBannerViewModel)company.Data;
                    cmodel = new CompanyBannerRequestModel
                    {
                        Id = s.Id,
                        BannerNameEng = s.BannerNameEng,
                        BannerNameArb = s.BannerNameArb,
                        CompanyId = s.CompanyId,
                        EnglishUrl = s.EnglishUrl,
                        ArabicUrl = s.ArabicUrl,
                        ImageEng = s.ImageEng,
                        ImageArb = s.ImageArb,
                        Target = s.Target,
                        BannerStartDate = s.BannerStartDate,
                        BannerExpiryDate = s.BannerExpiryDate,
                        IsPublished = s.IsPublished.HasValue ? s.IsPublished.Value : false,
                        SortOrder = s.SortOrder,
                        CompanyName=s.CompanyName

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
        public IActionResult DeleteCompanyBanner(int id)
        {
            try
            {
                var resp = _company.DeleteCompanyBanner(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("Banner", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }

            return View("Banner");
        }

        #endregion

        #region CompanyGallery
        public IActionResult Gallery()
        {
            return View();
        }
        public IActionResult GetAllCompanyGallery()
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
                var allData = _company.GetAllCompanyGallery(pageNo, pageSize, searchValue).Result;
                var cData = (List<CompanyGalleryViewModel>)allData.Data;
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
        public IActionResult AddCompanyGallery()
        {
            BindCompany();
            return View();
        }
        public IActionResult AddUpdateCompanyGallery(CompanyGalleryRequestModel reqmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _company.AddEditCompanyGallery(reqmodel).Result;
                    Helper.MoveFileToS3Server(EnumImageType.GalleryFile, Convert.ToInt64(result.Data), reqmodel.File);
                    Helper.MoveFileToS3Server(EnumImageType.GalleryImage, Convert.ToInt64(result.Data), reqmodel.Image);
                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Gallery", "Company");
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
            return View("AddUpdateCompanyGallery", reqmodel);
        }
        public IActionResult EditCompanyGallery(int id)
        {
            CompanyGalleryRequestModel cmodel = new CompanyGalleryRequestModel();
            try
            {
                BindCompany();
                if (id > 0)
                {

                    var company = _company.GetCompanyGalleryById(id).Result;
                    var s = (CompanyGalleryViewModel)company.Data;
                    cmodel = new CompanyGalleryRequestModel
                    {
                        Id = s.Id,
                        Image = s.Image,
                        YoutubeVideoUrl = s.YoutubeVideoUrl,
                        File = s.File,
                        CompanyMenuId = s.CompanyMenuId,
                        TitleEng = s.TitleEng,
                        TitleArb = s.TitleArb,
                        ShortDescriptionEng = s.ShortDescriptionEng,
                        ShortDescriptionArb = s.ShortDescriptionArb,
                        DescriptionEng = s.DescriptionEng,
                        DescriptionArb = s.DescriptionArb,
                        Target = s.Target,
                        TargetUrl = s.TargetUrl,
                        IsPublished = s.IsPublished,
                        CompanyName=s.CompanyName

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
        public IActionResult DeleteCompanyGallery(int id)
        {
            try
            {
                var resp = _company.DeleteCompanyGallery(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("Gallery", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }

            return View("Gallery");
        }

        #endregion

        #region CompanyOffers
        public IActionResult Offer()
        {
            return View();
        }
        public IActionResult GetAllCompanyOffers()
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
                var allData = _company.GetAllCompanyOffer(pageNo, pageSize, searchValue).Result;
                var cData = (List<CompanyOffersViewModel>)allData.Data;
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
        public IActionResult AddCompanyOffers()
        {
            BindCompany();
            return View();
        }
        public IActionResult AddUpdateCompanyOffers(CompanyOffersRequestModel reqmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _company.AddEditCompanyoffers(reqmodel).Result;
                    Helper.MoveFileToS3Server(EnumImageType.OfferImage, Convert.ToInt64(result.Data), reqmodel.Image);
                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Offer", "Company");
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
            return View("AddCompanyOffers", reqmodel);
        }
        public IActionResult EditCompanyOffers(int id)
        {
            CompanyOffersRequestModel cmodel = new CompanyOffersRequestModel();
            try
            {
                BindCompany();
                if (id > 0)
                {

                    var company = _company.GetCompanyOfferById(id).Result;
                    var s = (CompanyOffersViewModel)company.Data;
                    cmodel = new CompanyOffersRequestModel
                    {
                        Id = s.Id,
                        OfferNameEng = s.OfferNameEng,
                        OfferNameArb = s.OfferNameArb,
                        OfferDescriptionEng = s.OfferDescriptionEng,
                        OfferDescriptionArb = s.OfferDescriptionArb,
                        OfferShortDescriptionEng = s.OfferShortDescriptionEng,
                        OfferShortDescriptionArb = s.OfferShortDescriptionArb,
                        OfferDisplayDate = s.OfferDisplayDate,
                        OfferStartDate = s.OfferStartDate,
                        OfferEndDate = s.OfferEndDate,
                        CompanyId = s.CompanyId,
                        OldPrice = s.OldPrice,
                        Price = s.Price,
                        Image = s.Image,
                        IsPublished = s.IsPublished.HasValue ? s.IsPublished.Value : false,
                        CompanyName= s.CompanyName

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
        public IActionResult DeleteCompanyOffers(int id)
        {
            try
            {
                var resp = _company.DeleteCompanyOffer(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("Offer", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }

            return View("Offer");
        }

        #endregion

        #region CompanyLink
        public IActionResult Link()
        {
            return View();
        }
        public IActionResult GetAllCompanyLink()
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
                var allData = _company.GetAllCompanyLink(pageNo, pageSize, searchValue).Result;
                var cData = (List<CompanyLinkViewModel>)allData.Data;
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
        public IActionResult AddCompanyLink()
        {
            BindCompany();
            return View();
        }
        public IActionResult AddUpdateCompanyLink(CompanyLinksRequestModel reqmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _company.AddEditCompanyLink(reqmodel).Result;
                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Link", "Company");
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
            return View("AddCompanyLink", reqmodel);
        }
        public IActionResult EditCompanyLink(int id)
        {
            CompanyLinksRequestModel cmodel = new CompanyLinksRequestModel();
            try
            {
                BindCompany();
                if (id > 0)
                {

                    var company = _company.GetCompanyLinkById(id).Result;
                    var s = (CompanyLinkViewModel)company.Data;
                    cmodel = new CompanyLinksRequestModel
                    {
                        CompanyId = s.CompanyId,
                        LinkNameEng = s.LinkNameEng,
                        LinkNameArb = s.LinkNameArb,
                        EnglishUrl = s.EnglishUrl,
                        ArabicUrl = s.ArabicUrl,
                        Target = s.Target,
                        IsPublished = s.IsPublished,
                        SortOrder = s.SortOrder,
                        IsDeleted = false,
                        CreationTime = DateTime.Now,
                        CreatorUserId = s.CreatorUserId

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
        public IActionResult DeleteCompanyLink(int id)
        {
            try
            {
                var resp = _company.DeleteCompanyLinks(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("Link", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }

            return View("Link");
        }

        #endregion


        #region FreeListing

        public IActionResult FreeListing()
        {
            return View();
        }
        public IActionResult GetAllFreeListing()
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
                var allData = _company.GetFreeListing(pageNo, pageSize, searchValue).Result;
                var cData = (List<CompanyFreeListingViewModel>)allData.Data;
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
        public IActionResult DeleteFreeListing(int id)
        {
            try
            {
                var resp = _company.DeleteFreeListing(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("FreeListing", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }

            return View("Offer");
        }

        public IActionResult ApproveRejectFreeListing(int id)
        {
            try
            {
                var resp = _company.ApproveRejectFreeListingCompany(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("FreeListing", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }

            return View("Offer");
        }

        public IActionResult FreeListingDetails(int id)
        {
            FreelistingDetailModel fmodel = new FreelistingDetailModel();
            if (id > 0)
            {
                try
                {
                    var allData = _company.GetFreeListingDetails(id).Result;
                    fmodel = (FreelistingDetailModel)allData.Data;
                    return View(fmodel);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                    return View(fmodel);

                }
            }
            return View(fmodel);



        }

        #endregion

        #region CompanyTeam
        public IActionResult CompanyTeam()
        {
            return View();
        }
        public IActionResult GetAllCompanyTeam()
        {
            try
            {
                var cid = Convert.ToInt32(ExtensionHelper.GetSession("CompanyMasterId"));
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
                var allData = _company.GetAllCompanyTeam(pageNo, pageSize, searchValue, cid).Result;
                var cData = (List<CompanyTeamViewModel>)allData.Data;
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
        public IActionResult AddCompanyTeam()
        {
            BindCompany();
            BindDesignation();
            return View();
        }
        public IActionResult AddUpdateCompanyTeam(CompanyTeamRequestModel reqmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _company.AddEditCompanyTeam(reqmodel).Result;
                    Helper.MoveFileToS3Server(EnumImageType.TeamPicture, Convert.ToInt64(result.Data), reqmodel.ProfilePic);

                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("CompanyTeam", "Company");
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
            return View("AddCompanyTeam", reqmodel);
        }
        public IActionResult EditCompanyTeam(int id)
        {
            CompanyTeamRequestModel cmodel = new CompanyTeamRequestModel();
            try
            {
                BindCompany();
                BindDesignation();
                if (id > 0)
                {

                    var company = _company.GetCompanyTeamById(id).Result;
                    var s = (CompanyTeamViewModel)company.Data;
                    cmodel = new CompanyTeamRequestModel
                    {
                        Id = s.Id,
                        CompanyId = s.CompanyId,
                        FullName = s.FullName,
                        Designation = s.Designation,
                        ProfilePic = s.ProfilePic,
                        IsPublished = s.IsPublished.HasValue ? s.IsPublished.Value : false,
                        IsDeleted = false,
                        CreationTime = DateTime.Now,
                        CreatorUserId = s.CreatorUserId,
                        CompanyName=s.CompanyName

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
        public IActionResult DeleteCompanyTeam(int id)
        {
            try
            {
                var resp = _company.DeleteCompanyTeam(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("CompanyTeam", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }

            return View("CompanyTeam");
        }

        #endregion

        #region CompanyAwards
        public IActionResult CompanyAward()
        {
            return View();
        }
        public IActionResult GetAllCompanyAward()
        {
            try
            {
                var cid = Convert.ToInt32(ExtensionHelper.GetSession("CompanyMasterId"));
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
                var allData = _company.GetAllCompanyAwards(pageNo, pageSize, searchValue, cid).Result;
                var cData = (List<CompanyAwardsViewModel>)allData.Data;
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
        public IActionResult AddCompanyAward()
        {
            BindCompany();
            return View();
        }
        public IActionResult AddUpdateCompanyAward(CompanyAwardsRequestModel reqmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _company.AddEditCompanyAwards(reqmodel).Result;
                    Helper.MoveFileToS3Server(EnumImageType.AwardFile, Convert.ToInt64(result.Data), reqmodel.AwardFile);

                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("CompanyAward", "Company");
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
            return View("AddCompanyAward", reqmodel);
        }
        public IActionResult EditCompanyAward(int id)
        {
            CompanyAwardsRequestModel cmodel = new CompanyAwardsRequestModel();
            try
            {
                BindCompany();
                if (id > 0)
                {

                    var company = _company.GetCompanyAwardsById(id).Result;
                    var s = (CompanyAwardsViewModel)company.Data;
                    cmodel = new CompanyAwardsRequestModel
                    {
                        Id = s.Id,
                        CompanyId = s.CompanyId,
                        AwardTitle = s.AwardTitle,
                        AwardDesc = s.AwardDesc,
                        AwardFile = s.AwardFile,
                        IsPublished = s.IsPublished.HasValue ? s.IsPublished.Value : false,
                        IsDeleted = false,
                        CreationTime = DateTime.Now,
                        CreatorUserId = s.CreatorUserId,
                        CompanyName=s.CompanyName,
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
        public IActionResult DeleteCompanyAward(int id)
        {
            try
            {
                var resp = _company.DeleteCompanyAwards(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("CompanyAward", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }

            return View("CompanyAward");
        }

        #endregion

        #region CompanyAddress
        public IActionResult CompanyAddress()
        {
            return View();
        }
        public IActionResult GetAllCompanyAddress()
        {
            try
            {
                var cid = Convert.ToInt32(ExtensionHelper.GetSession("CompanyMasterId"));
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
                var allData = _company.GetAllCompanyAddress(pageNo, pageSize, searchValue, cid).Result;
                var cData = (List<CompanyAddressViewModel>)allData.Data;
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
        public IActionResult AddCompanyAddress()
        {
            BindCountries();
            BindCompany();
            return View();
        }
        public IActionResult AddUpdateCompanyAddress(CompanyAddressRequestModel reqmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _company.AddEditCompanyAddress(reqmodel).Result;
                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("CompanyAddress", "Company");
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
            return View("AddCompanyAddress", reqmodel);
        }
        public IActionResult EditCompanyAddress(int id)
        {
            CompanyAddressRequestModel cmodel = new CompanyAddressRequestModel();
            try
            {
                BindCountries();
                BindCompany();
                if (id > 0)
                {

                    var company = _company.GetCompanyAddressById(id).Result;
                    var s = (CompanyAddressViewModel)company.Data;
                    cmodel = new CompanyAddressRequestModel
                    {
                        Id = s.Id,
                        CompanyId = s.CompanyId,
                        AddressDesc = s.AddressDesc,
                        CountryId = s.CountryId,
                        Contact = s.Contact,
                        GoogleLocation = s.GoogleLocation,
                        Website = s.Website,
                        RegionId = s.RegionId,
                        IsPublished = s.IsPublished.HasValue ? s.IsPublished.Value : false,
                        CompanyName=s.CompanyName
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
        public IActionResult DeleteCompanyAddress(int id)
        {
            try
            {
                var resp = _company.DeleteCompanyAddress(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("CompanyAddress", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }

            return View("CompanyAddress");
        }

        #endregion

        #region CompanyVideo
        public IActionResult CompanyVideo()
        {
            return View();
        }
        public IActionResult GetAllCompanyVideo()
        {
            try
            {
                var cid = Convert.ToInt32(ExtensionHelper.GetSession("CompanyMasterId"));
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
                var allData = _company.GetAllCompanyVideo(pageNo, pageSize, searchValue, cid).Result;
                var cData = (List<CompanyVideoViewModel>)allData.Data;
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
        public IActionResult AddCompanyVideo()
        {
            BindCompany();
            return View();
        }
        public IActionResult AddUpdateCompanyVideo(CompanyVideoRequestModel reqmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _company.AddEditCompanyVideo(reqmodel).Result;
                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("CompanyVideo", "Company");
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
            return View("AddCompanyVideo", reqmodel);
        }
        public IActionResult EditCompanyVideo(int id)
        {
            CompanyVideoRequestModel cmodel = new CompanyVideoRequestModel();
            try
            {
                BindCompany();
                if (id > 0)
                {

                    var company = _company.GetCompanyVideoById(id).Result;
                    var s = (CompanyVideoViewModel)company.Data;
                    cmodel = new CompanyVideoRequestModel
                    {
                        Id = s.Id,
                        CompanyId = s.CompanyId,
                        VideoNameArb = s.VideoNameArb,
                        VideoNameEng = s.VideoNameEng,
                        EnglishUrl = s.EnglishUrl,
                        ArabicUrl = s.ArabicUrl,
                        IsPublished = s.IsPublished.HasValue ? s.IsPublished.Value : false,
                        CompanyName=s.CompanyName
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
        public IActionResult DeleteCompanyVideo(int id)
        {
            try
            {
                var resp = _company.DeleteCompanyVideo(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("CompanyVideo", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }

            return View("CompanyVideo");
        }

        #endregion

        #region CompanyNewsArticle
        public IActionResult CompanyNews()
        {
            return View();
        }
        public IActionResult GetAllCompanyNews()
        {
            try
            {
                var cid = Convert.ToInt32(ExtensionHelper.GetSession("CompanyMasterId"));
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
                var allData = _company.GetAllCompanyNewsArticle(pageNo, pageSize, searchValue, cid).Result;
                var cData = (List<CompanyNewsArticleViewModel>)allData.Data;
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
        public IActionResult AddCompanyNews()
        {
            BindCompany();
            return View();
        }
        public IActionResult AddUpdateCompanyNews(CompanyNewsArticleRequestModel reqmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _company.AddEditCompanyNewsArtical(reqmodel).Result;
                    Helper.MoveFileToS3Server(EnumImageType.NewsImage, Convert.ToInt64(result.Data), reqmodel.NewsUrl);

                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("CompanyNews", "Company");
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
            return View("AddCompanyNews", reqmodel);
        }
        public IActionResult EditCompanyNews(int id)
        {
            CompanyNewsArticleRequestModel cmodel = new CompanyNewsArticleRequestModel();
            try
            {
                BindCompany();
                if (id > 0)
                {

                    var company = _company.GetCompanyNewsArticleById(id).Result;
                    var s = (CompanyNewsArticleViewModel)company.Data;
                    cmodel = new CompanyNewsArticleRequestModel
                    {
                        Id = s.Id,
                        CompanyId = s.CompanyId,
                        NewsTitle = s.NewsTitle,
                        NewsDesc = s.NewsDesc,
                        NewsUrl = s.NewsUrl,
                        IsPublished = s.IsPublished.HasValue ? true : false,
                        CompanyName=s.CompanyName
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
        public IActionResult DeleteCompanyNews(int id)
        {
            try
            {
                var resp = _company.DeleteCompanyNewsArtical(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("CompanyNews", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }

            return View("CompanyNews");
        }

        #endregion
        #region CompanyEvent
        public IActionResult Event()
        {
            return View();
        }
        public IActionResult GetAllCompanyEvent()
        {
            try
            {
                var cid = Convert.ToInt32(ExtensionHelper.GetSession("CompanyMasterId"));
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
                var allData = _company.GetAllCompanyEvent(pageNo, pageSize, searchValue, cid).Result;
                var cData = (List<CompanyEventViewModel>)allData.Data;
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
        public IActionResult AddCompanyEvent()
        {
            BindCompany();
            BindEventType();
            return View();
        }
        public IActionResult AddUpdateCompanyEvent(CompanyEventRequestModel reqmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _company.AddEditCompanyEvent(reqmodel).Result;
                    Helper.MoveFileToS3Server(EnumImageType.NewsImage, Convert.ToInt64(result.Data), reqmodel.EventImage);

                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Event", "Company");
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
            return View("AddCompanyEvent", reqmodel);
        }
        public IActionResult EditCompanyEvent(int id)
        {
            CompanyEventRequestModel cmodel = new CompanyEventRequestModel();
            try
            {
                BindCompany();
                BindEventType();
                if (id > 0)
                {

                    var company = _company.GetCompanyEventById(id).Result;
                    var s = (CompanyEventViewModel)company.Data;
                    cmodel = new CompanyEventRequestModel
                    {
                        Id = s.Id,
                        CompanyId = s.CompanyId,
                        EventTitle = s.EventTitle,
                        EventDesc = s.EventDesc,
                        EventImage = s.EventImage,
                        StartDate = s.StartDate,
                        StartTime = s.StartTime,
                        EndDate = s.EndDate,
                        EndTime = s.EndTime,
                        EventUrl = s.EventUrl,
                        EventTypeId = s.EventTypeId,
                        CompanyName=s.CompanyName,
                        IsPublished = s.IsPublished,
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
        public IActionResult DeleteCompanyEvent(int id)
        {
            try
            {
                var resp = _company.DeleteCompanyEvent(id).Result;

                if (resp.IsSuccess)
                {
                    _notyfService.Success(resp.Message);
                    return RedirectToAction("Event", "Company");
                }
                else
                {
                    _notyfService.Warning(resp.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message);
            }

            return View("Event");
        }

        #endregion


        public IActionResult Package()
        {
            return View();
        }
        public IActionResult Video()
        {
            return View();
        }
        public IActionResult Voucher()
        {
            return View();
        }
        public IActionResult NewsArticle()
        {
            return View();
        }

        public IActionResult ReviewLike()
        {
            return View();
        }



        private void BindRoles()
        {
            try
            {
                List<RoleModel> roles = new List<RoleModel>();
                roles = _master.GetMasterRoles().Result;
                ViewBag.Roles = new SelectList(roles, "Id", "DisplayName");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        [HttpGet]
        public JsonResult SearchCompany(string term)
        {
            
            var allData = _company.SearchCompany(term).Result;
            var cData = (List<CompanyModel>)allData.Data;
         

            return Json(cData);
        }

        private CompanyRequestModel companydetail(int id)
        {

            CompanyRequestModel cmodel = new CompanyRequestModel();
            var company = _company.GetCompanyById(id).Result;
            cmodel = (CompanyRequestModel)company.Data;
            return cmodel;
        }
        public IActionResult SetCompany(int id)
        {
            if (id > 0)
            { 
                var cdetail = companydetail(id);
                ExtensionHelper.SetSession("Companyname", cdetail.NameEng);
                ExtensionHelper.SetSession("CompanyMasterId", Convert.ToString(id));
            }
            return View("Index", "Company");
        }
    }
  
}
