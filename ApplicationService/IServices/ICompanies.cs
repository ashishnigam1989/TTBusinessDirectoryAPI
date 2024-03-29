﻿using CommonService.RequestModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.IServices
{
    public interface ICompanies
    {
        Task<GetResults> GetAllCompanies(int page, int limit, string searchValue);
        Task<GetResults> GetCompanyById(int id);
        Task<GetResults> CreateUpdateCompany(CompanyRequestModel creqmodel);
        Task<GetResults> DeleteCompany(int id);
        Task<GetResults> VerifyCompany(int id);
        Task<GetResults> GetMasterCompanies();
        Task<GetResults> GetMasterEventType();

        Task<GetResults> AddEditCompanyBrand(CompanyBrandRequestModel cbModel);
        Task<GetResults> GetAllCompanyBrand(int page, int limit, string searchValue,int companyId);
        Task<GetResults> GetCompanyBrandById(int id);
        Task<GetResults> DeleteCompanyBrand(int Id);
        Task<List<long>> GetCompanyBrand(int companyid);


        Task<GetResults> GetAllCompanyCategory(int pageNo, int pageSize, string searchValue, int companyId);
        Task<GetResults> GetCompanyCategoryById(int id);
        Task<GetResults> DeleteCompanyCategory(int Id);
        Task<GetResults> AddEditCompanyCategory(CompanyCategoryRequestModel ccModel);
        Task<List<long>> GetCompanyCategory(int companyid);

        Task<GetResults> AddEditCompanyProduct(CompanyProductRequestModel cpModel);
        Task<GetResults> DeleteCompanyProduct(int Id); 
        Task<GetResults> GetCompanyProductById(int id);
        Task<GetResults> GetAllCompanyProduct(int page, int limit, string searchValue,int id);


        Task<GetResults> AddEditCompanyService(CompanyServiceRequestModel csModel);
        Task<GetResults> DeleteCompanyService(int Id);
        Task<GetResults> GetAllCompanyService(int page, int limit, string searchValue, int id);
        Task<GetResults> GetCompanyServiceById(int id);
        Task<GetResults> GetFeaturedCompanies(int limit);

        Task<GetResults> AddEditCompanyBanner(CompanyBannerRequestModel csModel);
        Task<GetResults> DeleteCompanyBanner(int Id);
        Task<GetResults> GetAllCompanyBanners(int page, int limit, string searchValue, int id);
        Task<GetResults> GetCompanyBannerById(int id);

        Task<GetResults> AddEditCompanyGallery(CompanyGalleryRequestModel csModel);
        Task<GetResults> DeleteCompanyGallery(int Id);
        Task<GetResults> GetAllCompanyGallery(int page, int limit, string searchValue);
        Task<GetResults> GetCompanyGalleryById(int id);

        Task<GetResults> GetAllKeywords();

        Task<GetResults> GetCompanyDetailsById(long companyId, int limit = 20);

        Task<GetResults> AddEditCompanyoffers(CompanyOffersRequestModel csModel);
        Task<GetResults> DeleteCompanyOffer(int Id);
        Task<GetResults> GetAllCompanyOffer(int page, int limit, string searchValue);
        Task<GetResults> GetCompanyOfferById(int id);

        Task<GetResults> AddEditCompanyLink(CompanyLinksRequestModel csModel);
        Task<GetResults> DeleteCompanyLinks(int Id);
        Task<GetResults> GetAllCompanyLink(int page, int limit, string searchValue);
        Task<GetResults> GetCompanyLinkById(int id);


        Task<GetResults> GetFreeListing(int page, int limit, string searchValue);
        Task<GetResults> ApproveRejectFreeListingCompany(int id);
        Task<GetResults> DeleteFreeListing(int Id);
        Task<GetResults> GetFreeListingDetails(int id);
        Task<GetResults> GetFreeListing(int id);

        Task<GetResults> AddEditCompanyTeam(CompanyTeamRequestModel ctModel);
        Task<GetResults> DeleteCompanyTeam(int Id);
        Task<GetResults> GetAllCompanyTeam(int page, int limit, string searchValue, int id);
        Task<GetResults> GetCompanyTeamById(int id);


        Task<GetResults> AddEditCompanyAwards(CompanyAwardsRequestModel ctModel);
        Task<GetResults> DeleteCompanyAwards(int Id);
        Task<GetResults> GetAllCompanyAwards(int page, int limit, string searchValue, int id=0);
        Task<GetResults> GetCompanyAwardsById(int id);


        Task<GetResults> AddEditCompanyAddress(CompanyAddressRequestModel ctModel);
        Task<GetResults> DeleteCompanyAddress(int Id);
        Task<GetResults> GetAllCompanyAddress(int page, int limit, string searchValue, int id);
        Task<GetResults> GetCompanyAddressById(int id);


        Task<GetResults> AddEditCompanyVideo(CompanyVideoRequestModel ctModel);
        Task<GetResults> DeleteCompanyVideo(int Id);
        Task<GetResults> GetAllCompanyVideo(int page, int limit, string searchValue, int id);
        Task<GetResults> GetCompanyVideoById(int id);


        Task<GetResults> AddEditCompanyNewsArtical(CompanyNewsArticleRequestModel ctModel);
        Task<GetResults> DeleteCompanyNewsArtical(int Id);
        Task<GetResults> GetAllCompanyNewsArticle(int page, int limit, string searchValue, int id=0);
        Task<GetResults> GetCompanyNewsArticleById(int id);

        Task<GetResults> AddEditCompanyEvent(CompanyEventRequestModel ctModel);
        Task<GetResults> DeleteCompanyEvent(int Id);
        Task<GetResults> GetAllCompanyEvent(int page, int limit, string searchValue, int id = 0);
        Task<GetResults> GetCompanyEventById(int id);



        Task<GetResults> SearchCompany(string searchValue);
        Task<GetResults> GetProductsByCompanyId(long companyId, int skip, int limit);
        Task<GetResults> GetServicesByCompanyId(long companyId, int skip, int limit);
        Task<GetResults> GetCompanyEvents(long companyId, int skip, int limit);

        Task<GetResults> GetCompanyNewsArticles(long companyId, int skip, int limit);
    }
}
