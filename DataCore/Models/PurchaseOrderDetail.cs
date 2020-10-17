using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Models
{
    public class PurchaseOrderDetail
    {
        public int ID { get; set; }
        public string GUID { get; set; }
        public string PurchaseOrderGUID { get; set; }
        public string MaterialGUID { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public string TotalWeight    { get; set; }
        public string TotalWeightUOMType { get; set; }
        public string Quantity { get; set; }
        public string QuantityUOMType { get; set; }
        public string WeightPerQuantity { get; set; }
        public string WeightPerQuantityUOMType { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public string PackingDetail { get; set; }
        public string Remark { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string UpDatedDate { get; set; }
        public string UpDatedBy { get; set; }
        public string UpDatedByName { get; set; }
        public int Status { get; set; }
    }
}
