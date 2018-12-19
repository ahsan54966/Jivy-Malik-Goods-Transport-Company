using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Jivy_Malik_Goods_Transport_Company.Models;
using Jivy_Malik_Goods_Transport_Company.ViewModel.Admin.BuiltyData;
using Jivy_Malik_Goods_Transport_Company.ViewModel.Admin.RouteDetail;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jivy_Malik_Goods_Transport_Company.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //Here Is Vehicle Routs Start
        public ActionResult VehicleRouts()
        {
            var data = new RouteDetailCrudViewModel();
            var AllData = data.ShowAllRoutes();

            var viewmodel = new RouteDetailViewModel()
            {
                routedetail = AllData,



            };



            return View(viewmodel);
        }




        [HttpPost]
        public JsonResult AddNewRoutes(VehicleRoutes Rout)
        {
            if (Rout.RouteId == 0)
            {
                var data = new RouteDetailCrudViewModel();
                data.AddNewRoutes(Rout);


                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = new RouteDetailCrudViewModel();
                data.AddRoutesUpdate(Rout);


                return Json("ok", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuiltyHome()
        {
            var method = new BuiltyCrudViewModel();
            var builtylist = new List<Builty>();
            try
            {
                builtylist=method.GetAllBuilty();
            }
            catch(Exception e)
            {
                TempData["BuiltyHomeMessage"] = e.Message;
            }
            return View(builtylist);
        }
        public ActionResult CompanyBuilty(int id = 0)
        {
            Builty editBuilty = new Builty();

            var method = new BuiltyCrudViewModel();

            //Callthis Method from VehicleCRudView
            var viewmodel = new BuiltyViewModel();
            if (id != 0)
            {
                editBuilty = method.Editbuilty(id);
            }
            viewmodel.Builty = editBuilty;
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult CompanyBuilty(Builty builty)
        {
            var ReportData = new DataSet();
            string message = "";
            var method = new BuiltyCrudViewModel();
            var viewmodel = new BuiltyViewModel();
           // ModelState["BuiltyNumber"].Errors.Clear();
            if(ModelState.IsValid)
            {
                if (builty.BuiltyNumber == 0)
                {
                    try
                    {
                        ReportData = method.SaveBuilty(builty);
                        if (ReportData.Tables[0].Rows.Count != 0)
                        {
                            TempData["ReportData"] = ReportData;
                            TempData["Type"] = builty.BuiltyType;
                            TempData.Keep();
                            return RedirectToAction("GenerateBuilty", "Admin");
                        }
                    }
                    catch(Exception e)
                    {
                        message = e.Message;
                    }

//                    return RedirectToAction("builtyhome", "Admin", new { livecompanyid = Session["user_Id"] });

                }
                else
                {
                    try
                    {
                        ReportData = method.BuiltyUpdate(builty);

                        if (ReportData.Tables[0].Rows.Count != 0)
                        {
                            TempData["ReportData"] = ReportData;
                            TempData["Type"] = builty.BuiltyType;
                            TempData.Keep();
                            return RedirectToAction("GenerateBuilty", "Admin");
                        }

                    }
                    catch (Exception e)
                    {
                        message = e.Message;
                    }

                    /*    if (message != "")
                        {
                            TempData["message"] = "Builty update sucessfully";
                            TempData.Keep();
                            return RedirectToAction("builtyhome", "Admin", new { livecompanyid = Session["user_Id"] });
                        }
                        else
                        {*/

            //        return RedirectToAction("builtyhome", "Admin", new { livecompanyid = Session["user_Id"] });

                }

          }
            TempData["GenerateBuiltyMessage"] = message;
  /*          viewmodel = new BuiltyViewModel()
            {
                builty = builty
            };
   */
            viewmodel.Builty = builty;
            return View(viewmodel);
        }
        public ActionResult FactoryBuilty()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FactoryBuilty(Builty builty)
        {
            return View();
        }
        public void GenerateBuilty()
        {
            string type = TempData["Type"].ToString();
            var ReportData = (DataSet)TempData["ReportData"];
            ReportDocument rd = new ReportDocument();
            if (type=="Company")
            {
                rd.Load(Path.Combine(Server.MapPath("~/Reports/FactoryBuilty.rpt")));
            }
            else if (type=="Factory")
            {
                rd.Load(Path.Combine(Server.MapPath("~/Reports/FactoryBuilty.rpt")));
            }

            rd.SetDataSource(ReportData);
            Response.Clear();
            string filepath = Server.MapPath("~/" + "Builty.rpt");
            rd.ExportToDisk(ExportFormatType.PortableDocFormat, filepath);
            FileInfo fileinfo = new FileInfo(filepath);
            Response.AddHeader("Content-Disposition", "inline;filenam=Builty.pdf");
            Response.ContentType = "application/pdf";
            Response.WriteFile(fileinfo.FullName);
        }

        public JsonResult GetVehicles(string Prefix)
        {
            var method = new BuiltyCrudViewModel();
            var vehiclelist = method.GetAllVehicles();
            /*
            var VehicleData = (from N in vehiclelist
                               where N.VehicleNumber.StartsWith(Prefix)
                               select new { N.VehicleNumber });
             */
            return Json(vehiclelist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVehicleInformation(string VehicleNumber)
        {
            var method = new BuiltyCrudViewModel();
            var VehicleInformation = method.GetVehicleInformation(VehicleNumber);
            return Json(VehicleInformation, JsonRequestBehavior.AllowGet);
        }


        //Builty Detail

        public ActionResult BuiltyDetail(int id = 0)
        {
            var BuiltyDetail = new BuiltyCrudViewModel();
            Builty builtydetail = BuiltyDetail.Editbuilty(id);
            return View(builtydetail);
        }

        public ActionResult BuiltyDelete(string id, int companyid = 0)
        {
            string message = "";
            if (id == "")
            {
                TempData["message"] = "Id Missing";

            }
            else
            {

                var builtydlete = new BuiltyCrudViewModel();
                try
                {
                    string result = builtydlete.BuiltyDelete(id);
                    if (result == "1")
                    {
                        message = "Builty Deleted Successfully";
                        TempData["BuiltyHomeMessage"] = message;
                    }
                    else if (result == "0")
                    {
                        message = "Problem During Delete";
                        TempData["BuiltyHomeMessage"] = message;
                    }
                }
                catch (Exception e)
                {
                    TempData["BuiltyHomeMessage"] = e.Message.ToString();
                }

            }

            return RedirectToAction("BuiltyHome", "Admin", new {});
        }
    }
}