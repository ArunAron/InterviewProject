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
    public class RoleScreenMapController : Controller
    {
        DA_Role daRole = new DA_Role();
        DA_RoleScreenMap daRSM = new DA_RoleScreenMap();
        DA_ScreenCategory daScreenCategory = new DA_ScreenCategory();
        // GET: RoleScreenMap
        public ActionResult RoleScreenMapList()
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                List<Role> RoleList = daRole.GetAllRoles();
                RoleList.Add(new Role { GUID = "All", RoleName = "Select Role", ID = 0 });
                RoleList = RoleList.OrderBy(a => a.ID).ToList();
                ViewBag.RoleList = RoleList;
                ViewBag.ActivePageID = "PageRoleScreenMap";
                return View();
            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }
            
        }
        public ActionResult RoleScreenMapListPartial(string RoleGUID)
        {
            List<RoleScreenMap> RoleList = daRSM.GetAllRoleScreenMap(RoleGUID);
            List<ScreenCategory> listSC = daScreenCategory.GetAllScreenCategorys().OrderBy(a=>a.ScreenCategoryName).ToList();
            foreach (var data in listSC.ToList())
            {
                data.RoleScreenMapList = RoleList.Where(a => a.ScreenCategoryGUID == data.GUID).ToList();
                if (data.RoleScreenMapList.Count == 0)
                {
                    listSC.Remove(data);
                }
            }
            return PartialView(listSC);
        }
        public JsonResult GiveAndTakeAccess(string ScreenGUID, string RoleGUID , string Access)
        {
            bool access = daRSM.GiveAndTakeAccess(ScreenGUID,  RoleGUID,  Access);
            if (access)
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