using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataCore.DA;
using DataCore.Models;
using Microsoft.SqlServer.Server;

namespace HIMS.Controllers
{

    public class LoginController : Controller
    {
        DA_SystemUser da = new DA_SystemUser();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CheckLogin(string UserName, string Password)
        {
            string SystemUserGUID = string.Empty;
            SystemUser loginData = da.CheckLogin(UserName, Password);
            if (loginData.ID > 0)
            {
                Session["UserInfo"] = loginData;
                SystemUserGUID = loginData.GUID;
            }
            else
            {
                SystemUserGUID = "wrong";
            }
            return Json(SystemUserGUID, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EnterSystem()
        {
            string WarehouseGUID = Request.Form["Warehouse"];
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                userInfo.WarehouseGUID = WarehouseGUID;
                userInfo.WarehouseName = new DA_Warehouse().GetAllWarehouseByGUID(WarehouseGUID).WarehouseName;
                Session["UserInfo"] = userInfo;
            }   
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}
