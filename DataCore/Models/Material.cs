using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace DataCore.Models
{
    public class Material
    {
        public int ID { get; set; }
        public string GUID { get; set; }
        public string MaterialName { get; set; }
        public string ShortCut { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialTypeGUID { get; set; }
        public string MaterialTypeName { get; set; }
        public string UOMType { get; set; }
        public string UOMDescription { get; set; }
        public string Quality { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string UpDatedDate { get; set; }
        public string UpDatedBy { get; set; }
        public string UpDatedByName { get; set; }
        public int Status { get; set; }

    }


}
