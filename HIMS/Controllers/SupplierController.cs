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
    public class SupplierController : Controller
    {
        DA_Supplier da = new DA_Supplier();
        CommonClass cs = new CommonClass();

        // GET: Supplier
        public ActionResult SupplierList()
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                ViewBag.SystemUserType = userInfo.SystemUserType;
                ViewBag.ActivePageID = "PageSupplier";
                return View("SupplierList");
            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }
           
        }

        public ActionResult SupplierListPartial()
        {
            List<Supplier> list = new List<Supplier>();
            list = da.GetAllSuppliers();
            return PartialView("SupplierListPartial", list);
        }

        public ActionResult EditSupplierForm(string GUID)
        {
            Supplier data = new Supplier();
            if (!string.IsNullOrEmpty(GUID))
            {
                data = da.GetAllSupplierByGUID(GUID);
            }
            return PartialView("EditSupplierForm", data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditSupplier(Supplier data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                if (!string.IsNullOrEmpty(data.GUID))
                {
                    data.UpDatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    data.UpDatedBy = userInfo.GUID;
                    bool updated = da.UpdateSupplier(data);
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
                    bool added = da.AddSupplier(data);
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

        public JsonResult DeleteSupplier(string GUID)
        {
            bool deleted = da.DeleteSupplier(GUID);
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