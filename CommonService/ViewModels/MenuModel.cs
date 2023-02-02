using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.ViewModels
{
    public class MenuModel
    {
        public int MenuId { get; set; }
        public int ParentId { get; set; }
        public string MenuName { get; set; }
        public string ComponentName { get; set; }
        public string MenuPath { get; set; }
        public string MenuIcon { get; set; }
        public int? Sequence { get; set; }
        public bool IsActivated { get; set; }
        public bool IsSelected { get; set; }
        public bool IsActive { get; set; }
        public List<MenuModel> ChildMenus { get; set; }
    }
}
