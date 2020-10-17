using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace DataCore.Models
{
    public class ScreenCategory
    {
        public int ID { get; set; }
        public string GUID { get; set; }
        public string ScreenCategoryName { get; set; }
        public string Icon { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string UpDatedDate { get; set; }
        public string UpDatedBy { get; set; }
        public string UpDatedByName { get; set; }
        public int Status { get; set; }

        public List<Screen> ScreenList { get; set; }
        public List<RoleScreenMap> RoleScreenMapList { get; set; }
    }
}
