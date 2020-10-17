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
    public class UOMController : Controller
    {
        DA_UOM da = new DA_UOM();
        Material mtdata = new Material();
        // GET: UOM
        public ActionResult UOMList(string GUID)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                ViewBag.SystemUserType = userInfo.SystemUserType;
                mtdata = new DA_Material().GetAllMaterialByGUID(GUID);
                List<UOMType> umotyLst = new DA_UOMType().GetAllUOMTypes();
                umotyLst.Add(new UOMType { GUID = "All", Type = "Choose Type", ID = 0 });
                ViewBag.UOMTypeList = umotyLst.OrderBy(a => a.ID);
                return PartialView("UOMList", mtdata);

            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }
           
        }

        public ActionResult UOMListPartial(string getpassdata)
        {
            List<UOM> list = new List<UOM>();
            if (!string.IsNullOrEmpty(getpassdata))
            {
                list = da.GetUOMs_Filters(getpassdata).OrderBy(a => a.Level).ToList();
            }
            return PartialView("UOMListPartial", list);
        }

        public ActionResult EditUMO(string getpassdata)
        {
            if (Session["UserInfo"] != null)
            {
                SystemUser userInfo = (SystemUser)Session["UserInfo"];
                List<Material> list = new List<Material>();
                if (!string.IsNullOrEmpty(getpassdata))
                {
                    var serializeData = JsonConvert.DeserializeObject<SM_UOM>(getpassdata);
                    UOM data = new UOM();
                    data.GUID = serializeData.GUID;
                    data.MaterialGUID = serializeData.MaterialGUID;
                    data.MaterialCode = serializeData.MaterialCode;
                    data.Level = serializeData.Level;
                    data.Quantity = serializeData.Quantity;
                    data.Description = serializeData.Quantity + serializeData.Type;
                    data.Type = serializeData.Type;

                    bool result = false;
                    if (!string.IsNullOrEmpty(data.GUID))
                    {
                        data.UpDatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                        data.UpDatedBy = userInfo.GUID;
                        result = da.UpdateUOM(data);
                    }
                    else
                    {

                        data.GUID = Guid.NewGuid().ToString();
                        data.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                        data.CreatedBy = userInfo.GUID;
                        data.UpDatedDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                        data.UpDatedBy = userInfo.GUID;
                        data.Status = 1;
                        result = da.AddUOM(data);
                    }
                    if (result)
                    {
                        //Saving UMODescription in Material
                        mtdata = new DA_Material().GetAllMaterialByGUID(serializeData.MaterialGUID);
                        List<UOM> umotyLst = da.GetUOMs_Filters(serializeData.MaterialGUID).OrderBy(a => a.Level).ToList();
                        int c = 1;
                        UOM tempUOM = new UOM();
                        foreach (var a in umotyLst)
                        {
                            if (c == 1)
                            {
                                mtdata.UOMDescription = a.Description;
                                mtdata.UOMType = a.Type;
                            }
                            else if (c == 2)
                            {
                                mtdata.UOMDescription += " -> " + a.Description;
                                tempUOM = a;
                            }
                            else
                            {
                                mtdata.UOMDescription = mtdata.UOMDescription + " / 1" + tempUOM.Type;
                                mtdata.UOMDescription += " -> " + a.Description;
                            }
                            c++;
                        }
                        new DA_Material().UpdateMaterialUOM(mtdata);
                        //end of Saving UMODescription in Material
                        return Json(mtdata.UOMDescription, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Fail", JsonRequestBehavior.AllowGet);
                    }
                }
                return PartialView("MaterialListPartial", list);

            }
            else
            {
                return RedirectToAction("SessionTimeOut", "Error");
            }
            
        }

        public JsonResult DeleteUOM(string GUID)
        {
            UOM temp = new DA_UOM().GetAllUOMByGUID(GUID);
            bool deleted = da.DeleteUOM(GUID);
            if (deleted)
            {
                mtdata = new DA_Material().GetAllMaterialByGUID(temp.MaterialGUID);
                List<UOM> umotyLst = da.GetUOMs_Filters(temp.MaterialGUID).OrderBy(a => a.Level).ToList();
                int c = 1;
                UOM tempUOM = new UOM();
                foreach (var a in umotyLst)
                {
                    if (c == 1)
                    {
                        mtdata.UOMDescription = a.Description;
                        mtdata.UOMType = a.Type;
                    }
                    else if (c == 2)
                    {
                        mtdata.UOMDescription += " -> " + a.Description;
                        tempUOM = a;
                    }
                    else
                    {
                        mtdata.UOMDescription = mtdata.UOMDescription + " / 1" + tempUOM.Type;
                        mtdata.UOMDescription += " -> " + a.Description;
                    }
                    c++;
                }
                new DA_Material().UpdateMaterialUOM(mtdata);
                return Json(mtdata.UOMDescription, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }
    }
}