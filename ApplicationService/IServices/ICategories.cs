using CommonService.RequestModel;
using CommonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.IServices
{
    public interface ICategories
    {
        Task<GetResults> GetAllCategories(bool isFeatured);
    }
}
