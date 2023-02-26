using CommonService.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public class ChangeStatusModel
    {
        public int Id { get; set; }
        public EnumStatus Status { get; set; }
    }
}
