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
    public class WarehouseController : Controller
    {
        // GET: Warehouse
        DA_Warehouse da = new DA_Warehouse();
        CommonClass cs = new CommonClass();

        // GET: Warehouse
        public ActionResult WarehouseList()
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                ViewBag.SystemUserType = userInfo.SystemUserType;
                ViewBag.ActivePageID = "PageWarehouse";
                return View("WarehouseList");

            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }
           
        }

        public ActionResult WarehouseListPartial()
        {
            List<Warehouse> list = new List<Warehouse>();
            list = da.GetAllWarehouses();
            return PartialView("WarehouseListPartial", list);
        }

        

        public ActionResult EditWarehouseForm(string GUID)
        {
            Warehouse data = new Warehouse();
            if (!string.IsNullOrEmpty(GUID))
            {
                data = da.GetAllWarehouseByGUID(GUID);
            }
            return PartialView("EditWarehouseForm", data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditWarehouse(Warehouse data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                if (!string.IsNullOrEmpty(data.GUID))
                {
                    data.UpDatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    data.UpDatedBy = userInfo.GUID;
                    bool updated = da.UpdateWarehouse(data);
                    if (updated)
                    {
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Fail", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    data.GUID = Guid.NewGuid().ToString();
                    data.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    data.CreatedBy = userInfo.GUID;
                    data.UpDatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    data.UpDatedBy = userInfo.GUID;
                    data.Status = 1;
                    bool added = da.AddWarehouse(data);
                    if (added)
                    {
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Fail", JsonRequestBehavior.AllowGet);
                    }
                }

            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }
            
        }

        public JsonResult DeleteWarehouse(string GUID)
        {
            bool deleted = da.DeleteWarehouse(GUID);
            if (deleted)
            {
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }
    }
}