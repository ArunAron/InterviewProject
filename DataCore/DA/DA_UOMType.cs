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
    public class DA_UOMType
    {
        string connectionString = ConnectionString.MyConnection();
        ListFetcher lstFetch = new ListFetcher();

        public List<UOMType> GetAllUOMTypes()
        {
            List<UOMType> list = new List<UOMType>();
            list = lstFetch.ExcuteObject<UOMType>("[dbo].[UOMType_GetAll]", true).ToList();
            return list;
        }

        public UOMType GetAllUOMTypeByGUID(string GUID)
        {
            UOMType mdl = new UOMType();
            List<UOMType> list = this.GetAllUOMTypes();
            list = list.Where(a => (!string.IsNullOrEmpty(GUID)) ? a.GUID == GUID : true).ToList();
            if (list != null)
                mdl = list.First();
            return mdl;
        }

        public List<UOMType> GetUOMTypes_Filters(SM_UOMType searchData)
        {
            List<UOMType> list = this.GetAllUOMTypes();
            list = list.Where(a => (searchData.UOMTypeID > 0) ? a.ID == searchData.UOMTypeID : true).ToList();
            list = list.ToPagedList(searchData.CurrentPage++, CommonClass.PageSize).ToList();
            return list;
        }
        public int GetAllUOMTypeCount(SM_UOMType searchData)
        {
            int Count = 0;
            List<UOMType> list = this.GetAllUOMTypes();
            list = list.Where(a => (searchData.UOMTypeID > 0) ? a.ID == searchData.UOMTypeID : true).ToList();
            Count = list.Count;
            return Count;
        }

        public bool AddUOMType(UOMType data)
        {
            bool added = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UOMType_Add", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@Type", data.Type);
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
        public bool UpdateUOMType(UOMType data)
        {
            bool updated = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UOMType_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@Type", data.Type);
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
        public bool DeleteUOMType(string GUID)
        {
            bool deleted = false;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UOMType_Delete_Recover", con);
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
        public bool RecoverUOMType(string GUID)
        {
            bool recovered = false;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UOMType_Delete_Recover", con);
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
