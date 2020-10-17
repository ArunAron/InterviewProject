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
    public class MaterialTypeController : Controller
    {
        DA_MaterialType da = new DA_MaterialType();
        CommonClass cs = new CommonClass();
        
        // GET: MaterialType
        public ActionResult MaterialTypeList(SM_MaterialType data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                ViewBag.SystemUserType = userInfo.SystemUserType;

                data.TotalPage = cs.TotalPage(da.GetAllMaterialTypeCount(data));
                data.TotalCount = da.GetAllMaterialTypeCount(data);
                data.CurrentPage = 1;

                ViewBag.ActivePageID = "PageMaterialType";
                return View("MaterialTypeList", data);
            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }
            
        }

        public ActionResult MaterialTypeListPartial(string getpassdata)
        {
            List<MaterialType> list = new List<MaterialType>();
            if(!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_MaterialType>(getpassdata);
                list = da.GetMaterialTypes_Filters(serializeData);
                ViewBag.CurrentPagePartial = serializeData.CurrentPage - 1;
            }
            return PartialView("MaterialTypeListPartial", list);
        }

        public ActionResult GetTotalPage(string getpassdata)
        {
            List<MaterialType> list = new List<MaterialType>();
            int TotalPage = 0;
            if (!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_MaterialType>(getpassdata);
                TotalPage = cs.TotalPage(da.GetAllMaterialTypeCount(serializeData));
            }
            return Json(TotalPage, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditMaterialTypeForm(string GUID)
        {
            MaterialType data = new MaterialType();
            if (!string.IsNullOrEmpty(GUID))
            {
                data = da.GetAllMaterialTypeByGUID(GUID);
            }
            return PartialView("EditMaterialTypeForm", data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditMaterialType(MaterialType data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                if (!string.IsNullOrEmpty(data.GUID))
                {
                    data.UpDatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    data.UpDatedBy = userInfo.GUID;
                    bool updated = da.UpdateMaterialType(data);
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
                    bool added = da.AddMaterialType(data);
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
        public ActionResult SearchMaterialType(SM_MaterialType mdl)
        {
            if (string.IsNullOrEmpty(mdl.MaterialTypeName))
                mdl.MaterialTypeID = 0;
            return RedirectToAction("MaterialTypeList", mdl);
        }

        public JsonResult DeleteMaterialType(string GUID)
        {
            bool deleted = da.DeleteMaterialType(GUID);
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