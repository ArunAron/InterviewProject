using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace DataCore.Models
{
    public class Screen
    {
        public int ID { get; set; }
        public string GUID { get; set; }
        public string ScreenName { get; set; }
        public string ScreenUniqueName { get; set; }
        public string ScreenCategoryName { get; set; }
        public string ScreenCategoryGUID { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string UpDatedDate { get; set; }
        public string UpDatedBy { get; set; }
        public string UpDatedByName { get; set; }
        public int Status { get; set; }

    }
}
