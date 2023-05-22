using CommonService.RequestModel;
using CommonService.ViewModels;
using DatabaseService.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.IServices
{
    public interface IListing
    {
        public Task AddFreeListing(FreeListingModel freeListing);
    }
}
