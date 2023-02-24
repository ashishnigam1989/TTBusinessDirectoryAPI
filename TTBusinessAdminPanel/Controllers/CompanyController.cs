using ApplicationService.IServices;
using CommonService.RequestModel;
using CommonService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TTBusinessAdminPanel.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ILogger<CompanyController> _logger;
        private ICompany _company;

        public CompanyController(ILogger<CompanyController> logger, ICompany company)
        {
            _logger = logger;
            _company = company;
        }


        //View/Add/Edit
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllCompany()
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
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
           var company= _company.GetCompanyById(id).Result;
            CompanyRequestModel cmodel = (CompanyRequestModel)company.Data;
            return View(cmodel);
        }

        //View/Add/Delete
        public IActionResult Category()
        {
            return View();
        }
        public IActionResult CategoryAdd()
        {
            return View();
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
    }
}
