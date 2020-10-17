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
    public class RoleController : Controller
    {
        DA_Role da = new DA_Role();
        CommonClass cs = new CommonClass();

        // GET: Role
        public ActionResult RoleList(SM_Role data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                ViewBag.SystemUserType = userInfo.SystemUserType;


                data.TotalPage = cs.TotalPage(da.GetAllRoleCount(data));
                data.TotalCount = da.GetAllRoleCount(data);
                data.CurrentPage = 1;

                ViewBag.ActivePageID = "PageRole";
                return View("RoleList", data);

            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }
            
        }

        public ActionResult RoleListPartial(string getpassdata)
        {
            List<Role> list = new List<Role>();
            if (!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_Role>(getpassdata);
                list = da.GetRoles_Filters(serializeData);
                ViewBag.CurrentPagePartial = serializeData.CurrentPage - 1;
            }
            return PartialView("RoleListPartial", list);
        }

        public ActionResult GetTotalPage(string getpassdata)
        {
            List<Role> list = new List<Role>();
            int TotalPage = 0;
            if (!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_Role>(getpassdata);
                TotalPage = cs.TotalPage(da.GetAllRoleCount(serializeData));
            }
            return Json(TotalPage, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditRoleForm(string GUID)
        {
            Role data = new Role();
            if (!string.IsNullOrEmpty(GUID))
            {
                data = da.GetAllRoleByGUID(GUID);
            }
            return PartialView("EditRoleForm", data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditRole(Role data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                if (!string.IsNullOrEmpty(data.GUID))
                {
                    data.UpDatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    data.UpDatedBy = userInfo.GUID;
                    bool updated = da.UpdateRole(data);
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
                    bool added = da.AddRole(data);
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
        public ActionResult SearchRole(SM_Role mdl)
        {
            if (string.IsNullOrEmpty(mdl.RoleName))
                mdl.RoleID = 0;
            return RedirectToAction("RoleList", mdl);
        }

        public JsonResult DeleteRole(string GUID)
        {
            bool deleted = da.DeleteRole(GUID);
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