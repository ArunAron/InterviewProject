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
    public class DepartmentController : Controller
    {
        DA_Department da = new DA_Department();
        CommonClass cs = new CommonClass();

        // GET: Department
        public ActionResult DepartmentList(SM_Department data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                ViewBag.SystemUserType = userInfo.SystemUserType;


                data.TotalPage = cs.TotalPage(da.GetAllDepartmentCount(data));
                data.TotalCount = da.GetAllDepartmentCount(data);
                data.CurrentPage = 1;

                ViewBag.ActivePageID = "PageDepartment";
                return View("DepartmentList", data);

            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }
        }

        public ActionResult DepartmentListPartial(string getpassdata)
        {
            List<Department> list = new List<Department>();
            if (!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_Department>(getpassdata);
                list = da.GetDepartments_Filters(serializeData);
                ViewBag.CurrentPagePartial = serializeData.CurrentPage - 1;
            }
            return PartialView("DepartmentListPartial", list);
        }

        public ActionResult GetTotalPage(string getpassdata)
        {
            List<Department> list = new List<Department>();
            int TotalPage = 0;
            if (!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_Department>(getpassdata);
                TotalPage = cs.TotalPage(da.GetAllDepartmentCount(serializeData));
            }
            return Json(TotalPage, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditDepartmentForm(string GUID)
        {
            Department data = new Department();
            if (!string.IsNullOrEmpty(GUID))
            {
                data = da.GetAllDepartmentByGUID(GUID);
            }
            return PartialView("EditDepartmentForm", data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditDepartment(Department data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                if (!string.IsNullOrEmpty(data.GUID))
                {
                    data.UpDatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    data.UpDatedBy = userInfo.GUID;
                    bool updated = da.UpdateDepartment(data);
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
                    bool added = da.AddDepartment(data);
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
        public ActionResult SearchDepartment(SM_Department mdl)
        {
            if (string.IsNullOrEmpty(mdl.DepartmentName))
                mdl.DepartmentID = 0;
            return RedirectToAction("DepartmentList", mdl);
        }

        public JsonResult DeleteDepartment(string GUID)
        {
            bool deleted = da.DeleteDepartment(GUID);
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