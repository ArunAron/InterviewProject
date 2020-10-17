using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.SearchModel
{
    public class SM_Patient
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string PatientCode { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
