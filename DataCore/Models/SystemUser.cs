using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace DataCore.Models
{
    public class SystemUser
    {
        public int ID { get; set; }
        public string GUID { get; set; }
        public string SystemUserName { get; set; }
        public string SystemUserType { get; set; }
        public string SystemUserCode { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string RoleGUID { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public string DepartmentGUID { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string UpDatedDate { get; set; }
        public string UpDatedBy { get; set; }
        public string UpDatedByName { get; set; }
        public int Status { get; set; }

        //Not Mapped Field
        public string WarehouseGUID { get; set; }
        public string WarehouseName { get; set; }

    }


}
