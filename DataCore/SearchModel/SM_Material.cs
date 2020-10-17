using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.SearchModel
{
    public class SM_Material
    {
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string MaterialTypeGUID { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
