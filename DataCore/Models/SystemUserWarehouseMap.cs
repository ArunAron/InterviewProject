using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace DataCore.Models
{
    public class SystemUserWarehouseMap
    {

        public string GUID { get; set; }
        public string SystemUserGUID { get; set; }
        public string WarehouseGUID { get; set; }
        public string WarehouseName { get; set; }
        public int? AllowAccess { get; set; }
        public int? AllowAction { get; set; }
 
 
    }


}
