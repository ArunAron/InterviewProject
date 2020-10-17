using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataCore.Models;
using DataCore.DA;
using DataCore;
using DataCore.SearchModel;
using Newtonsoft.Json;

namespace HIMS.Controllers
{
    public class SystemUserWarehouseMapController : Controller
    {
        DA_Role daRole = new DA_Role();
        DA_SystemUserWarehouseMap daRSM = new DA_SystemUserWarehouseMap();
      
        // GET: SystemUserWarehouseMap
        public ActionResult SystemUserWarehouseMapList()
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                ViewBag.SystemUserType = userInfo.SystemUserType;
                ViewBag.ActivePageID = "PageSystemUserWarehouseMap";
                return View();

            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }
            
        }
        public ActionResult SystemUserWarehouseMapListPartial(string SystemUserGUID)
        {
            List<SystemUserWarehouseMap> list = daRSM.GetAllSystemUserWarehouseMap(SystemUserGUID);
            return PartialView("SystemUserWarehouseMapListPartial", list);
        }
        public JsonResult GiveAndTakeAccess(string WarehouseGUID, string SystemUserGUID, string Access)
        {
            bool access = daRSM.GiveAndTakeAccess(WarehouseGUID, SystemUserGUID, Access);
            if (access)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAccessibleWarehouse(string SystemUserGUID)
        {
            List<SystemUserWarehouseMap> list = daRSM.GetAccessibleWarehouse(SystemUserGUID);
            return PartialView("GetAccessibleWarehouse", list);
        }
        public ActionResult GetAccessibleWarehouseModal()
        {
            if(Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                List<SystemUserWarehouseMap> list = daRSM.GetAccessibleWarehouse(userInfo.GUID);
                ViewBag.CurrentWarehouseGUID = userInfo.WarehouseGUID;
                return PartialView("GetAccessibleWarehouseModal", list);

            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }
            
        }
    }
}