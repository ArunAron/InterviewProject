using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.SearchModel
{
    public class SM_Stock
    {
     
        public string MaterialName { get; set; }
        public string MaterialGUID { get; set; }
        public string WarehouseGUID { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
