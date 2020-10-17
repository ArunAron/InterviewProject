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
    public class SystemUserController : Controller
    {
        DA_SystemUser da = new DA_SystemUser();
        DA_Role daRole = new DA_Role();
        DA_Department daDepartment = new DA_Department();
        CommonClass cs = new CommonClass();

        // GET: SystemUser
        public ActionResult SystemUserList(SM_SystemUser data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                ViewBag.SystemUserType = userInfo.SystemUserType;
                data.TotalPage = cs.TotalPage(da.GetAllSystemUserCount(data));
                data.TotalCount = da.GetAllSystemUserCount(data);
                data.CurrentPage = 1;

                List<Role> roleList = daRole.GetAllRoles();
                roleList.Add(new Role { GUID = "All", RoleName = "All", ID = 0 });
                ViewBag.RoleList = roleList.OrderBy(a => a.ID);
                ViewBag.SelectedRole = data.RoleGUID;

                List<Department> DepartmentList = daDepartment.GetAllDepartments();
                DepartmentList.Add(new Department { GUID = "All", DepartmentName = "All", ID = 0 });
                ViewBag.DepartmentList = DepartmentList.OrderBy(a => a.ID);
                ViewBag.SelectedDepartment = data.DepartmentGUID;

                ViewBag.ActivePageID = "PageSystemUser";
                return View("SystemUserList", data);

            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }

        }

        public ActionResult SystemUserListPartial(string getpassdata)
        {
            List<SystemUser> list = new List<SystemUser>();
            if (!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_SystemUser>(getpassdata);
                list = da.GetSystemUsers_Filters(serializeData);
                ViewBag.CurrentPagePartial = serializeData.CurrentPage - 1;
            }
            return PartialView("SystemUserListPartial", list);
        }

        public ActionResult GetTotalPage(string getpassdata)
        {
            List<SystemUser> list = new List<SystemUser>();
            int TotalPage = 0;
            if (!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_SystemUser>(getpassdata);
                TotalPage = cs.TotalPage(da.GetAllSystemUserCount(serializeData));
            }
            return Json(TotalPage, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditSystemUserForm(string GUID)
        {
            SystemUser data = new SystemUser();
            ViewBag.RoleList = daRole.GetAllRoles();
            ViewBag.DepartmentList = daDepartment.GetAllDepartments();

            if (!string.IsNullOrEmpty(GUID))
            {
                data = da.GetAllSystemUserByGUID(GUID);
                ViewBag.SelectedRole = data.RoleGUID;
                ViewBag.SelectedDepartment = data.DepartmentGUID;
            }
            return PartialView("EditSystemUserForm", data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditSystemUser(SystemUser data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                data.RoleGUID = Request.Form["Role"];
                data.DepartmentGUID = Request.Form["Department"];
                data.SystemUserType = Request.Form["SystemUserType"];
                if (!string.IsNullOrEmpty(data.GUID))
                {
                    data.UpDatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    data.UpDatedBy = userInfo.GUID;
                    bool updated = da.UpdateSystemUser(data);
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
                    bool added = da.AddSystemUser(data);
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
        public ActionResult SearchSystemUser(SM_SystemUser mdl)
        {
            mdl.RoleGUID = Request.Form["Role"];
            mdl.DepartmentGUID = Request.Form["Department"];

            if (string.IsNullOrEmpty(mdl.SystemUserName))
                mdl.SystemUserID = 0;

            if (mdl.RoleGUID == "All")
                mdl.RoleGUID = null;

            if (mdl.DepartmentGUID == "All")
                mdl.DepartmentGUID = null;

            return RedirectToAction("SystemUserList", mdl);
        }

        public JsonResult DeleteSystemUser(string GUID)
        {
            bool deleted = da.DeleteSystemUser(GUID);
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