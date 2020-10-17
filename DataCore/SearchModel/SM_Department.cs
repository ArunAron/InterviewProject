using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.SearchModel
{
    public class SM_Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
