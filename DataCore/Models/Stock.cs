﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace DataCore.Models
{
    public class Stock
    {
        public int ID { get; set; }
        public string GUID { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialGUID { get; set; }
        public string MaterialName { get; set; }
        public string UOMType { get; set; }
        public string WarehouseGUID { get; set; }
        public string WarehouseName { get; set; }
        public int Quantity { get; set; }
        public string BatchNumber { get; set; }
        public string Description { get; set; }
        public string ExpiryDate { get; set; }
        public string BuyingPrice { get; set; }
        public string SellingPrice { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string UpDatedDate { get; set; }
        public string UpDatedBy { get; set; }
        public string UpDatedByName { get; set; }
        public int Status { get; set; }

    }


}