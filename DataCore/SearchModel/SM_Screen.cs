using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.SearchModel
{
    public class SM_Screen
    {
        public int ScreenID { get; set; }
        public string ScreenName { get; set; }
        public string ScreenCategoryGUID { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
