using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore
{
    public class CommonClass
    {
        public static int PageSize = 5;
        
        public static string NulltoEmptyString(string data)
        {
            if (string.IsNullOrEmpty(data))
                data = "";
            return data;
        }

        public int TotalPage(int totalCount)
        {
            int Total = 0;
            Total = totalCount;
            float Pages_Count = Total / PageSize;
            float pages_count_mod = Total % PageSize;
            if (pages_count_mod != 0)
            {
                Pages_Count++;

            }
            Total = Convert.ToInt32(Pages_Count);
            return Total;
        }

        public List<string> ApprovalStatusList()
        {
            List<string> lst = new List<string>();
            lst.Add("ALL");
            lst.Add("PENDING");
            lst.Add("APPROVED");
            lst.Add("REJECTED");
            return lst;
        }
    }
}
