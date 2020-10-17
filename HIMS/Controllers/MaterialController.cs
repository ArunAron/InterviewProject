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
    public class MaterialController : Controller
    {
        DA_Material da = new DA_Material();
        DA_MaterialType daMt = new DA_MaterialType();
        CommonClass cs = new CommonClass();

        // GET: Material
        public ActionResult MaterialList(SM_Material data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                ViewBag.SystemUserType = userInfo.SystemUserType;

                data.TotalPage = cs.TotalPage(da.GetAllMaterialCount(data));
                data.TotalCount = da.GetAllMaterialCount(data);
                data.CurrentPage = 1;

                List<MaterialType> mtList = daMt.GetAllMaterialTypes();
                mtList.Add(new MaterialType { GUID = "All", MaterialTypeName = "All", ID = 0 });
                ViewBag.MaterialTypeList = mtList.OrderBy(a => a.ID);
                ViewBag.SelectedMaterialType = data.MaterialTypeGUID;
                ViewBag.ActivePageID = "PageMaterial";
                return View("MaterialList", data);

            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }

        }

        public ActionResult MaterialListPartial(string getpassdata)
        {
            List<Material> list = new List<Material>();
            if (!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_Material>(getpassdata);
                list = da.GetMaterials_Filters(serializeData);
                ViewBag.CurrentPagePartial = serializeData.CurrentPage - 1;
            }
            return PartialView("MaterialListPartial", list);
        }

        public ActionResult GetTotalPage(string getpassdata)
        {
            List<Material> list = new List<Material>();
            int TotalPage = 0;
            if (!string.IsNullOrEmpty(getpassdata))
            {
                var serializeData = JsonConvert.DeserializeObject<SM_Material>(getpassdata);
                TotalPage = cs.TotalPage(da.GetAllMaterialCount(serializeData));
            }
            return Json(TotalPage, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditMaterialForm(string GUID)
        {
            Material data = new Material();
            ViewBag.MaterialTypeList = daMt.GetAllMaterialTypes();
            if (!string.IsNullOrEmpty(GUID))
            {
                data = da.GetAllMaterialByGUID(GUID);
                ViewBag.SelectedMaterialType = data.MaterialTypeGUID;
            }
            return PartialView("EditMaterialForm", data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditMaterial(Material data)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                data.MaterialTypeGUID = Request.Form["MaterialType"];
                if (!string.IsNullOrEmpty(data.GUID))
                {
                    data.UpDatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    data.UpDatedBy = userInfo.GUID;

                    bool updated = da.UpdateMaterial(data);
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
                    bool added = da.AddMaterial(data);
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
        public ActionResult SearchMaterial(SM_Material mdl)
        {
            mdl.MaterialTypeGUID = Request.Form["SearchMaterialType"];
            if (string.IsNullOrEmpty(mdl.MaterialName))
                mdl.MaterialID = 0;

            if (mdl.MaterialTypeGUID == "All")
                mdl.MaterialTypeGUID = null;

            return RedirectToAction("MaterialList", mdl);
        }

        public JsonResult DeleteMaterial(string GUID)
        {
            bool deleted = da.DeleteMaterial(GUID);
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