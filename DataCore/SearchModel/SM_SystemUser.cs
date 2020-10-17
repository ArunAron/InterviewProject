using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.SearchModel
{
    public class SM_SystemUser
    {
        public int SystemUserID { get; set; }
        public string SystemUserName { get; set; }
        public string RoleGUID { get; set; }
        public string DepartmentGUID { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
