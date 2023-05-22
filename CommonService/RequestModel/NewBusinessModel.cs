using CommonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public class NewBusinessModel
    {
        public UserRequestModel NewUserDetails { get; set; }
        public FreeListingModel FreeListingDetails { get; set; }

    }
}
