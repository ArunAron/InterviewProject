using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace DataCore.Models
{
    public class UOM
    {
        public int ID { get; set; }
        public string GUID { get; set; }
        public string MaterialGUID { get; set; }
        public string MaterialCode { get; set; }
        public string Level { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string Type { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string UpDatedDate { get; set; }
        public string UpDatedBy { get; set; }
        public string UpDatedByName { get; set; }
        public int Status { get; set; }
        
    }

    
}
