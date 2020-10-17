using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.SearchModel
{
   public class SM_PurchaseOrder
    {
        public string PurchaseOrderGUID { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string SupplierGUID { get; set; }
        public string SupplierName { get; set; }
        public string PurchaseOrderDate { get; set; }
        public string WarehouseGUID { get; set; }
        public string ApprovalStatus { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
