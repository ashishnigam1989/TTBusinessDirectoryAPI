using ApplicationService.IServices;
using AspNetCoreHero.ToastNotification.Abstractions;
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
        private readonly INotyfService _notyfService;

        public CompanyController(ICompanies company, INotyfService notyfService)
        {
            _logger = LogManager.GetLogger("Company");
            _company = company; 
            _notyfService = notyfService;
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
            CompanyRequestModel cmodel=new CompanyRequestModel();
            try
            {
                var company = _company.GetCompanyById(id).Result;
                cmodel = (CompanyRequestModel)company.Data;
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
                _notyfService.Error(ex.Message.ToString());
            }
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
        public IActionResult Category()
        {
            return View();
        }
        public IActionResult Brand()
        {
            return View();
        }
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
