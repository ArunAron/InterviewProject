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

namespace DataCore.DA
{
    public class DA_RoleScreenMap
    {
        string connectionString = ConnectionString.MyConnection();
        ListFetcher lstFetch = new ListFetcher();

        public List<RoleScreenMap> GetAllRoleScreenMap(string RoleGUID)
        {
            List<RoleScreenMap> list = new List<RoleScreenMap>();
            list = lstFetch.ExcuteObject<RoleScreenMap>("[dbo].[RoleScreenMap_GetAll]", true , "@RoleGUID" , RoleGUID).ToList();
            return list;
        }
        public bool GiveAndTakeAccess(string ScreenGUID, string RoleGUID, string Access)
        {
            bool access = false;
            List<RoleScreenMap> list = new List<RoleScreenMap>();
            RoleScreenMap model = new RoleScreenMap();
            list = lstFetch.ExcuteObject<RoleScreenMap>("[dbo].[RoleScreenMap_GetAllReal]", true).ToList();
            list = list.Where(a => a.ScreenGUID == ScreenGUID && a.RoleGUID == RoleGUID).ToList();
            if(list.Count > 0)
            {
                model = list.FirstOrDefault();
                model.AllowAccess = Convert.ToInt32(Access);
                model.AllowAction = 1;
                this.UpdateRoleScreenMap(model);
                 access = true;
            }
            else
            {
                model.GUID = Guid.NewGuid().ToString();
                model.RoleGUID = RoleGUID;
                model.ScreenGUID = ScreenGUID;
                model.AllowAction = 1;
                model.AllowAccess = Convert.ToInt32(Access);
                this.AddRoleScreenMap(model);
                access = true;
            }
            return access;
        }

        public bool AddRoleScreenMap(RoleScreenMap data)
        {
            bool added = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("RoleScreenMap_Add", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@ScreenGUID", data.ScreenGUID);
            cmd.Parameters.AddWithValue("@RoleGUID", data.RoleGUID);
            cmd.Parameters.AddWithValue("@AllowAccess", data.AllowAccess);
            cmd.Parameters.AddWithValue("@AllowAction", data.AllowAction);
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
        public bool UpdateRoleScreenMap(RoleScreenMap data)
        {
            bool updated = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("RoleScreenMap_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@AllowAccess", data.AllowAccess);
            cmd.Parameters.AddWithValue("@AllowAction", data.AllowAction);

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
    }
}
