using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Models
{
   public class PurchaseOrder
    {
        public int ID { get; set; }
        public string GUID { get; set; }
        public string PurchaseOrderNo { get; set; } 
        public string SupplierGUID { get; set; }
        public string SupplierName { get; set; }
        public string CompanyLocation { get; set; }
        public string FarmerCode { get; set; }
        public string VehicleDetails { get; set; }
        public string RecieveNoteNo { get; set; }
        public string PurchaseOrderDate { get; set; }
        public string WarehouseGUID { get; set; }
        public string WarehouseName { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedByName { get; set; }
        public string RejectedDate { get; set; }
        public string RejectedBy { get; set; }
        public string RejectedByName { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string UpDatedDate { get; set; }
        public string UpDatedBy { get; set; }
        public string UpDatedByName { get; set; }
        public int Status { get; set; }
    }
}
