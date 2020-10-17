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
    public class DA_Material
    {
        string connectionString = ConnectionString.MyConnection();
        ListFetcher lstFetch = new ListFetcher();

        public List<Material> GetAllMaterials()
        {
            List<Material> list = new List<Material>();
            list = lstFetch.ExcuteObject<Material>("[dbo].[Material_GetAll]", true).ToList();
            return list;
        }

        public List<Material> GetMaterials_Filters(SM_Material searchData)
        {
            List<Material> list = this.GetAllMaterials();
            list = list.Where(a => (searchData.MaterialID > 0) ? a.ID == searchData.MaterialID : true).Where(a => (searchData.MaterialTypeGUID != null) ? a.MaterialTypeGUID == searchData.MaterialTypeGUID : true).ToList();
            list = list.ToPagedList(searchData.CurrentPage++, CommonClass.PageSize).ToList();
            return list;
        }
        public int GetAllMaterialCount(SM_Material searchData)
        {
            int Count = 0;
            List<Material> list = this.GetAllMaterials();
            list = list.Where(a => (searchData.MaterialID > 0) ? a.ID == searchData.MaterialID : true).Where(a => (searchData.MaterialTypeGUID != null) ? a.MaterialTypeGUID == searchData.MaterialTypeGUID : true).ToList();
            Count = list.Count;
            return Count;
        }

        public Material GetAllMaterialByGUID(string GUID)
        {
            Material mdl = new Material();
            List<Material> list = this.GetAllMaterials();
            list = list.Where(a => (!string.IsNullOrEmpty(GUID)) ? a.GUID == GUID : true).ToList();
            if (list != null)
                mdl = list.First();
            return mdl;
        }

        public bool AddMaterial(Material data)
        {
            bool added = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Material_Add", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@MaterialName", data.MaterialName);
            cmd.Parameters.AddWithValue("@ShortCut", data.ShortCut); 
            cmd.Parameters.AddWithValue("@MaterialTypeGUID", data.MaterialTypeGUID);
            cmd.Parameters.AddWithValue("@Quality", data.Quality);
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
        public bool UpdateMaterial(Material data)
        {
            bool updated = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Material_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@MaterialName", data.MaterialName);
            cmd.Parameters.AddWithValue("@ShortCut", data.ShortCut);
            cmd.Parameters.AddWithValue("@MaterialTypeGUID", data.MaterialTypeGUID);
            cmd.Parameters.AddWithValue("@Quality", data.Quality);
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
        public bool UpdateMaterialUOM(Material data)
        {
            bool updated = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Material_UOM_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GUID", data.GUID);
            cmd.Parameters.AddWithValue("@UOMDescription", data.UOMDescription);
            cmd.Parameters.AddWithValue("@UOMType", data.UOMType);
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
        public bool DeleteMaterial(string GUID)
        {
            bool deleted = false;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Material_Delete_Recover", con);
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
        public bool RecoverMaterial(string GUID)
        {
            bool recovered = false;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Material_Delete_Recover", con);
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
