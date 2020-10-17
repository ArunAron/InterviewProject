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
    public class DA_SystemUserWarehouseMap
    {
        string connectionString = ConnectionString.MyConnection();
        ListFetcher lstFetch = new ListFetcher();

        public List<SystemUserWarehouseMap> GetAllSystemUserWarehouseMap(string SystemUserGUID)
        {
            List<SystemUserWarehouseMap> list = new List<SystemUserWarehouseMap>();
            list = lstFetch.ExcuteObject<SystemUserWarehouseMap>("[dbo].[SystemUserWarehouseMap_GetAll]", true , "@SystemUserGUID", SystemUserGUID).ToList();
            return list;
        }

        public List<SystemUserWarehouseMap> GetAccessibleWarehouse(string SystemUserGUID)
        {
            List<SystemUserWarehouseMap> list = new List<SystemUserWarehouseMap>();
            list = lstFetch.ExcuteObject<SystemUserWarehouseMap>("[dbo].[SystemUserWarehouseMap_GetAccessible]", true, "@SystemUserGUID", SystemUserGUID).ToList();
            return list;
        }
        public bool GiveAndTakeAccess(string WarehouseGUID, string SystemUserGUID, string Access)
        {
            bool access = false;
            List<SystemUserWarehouseMap> list = new List<SystemUserWarehouseMap>();
            SystemUserWarehouseMap model = new SystemUserWarehouseMap();
            list = lstFetch.ExcuteObject<SystemUserWarehouseMap>("[dbo].[SystemUserWarehouseMap_GetAllReal]", true).ToList();
            list = list.Where(a => a.SystemUserGUID == SystemUserGUID && a.WarehouseGUID == WarehouseGUID).ToList();
            if(list.Count > 0)
            {
                model = list.FirstOrDefault();
                model.AllowAccess = Convert.ToInt32(Access);
                model.AllowAction = 1;
                this.UpdateSystemUserWarehouseMap(model);
                 access = true;
            }
            else
            {
                model.GUID = Guid.NewGuid().ToString();
                model.SystemUserGUID = SystemUserGUID;
                model.WarehouseGUID = WarehouseGUID;
                model.AllowAction = 1;
                model.AllowAccess = Convert.ToInt32(Access);
                this.AddSystemUserWarehouseMap(model);
                access = true;
            }
            return access;
        }

        public bool AddSystemUserWarehouseMap(SystemUserWarehouseMap data)
        {
            bool added = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SystemUserWarehouseMap_Add", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@SystemUserGUID", data.SystemUserGUID);
            cmd.Parameters.AddWithValue("@WarehouseGUID", data.WarehouseGUID);
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
        public bool UpdateSystemUserWarehouseMap(SystemUserWarehouseMap data)
        {
            bool updated = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SystemUserWarehouseMap_Update", con);
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
