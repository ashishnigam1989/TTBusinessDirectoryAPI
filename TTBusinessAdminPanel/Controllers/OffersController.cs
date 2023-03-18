using ApplicationService.IServices;
using CommonService.RequestModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TTBusinessAdminPanel.Controllers
{
    public class OffersController : Controller
    {
        private Logger _logger;

        private IOffers _offers;
        public OffersController(IOffers offers)
        {
            _logger = LogManager.GetLogger("Offers");
            _offers = offers;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllOffers()
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
            var allData = _offers.GetAllOffers(pageNo, pageSize, searchValue).Result;
            var cData = (List<OffersViewModel>)allData.Data;
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                cData = cData.OrderBy(o => sortColumn + " " + sortColumnDirection).ToList();
            }
            recordsTotal = allData.Total;
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = cData };
            return Ok(jsonData);
        }
        public IActionResult AddOffer()
        {
            return View();
        }
        public IActionResult AddEditOffer(OffersRequestModel oreqmodel)
        {
            try
            {
                _offers.AddUpdateOffer(oreqmodel);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View("Index");
        }
        public IActionResult EditOffer(int id)
        {
            OffersRequestModel model = new OffersRequestModel();
            try
            {
                var offer = _offers.GetOfferById(id).Result;
                var oObj = (OffersViewModel)offer.Data;
                if (oObj != null)
                {
                    model = new OffersRequestModel()
                    {
                        Id = oObj.Id,
                        CompanyId = oObj.CompanyId,
                        OfferNameEng = oObj.OfferNameEng,
                        OfferNameArb = oObj.OfferNameArb,
                        OfferShortDescriptionEng = oObj.OfferShortDescriptionEng,
                        OfferShortDescriptionArb = oObj.OfferShortDescriptionArb,
                        OfferDescriptionEng = oObj.OfferDescriptionEng,
                        OfferDescriptionArb = oObj.OfferDescriptionArb,
                        OldPrice = oObj.OldPrice,
                        Price = oObj.Price,
                        OfferDisplayDate = oObj.OfferDisplayDate,
                        OfferStartDate = oObj.OfferStartDate,
                        OfferEndDate = oObj.OfferEndDate,
                        SortOrder = oObj.SortOrder,
                        IsDeleted = oObj.IsDeleted,
                        DeleterUserId = oObj.DeleterUserId,
                        DeletionTime = oObj.DeletionTime,
                        LastModificationTime = oObj.LastModificationTime,
                        LastModifierUserId = oObj.LastModifierUserId,
                        CreationTime = oObj.CreationTime,
                        CreatorUserId = oObj.CreatorUserId,
                        IsPublished = oObj.IsPublished,
                        Image = oObj.Image

                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View(model);
        }
        public IActionResult DeleteOffer(int id)
        {
            GetResults bmodel = new GetResults();
            try
            {
                bmodel = _offers.DeleteOffer(id).Result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return Json(bmodel);
        }
    }
}
