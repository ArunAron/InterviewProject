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
    public class DA_Screen
    {
        string connectionString = ConnectionString.MyConnection();
        ListFetcher lstFetch = new ListFetcher();

        public List<Screen> GetAllScreens()
        {
            List<Screen> list = new List<Screen>();
            list = lstFetch.ExcuteObject<Screen>("[dbo].[Screen_GetAll]", true).ToList();
            return list;
        }
       
        public List<Screen> GetRoleMapScreen(string RoleGUID)
        {
            List<Screen> list = new List<Screen>();
            list = lstFetch.ExcuteObject<Screen>("[dbo].[Screen_GetRoleMapScreen]", true, "@RoleGUID", RoleGUID).ToList();
            return list;
        }

        public List<Screen> GetScreens_Filters(SM_Screen searchData)
        {
            List<Screen> list = this.GetAllScreens();
            list = list.Where(a => (searchData.ScreenID > 0) ? a.ID == searchData.ScreenID : true).Where(a => (searchData.ScreenCategoryGUID != null) ? a.ScreenCategoryGUID == searchData.ScreenCategoryGUID : true).ToList();
            //list = list.ToPagedList(searchData.CurrentPage++, CommonClass.PageSize).ToList();
            return list;
        }
        
        public int GetAllScreenCount(SM_Screen searchData)
        {
            int Count = 0;
            List<Screen> list = this.GetAllScreens();
            list = list.Where(a => (searchData.ScreenID > 0) ? a.ID == searchData.ScreenID : true).Where(a => (searchData.ScreenCategoryGUID != null) ? a.ScreenCategoryGUID == searchData.ScreenCategoryGUID : true).ToList();
            Count = list.Count;
            return Count;
        }

        public Screen GetAllScreenByGUID(string GUID)
        {
            Screen mdl = new Screen();
            List<Screen> list = this.GetAllScreens();
            list = list.Where(a => (!string.IsNullOrEmpty(GUID)) ? a.GUID == GUID : true).ToList();
            if (list != null)
                mdl = list.First();
            return mdl;
        }

        public Screen GetAllScreenByPageID(string PageID)
        {
            Screen mdl = new Screen();
            List<Screen> list = this.GetAllScreens();
            list = list.Where(a => (!string.IsNullOrEmpty(PageID)) ? a.ScreenUniqueName == PageID : true).ToList();
            if (list != null)
                mdl = list.First();
            return mdl;
        }

        public bool AddScreen(Screen data)
        {
            bool added = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Screen_Add", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@ScreenName", data.ScreenName);
            cmd.Parameters.AddWithValue("@ScreenUniqueName", data.ScreenUniqueName);
            cmd.Parameters.AddWithValue("@ScreenCategoryGUID", data.ScreenCategoryGUID);
            cmd.Parameters.AddWithValue("@Controller", data.Controller);
            cmd.Parameters.AddWithValue("@Action", data.Action);
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
        public bool UpdateScreen(Screen data)
        {
            bool updated = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Screen_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@ScreenName", data.ScreenName);
            cmd.Parameters.AddWithValue("@ScreenUniqueName", data.ScreenUniqueName);
            cmd.Parameters.AddWithValue("@ScreenCategoryGUID", data.ScreenCategoryGUID);
            cmd.Parameters.AddWithValue("@Controller", data.Controller);
            cmd.Parameters.AddWithValue("@Action", data.Action);
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
        public bool DeleteScreen(string GUID)
        {
            bool deleted = false;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Screen_Delete_Recover", con);
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
        public bool RecoverScreen(string GUID)
        {
            bool recovered = false;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Screen_Delete_Recover", con);
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
