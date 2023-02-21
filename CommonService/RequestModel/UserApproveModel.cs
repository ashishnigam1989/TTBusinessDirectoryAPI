using CommonService.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public class UserApproveModel
    {
        public int UserId { get; set; }
        public EnumStatus Status { get; set; }
    }
}
