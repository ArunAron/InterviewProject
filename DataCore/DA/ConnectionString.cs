using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.DA
{
    public class ConnectionString
    {


        public static string MyConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myconnection"].ToString();
            return connectionString;
        }

    }
}

