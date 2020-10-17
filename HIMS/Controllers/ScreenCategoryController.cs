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
    public class ScreenCategoryController : Controller
    {
        DA_ScreenCategory da = new DA_ScreenCategory();
        CommonClass cs = new CommonClass();

        // GET: ScreenCategory
        public ActionResult ScreenCategoryList(SM_ScreenCategory data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                ViewBag.SystemUserType = userInfo.SystemUserType;
                data.TotalPage = cs.TotalPage(da.GetAllScreenCategoryCount(data));
                data.TotalCount = da.GetAllScreenCategoryCount(data);
                data.CurrentPage = 1;

                ViewBag.ActivePageID = "PageScreenCategory";
                return View("ScreenCategoryList", data);
            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }
            
        }

        public ActionResult ScreenCategoryListPartial(string getpassdata)
        {
            List<ScreenCategory> list = new List<ScreenCategory>();
            if (!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_ScreenCategory>(getpassdata);
                list = da.GetScreenCategorys_Filters(serializeData);
                ViewBag.CurrentPagePartial = serializeData.CurrentPage - 1;
            }
            return PartialView("ScreenCategoryListPartial", list);
        }

        public ActionResult GetTotalPage(string getpassdata)
        {
            List<ScreenCategory> list = new List<ScreenCategory>();
            int TotalPage = 0;
            if (!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_ScreenCategory>(getpassdata);
                TotalPage = cs.TotalPage(da.GetAllScreenCategoryCount(serializeData));
            }
            return Json(TotalPage, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditScreenCategoryForm(string GUID)
        {
            ScreenCategory data = new ScreenCategory();
            if (!string.IsNullOrEmpty(GUID))
            {
                data = da.GetAllScreenCategoryByGUID(GUID);
            }
            return PartialView("EditScreenCategoryForm", data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditScreenCategory(ScreenCategory data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                if (!string.IsNullOrEmpty(data.GUID))
                {
                    data.UpDatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    data.UpDatedBy = userInfo.GUID;
                    bool updated = da.UpdateScreenCategory(data);
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
                    bool added = da.AddScreenCategory(data);
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
        public ActionResult SearchScreenCategory(SM_ScreenCategory mdl)
        {
            if (string.IsNullOrEmpty(mdl.ScreenCategoryName))
                mdl.ScreenCategoryID = 0;
            return RedirectToAction("ScreenCategoryList", mdl);
        }

        public JsonResult DeleteScreenCategory(string GUID)
        {
            bool deleted = da.DeleteScreenCategory(GUID);
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