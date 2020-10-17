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
    public class ScreenController : Controller
    {
        DA_Screen da = new DA_Screen();
        DA_ScreenCategory daScreenCategory = new DA_ScreenCategory();
        DA_ScreenCategory daMt = new DA_ScreenCategory();
        CommonClass cs = new CommonClass();

        // GET: Screen
        public ActionResult ScreenList(SM_Screen data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                ViewBag.SystemUserType = userInfo.SystemUserType;

                data.TotalPage = cs.TotalPage(da.GetAllScreenCount(data));
                data.TotalCount = da.GetAllScreenCount(data);
                data.CurrentPage = 1;

                List<ScreenCategory> mtList = daMt.GetAllScreenCategorys();
                mtList.Add(new ScreenCategory { GUID = "All", ScreenCategoryName = "All", ID = 0 });
                ViewBag.ScreenCategoryList = mtList.OrderBy(a => a.ID);
                ViewBag.SelectedScreenCategory = data.ScreenCategoryGUID;
                ViewBag.ActivePageID = "PageScreen";
                return View("ScreenList", data);

            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }
            
        }

        public ActionResult ScreenListPartial(string getpassdata)
        {
            List<Screen> list = new List<Screen>();
            List<ScreenCategory> listSC = daScreenCategory.GetAllScreenCategorys();
            if (!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_Screen>(getpassdata);
                list = da.GetScreens_Filters(serializeData);

                foreach (var data in listSC.ToList())
                {
                    data.ScreenList = list.Where(a => a.ScreenCategoryGUID == data.GUID).ToList();
                    if(data.ScreenList.Count == 0)
                    {
                        listSC.Remove(data);
                    }
                }

                ViewBag.CurrentPagePartial = serializeData.CurrentPage - 1;
            }
            return PartialView("ScreenListPartial", listSC);
        }

        public ActionResult GetTotalPage(string getpassdata)
        {
            List<Screen> list = new List<Screen>();
            int TotalPage = 0;
            if (!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_Screen>(getpassdata);
                TotalPage = cs.TotalPage(da.GetAllScreenCount(serializeData));
            }
            return Json(TotalPage, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditScreenForm(string GUID)
        {
            Screen data = new Screen();
            ViewBag.ScreenCategoryList = daMt.GetAllScreenCategorys();
            if (!string.IsNullOrEmpty(GUID))
            {
                data = da.GetAllScreenByGUID(GUID);
                ViewBag.SelectedScreenCategory = data.ScreenCategoryGUID;
            }
            return PartialView("EditScreenForm", data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditScreen(Screen data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                data.ScreenCategoryGUID = Request.Form["ScreenCategory"];
                if (!string.IsNullOrEmpty(data.GUID))
                {
                    data.UpDatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    data.UpDatedBy = userInfo.GUID;
                    bool updated = da.UpdateScreen(data);
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
                    bool added = da.AddScreen(data);
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
        public ActionResult SearchScreen(SM_Screen mdl)
        {
            mdl.ScreenCategoryGUID = Request.Form["SearchScreenCategory"];
            if (string.IsNullOrEmpty(mdl.ScreenName))
                mdl.ScreenID = 0;

            if (mdl.ScreenCategoryGUID == "All")
                mdl.ScreenCategoryGUID = null;

            return RedirectToAction("ScreenList", mdl);
        }

        public JsonResult DeleteScreen(string GUID)
        {
            bool deleted = da.DeleteScreen(GUID);
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