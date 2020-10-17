using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.SearchModel
{
    public class SM_UOMType
    {
        public int UOMTypeID { get; set; }
        public string Type { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
