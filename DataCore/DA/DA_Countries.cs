using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using DataCore.Models;
using DataCore.SearchModel;
using PagedList;
namespace DataCore.DA
{
    public class DA_Countries
    {
        string connectionString = ConnectionString.MyConnection();
        ListFetcher lstFetch = new ListFetcher();

        public List<Countries> GetAllCountries()
        {
            List<Countries> list = new List<Countries>();
            list = lstFetch.ExcuteObject<Countries>("[dbo].[Countries_GetAll]", true).ToList();
            return list;
        }
    }
}
