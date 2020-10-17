using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.SearchModel
{
    public class Serial_PurchaseOrder
    {
        public string PurchaseOrderGUID { get; set; }
        public string PurchaseOrderDetailGUID { get; set; }
        public string WarehouseGUID { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string SupplierGUID { get; set; }
        public string CompanyLocation { get; set; }
        public string PurchaseOrderDate { get; set; }
        public string VehicleDetail { get; set; }
        public string FarmerCode { get; set; }

        public string MaterialGUID { get; set; }
        public string MaterialName { get; set; }
        public string TotalWeight { get; set; }
        public string TotalWeightUOMType { get; set; }
        public string Quantity { get; set; }
        public string QuantityUOMType { get; set; }
        public string WeightPerQuantity { get; set; }
        public string WeightPerQuantityUOMType { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public string PackingDetail { get; set; }
        public string Remark { get; set; }
    }
}
