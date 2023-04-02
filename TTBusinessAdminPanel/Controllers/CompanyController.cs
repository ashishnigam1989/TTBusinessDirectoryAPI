﻿using ApplicationService.IServices;
using ApplicationService.Services;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using CommonService.RequestModel;
using CommonService.ViewModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public CompanyController(ICompanies company,IMaster master,ILocation location, INotyfService notyfService)
        {
            _logger = LogManager.GetLogger("Company");
            _company = company; 
            _master= master;
            _location= location;    
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
            return View(reqmodel);
        }
        public IActionResult Edit(int id)
        {
            CompanyRequestModel cmodel = new CompanyRequestModel();
            try
            {
                var company = _company.GetCompanyById(id).Result;
                cmodel = (CompanyRequestModel)company.Data;
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

        #endregion

        #region CompanyBrand
        private void BindCompanyAndBrand()
        {
            var Companies = (List<CompanyModel>)_company.GetMasterCompanies().Result.Data;
            ViewBag.Companies = new SelectList(Companies, "id", "NameEng");

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
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _company.AddEditCompanyBrand(reqmodel).Result;
                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Brand", "Company");
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
            BindCompanyAndBrand();
            return View("Brand", reqmodel);
        }
        public IActionResult GetAllCompanyBrand()
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
                var allData = _company.GetAllCompanyBrand(pageNo, pageSize, searchValue).Result;
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
                var allData = _company.GetAllCompanyCategory(pageNo, pageSize, searchValue).Result;
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
        public IActionResult AddUpdateCompanyCategory(CompanyCategoryRequestModel reqmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _company.AddEditCompanyCategory(reqmodel).Result;
                    if (result.IsSuccess)
                    {
                        _notyfService.Success(result.Message);
                        return RedirectToAction("Category", "Company");
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
            BindCompanyAndCategory();
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
        #endregion
        public IActionResult Offer()
        {
            return View();
        }
        public IActionResult Product()
        {
            return View();
        }
        public IActionResult Service()
        {
            return View();
        }
        public IActionResult Banner()
        {
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }
        public IActionResult Link()
        {
            return View();
        }
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
        public IActionResult Event()
        {
            return View();
        }
        public IActionResult ReviewLike()
        {
            return View();
        }
    }
}
