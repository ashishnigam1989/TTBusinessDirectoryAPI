using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.RequestModel
{
    public class RoleMenuMapping
    {
        public int RoleId { get; set; }
        public List<int> Menus { get; set; }
    }

}
