using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace DataCore.Models
{
    public class RoleScreenMap
    {

        public string GUID { get; set; }
        public string RoleGUID { get; set; }
        public string ScreenGUID { get; set; }
        public string ScreenName { get; set; }
        public string ScreenCategoryGUID { get; set; }
        public string ScreenCategoryName { get; set; }
        public int? AllowAccess { get; set; }
        public int? AllowAction { get; set; }
 
 
    }


}
