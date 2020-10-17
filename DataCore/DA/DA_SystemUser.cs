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
    public class DA_SystemUser
    {
        string connectionString = ConnectionString.MyConnection();
        ListFetcher lstFetch = new ListFetcher();

        public List<SystemUser> GetAllSystemUsers()
        {
            List<SystemUser> list = new List<SystemUser>();
            list = lstFetch.ExcuteObject<SystemUser>("[dbo].[SystemUser_GetAll]", true).ToList();
            return list;
        }

        public SystemUser GetAllSystemUserByGUID(string GUID)
        {
            SystemUser mdl = new SystemUser();
            List<SystemUser> list = this.GetAllSystemUsers();
            list = list.Where(a => (!string.IsNullOrEmpty(GUID)) ? a.GUID == GUID : true).ToList();
            if (list != null)
                mdl = list.First();
            return mdl;
        }

        public SystemUser CheckLogin(string UserName, string Password)
        {
            SystemUser mdl = new SystemUser();
            List<SystemUser> list = this.GetAllSystemUsers();
            list = list.Where(a => a.SystemUserName == UserName && a.Password == Password).ToList();
            if (list != null && list.Count > 0)
                mdl = list.First();
            return mdl;
        }


        public List<SystemUser> GetSystemUsers_Filters(SM_SystemUser searchData)
        {
            List<SystemUser> list = this.GetAllSystemUsers();
            list = list.Where(a => (searchData.SystemUserID > 0) ? a.ID == searchData.SystemUserID : true)
                .Where(a => (searchData.RoleGUID != null) ? a.RoleGUID == searchData.RoleGUID : true)
                .Where(a => (searchData.DepartmentGUID != null) ? a.DepartmentGUID == searchData.DepartmentGUID : true).ToList();
            list = list.ToPagedList(searchData.CurrentPage++, CommonClass.PageSize).ToList();
            return list;
        }
        public int GetAllSystemUserCount(SM_SystemUser searchData)
        {
            int Count = 0;
            List<SystemUser> list = this.GetAllSystemUsers();
            list = list.Where(a => (searchData.SystemUserID > 0) ? a.ID == searchData.SystemUserID : true)
                .Where(a => (searchData.RoleGUID != null) ? a.RoleGUID == searchData.RoleGUID : true)
                .Where(a => (searchData.DepartmentGUID != null) ? a.DepartmentGUID == searchData.DepartmentGUID : true).ToList();
            Count = list.Count;
            return Count;
        }

        public bool AddSystemUser(SystemUser data)
        {
            bool added = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SystemUser_Add", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@SystemUserName", data.SystemUserName);
            cmd.Parameters.AddWithValue("@SystemUserType", data.SystemUserType);
            cmd.Parameters.AddWithValue("@FullName", data.FullName);
            cmd.Parameters.AddWithValue("@Password", data.Password);
            cmd.Parameters.AddWithValue("@RoleGUID", data.RoleGUID);
            cmd.Parameters.AddWithValue("@DepartmentGUID", data.DepartmentGUID);
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
        public bool UpdateSystemUser(SystemUser data)
        {
            bool updated = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SystemUser_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@SystemUserName", data.SystemUserName);
            cmd.Parameters.AddWithValue("@SystemUserType", data.SystemUserType);
            cmd.Parameters.AddWithValue("@FullName", data.FullName);
            cmd.Parameters.AddWithValue("@Password", data.Password);
            cmd.Parameters.AddWithValue("@RoleGUID", data.RoleGUID);
            cmd.Parameters.AddWithValue("@DepartmentGUID", data.DepartmentGUID);
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
        public bool DeleteSystemUser(string GUID)
        {
            bool deleted = false;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SystemUser_Delete_Recover", con);
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
        public bool RecoverSystemUser(string GUID)
        {
            bool recovered = false;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SystemUser_Delete_Recover", con);
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
