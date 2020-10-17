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
    public class HomeController : Controller
    {
        DA_Screen daScreen = new DA_Screen();
        DA_ScreenCategory daScreenCategory = new DA_ScreenCategory();
        CommonClass cs = new CommonClass();
        public ActionResult Index()
        {
            if (Session["UserInfo"] != null)
            {
                ViewBag.ActivePageID = "PageDashBoard";
                return View();
            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MenuTabsPartial(string ActivePageID)
        {
           
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                List<ScreenCategory> listSC = daScreenCategory.GetAllScreenCategorys();
                List<Screen> list = daScreen.GetRoleMapScreen(userInfo.RoleGUID);
                ViewBag.WarehouseGUID = userInfo.WarehouseGUID;
                ViewBag.WarehouseName = userInfo.WarehouseName;
                ViewBag.UserFullName = userInfo.FullName;
                ViewBag.ActivePageID = ActivePageID;

                foreach (var data in listSC)
                {
                    data.ScreenList = list.Where(a => a.ScreenCategoryGUID == data.GUID).OrderBy(a=>a.ScreenName).ToList();
                }

                return PartialView("MenuTabsPartial", listSC);
            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }

           
        }

        public JsonResult ChooseActiveWarehouse(string WarehouseGUID)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                userInfo.WarehouseGUID = WarehouseGUID;
                userInfo.WarehouseName = new DA_Warehouse().GetAllWarehouseByGUID(WarehouseGUID).WarehouseName;
                Session["UserInfo"] = userInfo;
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}