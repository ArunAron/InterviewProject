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
    public class UOMTypeController : Controller
    {
        DA_UOMType da = new DA_UOMType();
        CommonClass cs = new CommonClass();

        // GET: UOMType
        public ActionResult UOMTypeList(SM_UOMType data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                ViewBag.SystemUserType = userInfo.SystemUserType;
                data.TotalPage = cs.TotalPage(da.GetAllUOMTypeCount(data));
                data.TotalCount = da.GetAllUOMTypeCount(data);
                data.CurrentPage = 1;

                ViewBag.ActivePageID = "PageUOMType";
                return View("UOMTypeList", data);

            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }
            
        }

        public ActionResult UOMTypeListPartial(string getpassdata)
        {
            List<UOMType> list = new List<UOMType>();
            if (!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_UOMType>(getpassdata);
                list = da.GetUOMTypes_Filters(serializeData);
                ViewBag.CurrentPagePartial = serializeData.CurrentPage - 1;
            }
            return PartialView("UOMTypeListPartial", list);
        }

        public ActionResult GetTotalPage(string getpassdata)
        {
            List<UOMType> list = new List<UOMType>();
            int TotalPage = 0;
            if (!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_UOMType>(getpassdata);
                TotalPage = cs.TotalPage(da.GetAllUOMTypeCount(serializeData));
            }
            return Json(TotalPage, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditUOMTypeForm(string GUID)
        {
            UOMType data = new UOMType();
            if (!string.IsNullOrEmpty(GUID))
            {
                data = da.GetAllUOMTypeByGUID(GUID);
            }
            return PartialView("EditUOMTypeForm", data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditUOMType(UOMType data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                if (!string.IsNullOrEmpty(data.GUID))
                {
                    data.UpDatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    data.UpDatedBy = userInfo.GUID;
                    bool updated = da.UpdateUOMType(data);
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
                    bool added = da.AddUOMType(data);
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SearchUOMType(SM_UOMType mdl)
        {
            if (string.IsNullOrEmpty(mdl.Type))
                mdl.UOMTypeID = 0;
            return RedirectToAction("UOMTypeList", mdl);
        }

        public JsonResult DeleteUOMType(string GUID)
        {
            bool deleted = da.DeleteUOMType(GUID);
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