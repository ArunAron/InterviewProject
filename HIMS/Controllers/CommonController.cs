using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataCore.Models;
using DataCore.DA;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace HIMS.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
       
        public ActionResult Pagination(int Page, int Count)
        {
            ViewBag.Page = Page;
            ViewBag.Count = Count;
            return PartialView();
        }

        [HttpPost]
        public JsonResult GetMaterialTypeName(string Prefix)
        {
            //Note : you can bind same list from database  
            List<MaterialType> ObjList = new List<MaterialType>();
            DA_MaterialType daMT = new DA_MaterialType();
            ObjList = daMT.GetAllMaterialTypes();
            //Searching records from list using LINQ query  
            var CityList = (from N in ObjList
                            where N.MaterialTypeName.ToLower().Contains(Prefix.ToLower())
                            select new { N.MaterialTypeName, N.ID });
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetRoleName(string Prefix)
        {
            //Note : you can bind same list from database  
            List<Role> ObjList = new List<Role>();
            DA_Role daMT = new DA_Role();
            ObjList = daMT.GetAllRoles();
            //Searching records from list using LINQ query  
            var CityList = (from N in ObjList
                            where N.RoleName.ToLower().Contains(Prefix.ToLower())
                            select new { N.RoleName, N.ID });
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetMaterialName(string Prefix)
        {
            //Note : you can bind same list from database  
            List<Material> ObjList = new List<Material>();
            DA_Material daMT = new DA_Material();
            ObjList = daMT.GetAllMaterials();
            //Searching records from list using LINQ query  
            var CityList = (from N in ObjList
                            where N.MaterialName.ToLower().Contains(Prefix.ToLower())
                            select new { N.MaterialName, N.ID });
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDepartmentName(string Prefix)
        {
            //Note : you can bind same list from database  
            List<Department> ObjList = new List<Department>();
            DA_Department daMT = new DA_Department();
            ObjList = daMT.GetAllDepartments();
            //Searching records from list using LINQ query  
            var CityList = (from N in ObjList
                            where N.DepartmentName.ToLower().Contains(Prefix.ToLower())
                            select new { N.DepartmentName, N.ID });
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetMaterialNameReturnGUID(string Prefix)
        {
            //Note : you can bind same list from database  
            List<Material> ObjList = new List<Material>();
            DA_Material daMT = new DA_Material();
            ObjList = daMT.GetAllMaterials();
            //Searching records from list using LINQ query  
            var CityList = (from N in ObjList
                            where N.MaterialName.ToLower().Contains(Prefix.ToLower())
                            select new { N.MaterialName, N.GUID });
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSystemUserName(string Prefix)
        {
            //Note : you can bind same list from database  
            List<SystemUser> ObjList = new List<SystemUser>();
            DA_SystemUser daMT = new DA_SystemUser();
            ObjList = daMT.GetAllSystemUsers();
            //Searching records from list using LINQ query  
            var CityList = (from N in ObjList
                            where N.SystemUserName.ToLower().Contains(Prefix.ToLower())
                            select new { N.SystemUserName, N.ID });
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSystemUserNameReturnGUID(string Prefix)
        {
            //Note : you can bind same list from database  
            List<SystemUser> ObjList = new List<SystemUser>();
            DA_SystemUser daMT = new DA_SystemUser();
            ObjList = daMT.GetAllSystemUsers();
            //Searching records from list using LINQ query  
            var CityList = (from N in ObjList
                            where N.SystemUserName.ToLower().Contains(Prefix.ToLower())
                            select new { N.SystemUserName, N.GUID });
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetScreenName(string Prefix)
        {
            //Note : you can bind same list from database  
            List<Screen> ObjList = new List<Screen>();
            DA_Screen daMT = new DA_Screen();
            ObjList = daMT.GetAllScreens();
            //Searching records from list using LINQ query  
            var CityList = (from N in ObjList
                            where N.ScreenName.ToLower().Contains(Prefix.ToLower())
                            select new { N.ScreenName, N.ID });
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUOMTypeName(string Prefix)
        {
            //Note : you can bind same list from database  
            List<UOMType> ObjList = new List<UOMType>();
            DA_UOMType daMT = new DA_UOMType();
            ObjList = daMT.GetAllUOMTypes();
            //Searching records from list using LINQ query  
            var CityList = (from N in ObjList
                            where N.Type.ToLower().Contains(Prefix.ToLower())
                            select new { N.Type, N.ID });
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }

        // OLD Login , Get UOM For Specific Material
        //[HttpPost]
        //public JsonResult GetUOMByMaterialGUID(string Prefix)
        //{
        //    //Note : you can bind same list from database  
        //    List<UOM> ObjList = new List<UOM>();
        //    DA_UOM daMT = new DA_UOM();
        //    ObjList = daMT.GetAllUOMs();
        //    //Searching records from list using LINQ query  

        //    var CityList = (from N in ObjList
        //                    where N.MaterialGUID == Prefix orderby N.Type
        //                    select new { N.Type, N.ID });
        //    return Json(CityList, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult GetUOMByMaterialGUID(string Prefix)
        {
            //Note : you can bind same list from database  
            List<UOMType> ObjList = new List<UOMType>();
            DA_UOMType daMT = new DA_UOMType();
            ObjList = daMT.GetAllUOMTypes();
            //Searching records from list using LINQ query  

            var CityList = (from N in ObjList
                            orderby N.Type
                            select new { N.Type, N.ID });
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }

        

        public JsonResult GetSupplierNameReturnGUID(string Prefix)
        {
            //Note : you can bind same list from database  
            List<Supplier> ObjList = new List<Supplier>();
            DA_Supplier daMT = new DA_Supplier();
            ObjList = daMT.GetAllSuppliers();
            //Searching records from list using LINQ query  
            var CityList = (from N in ObjList
                            where N.SupplierName.ToLower().Contains(Prefix.ToLower())
                            select new { N.SupplierName, N.GUID });
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetScreenCategoryIDByScreenGUID(string Prefix)
        {
            //Note : you can bind same list from database  
            DA_Screen dA_Screen = new DA_Screen();
            Screen sc = dA_Screen.GetAllScreenByPageID(Prefix);
            return Json(sc.ScreenCategoryGUID, JsonRequestBehavior.AllowGet);
        }
    }
}