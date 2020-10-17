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
    public class DA_ScreenCategory
    {
        string connectionString = ConnectionString.MyConnection();
        ListFetcher lstFetch = new ListFetcher();

        public List<ScreenCategory> GetAllScreenCategorys()
        {
            List<ScreenCategory> list = new List<ScreenCategory>();
            list = lstFetch.ExcuteObject<ScreenCategory>("[dbo].[ScreenCategory_GetAll]", true).ToList();
            return list;
        }

        public List<ScreenCategory> GetScreenCategorys_Filters(SM_ScreenCategory searchData)
        {
            List<ScreenCategory> list = this.GetAllScreenCategorys();
            list = list.Where(a => (searchData.ScreenCategoryID > 0) ? a.ID == searchData.ScreenCategoryID : true).ToList();
            list = list.ToPagedList(searchData.CurrentPage++, CommonClass.PageSize).ToList();
            return list;
        }
        public int GetAllScreenCategoryCount(SM_ScreenCategory searchData)
        {
            int Count = 0;
            List<ScreenCategory> list = this.GetAllScreenCategorys();
            list = list.Where(a => (searchData.ScreenCategoryID > 0) ? a.ID == searchData.ScreenCategoryID : true).ToList();
            Count = list.Count;
            return Count;
        }

        public ScreenCategory GetAllScreenCategoryByGUID(string GUID)
        {
            ScreenCategory mdl = new ScreenCategory();
            List<ScreenCategory> list = this.GetAllScreenCategorys();
            list = list.Where(a => (!string.IsNullOrEmpty(GUID)) ? a.GUID == GUID : true).ToList();
            if (list != null)
                mdl = list.First();
            return mdl;
        }

        public bool AddScreenCategory(ScreenCategory data)
        {
            bool added = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("ScreenCategory_Add", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@ScreenCategoryName", data.ScreenCategoryName);
            cmd.Parameters.AddWithValue("@Icon", data.Icon);
            cmd.Parameters.AddWithValue("@CreatedDate", data.CreatedDate);
            cmd.Parameters.AddWithValue("@CreatedBy", data.CreatedBy);
            cmd.Parameters.AddWithValue("@UpDatedDate", data.UpDatedDate);
            cmd.Parameters.AddWithValue("@UpDatedBy", data.UpDatedBy);

            cmd.Parameters.AddWithValue("@Status", data.Status);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                added = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                added = false;
            }
            finally
            {
                con.Close();
            }

            return added;
        }
        public bool UpdateScreenCategory(ScreenCategory data)
        {
            bool updated = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("ScreenCategory_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@ScreenCategoryName", data.ScreenCategoryName);
            cmd.Parameters.AddWithValue("@Icon", data.Icon);
            cmd.Parameters.AddWithValue("@UpDatedDate", data.UpDatedDate);
            cmd.Parameters.AddWithValue("@UpDatedBy", data.UpDatedBy);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                updated = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                updated = false;
            }
            finally
            {
                con.Close();
            }
            return updated;
        }
        public bool DeleteScreenCategory(string GUID)
        {
            bool deleted = false;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("ScreenCategory_Delete_Recover", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GUID", GUID);
            cmd.Parameters.AddWithValue("@Status", 0);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                deleted = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                deleted = false;
            }
            finally
            {
                con.Close();
            }

            return deleted;
        }
        public bool RecoverScreenCategory(string GUID)
        {
            bool recovered = false;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("ScreenCategory_Delete_Recover", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GUID", GUID);
            cmd.Parameters.AddWithValue("@Status", 1);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                recovered = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                recovered = false;
            }
            finally
            {
                con.Close();
            }

            return recovered;
        }

    }
}
