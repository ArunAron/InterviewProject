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
    public class DA_Role
    {
        string connectionString = ConnectionString.MyConnection();
        ListFetcher lstFetch = new ListFetcher();

        public List<Role> GetAllRoles()
        {
            List<Role> list = new List<Role>();
            list = lstFetch.ExcuteObject<Role>("[dbo].[Role_GetAll]", true).ToList();
            return list;
        }

        public List<Role> GetRoles_Filters(SM_Role searchData)
        {
            List<Role> list = this.GetAllRoles();
            list = list.Where(a => (searchData.RoleID > 0) ? a.ID == searchData.RoleID : true).ToList();
            list = list.ToPagedList(searchData.CurrentPage++, CommonClass.PageSize).ToList();
            return list;
        }
        public int GetAllRoleCount(SM_Role searchData)
        {
            int Count = 0;
            List<Role> list = this.GetAllRoles();
            list = list.Where(a => (searchData.RoleID > 0) ? a.ID == searchData.RoleID : true).ToList();
            Count = list.Count;
            return Count;
        }

        public Role GetAllRoleByGUID(string GUID)
        {
            Role mdl = new Role();
            List<Role> list = this.GetAllRoles();
            list = list.Where(a => (!string.IsNullOrEmpty(GUID)) ? a.GUID == GUID : true).ToList();
            if (list != null)
                mdl = list.First();
            return mdl;
        }

        public bool AddRole(Role data)
        {
            bool added = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Role_Add", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@RoleName", data.RoleName);
            cmd.Parameters.AddWithValue("@Description", data.Description);
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
        public bool UpdateRole(Role data)
        {
            bool updated = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Role_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@RoleName", data.RoleName);
            cmd.Parameters.AddWithValue("@Description", data.Description);
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
        public bool DeleteRole(string GUID)
        {
            bool deleted = false;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Role_Delete_Recover", con);
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
        public bool RecoverRole(string GUID)
        {
            bool recovered = false;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Role_Delete_Recover", con);
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
