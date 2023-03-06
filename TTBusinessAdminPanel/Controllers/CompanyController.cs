using ApplicationService.IServices;
using AutoMapper;
using CommonService.RequestModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public CompanyController(ICompanies company)
        {
            _logger = LogManager.GetLogger("Company");
            _company = company; 
        }


        //View/Add/Edit
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
            return View();
        }
        public IActionResult AddEditCompany(CompanyRequestModel reqmodel)
        {
            try
            {
                _company.CreateUpdateCompany(reqmodel);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View("Index");
        }
        public IActionResult Edit(int id)
        {
            CompanyRequestModel cmodel=new CompanyRequestModel();
            try
            {
                var company = _company.GetCompanyById(id).Result;
                cmodel = (CompanyRequestModel)company.Data;
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
            }
            return View(cmodel);
        }

        //View/Add/Delete
        public IActionResult Category()
        {
            return View("Category");
        }
        public IActionResult GetAllCategories()
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
            var allData = _company.GetAllCategories(pageNo, pageSize, searchValue).Result;
            var cData = (List<CategoriesViewModel>)allData.Data;
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                cData = cData.OrderBy(o => sortColumn + " " + sortColumnDirection).ToList();
            }
            recordsTotal = allData.Total;
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = cData };
            return Ok(jsonData);
        }
        public IActionResult CategoryAdd()
        {
            return View("CategoryAdd");
        }
        public IActionResult AddEditCategory(CategoriesRequestModel rceqmodel)
        {
            try
            {
                _company.CreateUpdateCategory(rceqmodel);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View("Category");
        }
        public IActionResult EditCategory(int id)
        {
            CategoriesRequestModel cmodel = new CategoriesRequestModel();
            try
            {
                var company = _company.GetCategoryById(id).Result;
               var s = (CategoriesViewModel)company.Data;
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
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View(cmodel);
        }

        //View/Add/Delete
        public IActionResult Brand()
        {
            return View();
        }
        public IActionResult BrandAdd()
        {
            return View();
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



    }
}
