using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.SearchModel
{
    public class SM_Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
