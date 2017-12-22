using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Course_Stock.DAO;
using Course_Stock.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System.ComponentModel;
using System.Web.UI;

namespace Course_Stock.Controllers
{

    public class WorkerController : Controller
    {

        WorkerDAO w_d = new WorkerDAO();

        [Authorize(Roles = "Accountant, Manager")]
        public ViewResult ViewMaterials()
        {
            Log.Log.For(this).Info("Start get controller for ViewMaterials");
            return View();
        }

        [Authorize(Roles = "Accountant, Manager")]
        public ActionResult SelectMaterials(int? page)
        {
            Log.Log.For(this).Info("Start get controller for SelectMaterials");
            int pageNumber = (page ?? 1);
            return View(w_d.GetAllMaterial().ToPagedList(pageNumber, 10));
        }


        [Authorize(Roles = "Accountant, Manager")]
        public ViewResult ViewTables(int id)
        {
            Log.Log.For(this).Info("Start get controller for ViewTables");
            return View();
        }

        [Authorize(Roles = "Accountant, Manager")]
        public ActionResult SelectTables(int id, int? page)
        {
            Log.Log.For(this).Info("Start get controller for SelectTables");
            ViewBag.id = id;
            int pageNumber = (page ?? 1);
            return View(w_d.GetTables(id).ToPagedList(pageNumber, 10));
        }


        [Authorize(Roles = "Accountant, Manager")]
        public ViewResult ViewRecords(int id, bool close)
        {
            Log.Log.For(this).Info("Start get controller for ViewRecords");
            return View();
        }

        [Authorize(Roles = "Accountant, Manager")]
        public ActionResult SelectRecords(int id, bool close, int? page)
        {
            Log.Log.For(this).Info("Start get controller for SelectRecords");
            ViewBag.id = id;
            ViewBag.close = close;
            int pageNumber = (page ?? 1);

            return View(w_d.GetRecords(id).ToPagedList(pageNumber, 10));
        }

        [Authorize(Roles = "Manager")]
        public ActionResult CreateMaterial()
        {
            Log.Log.For(this).Info("Start get controller for CreateMaterial");
            return View();
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult CreateMaterial(Material m)
        {
            Log.Log.For(this).Info("Start post controller for CreateMaterial");
            try
            {
                w_d.CreateMaterial(m);
                return RedirectToAction("ViewMaterials");
            }
            catch (Exception ex)
            {
                Log.Log.For(this).Error("Post controller for CreateMaterial exeption " + ex);
                return View();
            }
        }

        [Authorize(Roles = "Accountant, Manager")]
        public ActionResult CreateRecord(int id)
        {
            Log.Log.For(this).Info("Start get controller for CreateRecord");
            ViewBag.id = id;
            Record r = new Record();
            r.id_table = id;
            return View(r);
        }

        [Authorize(Roles = "Accountant, Manager")]
        [HttpPost]
        public ActionResult CreateRecord(Record r)
        {
            Log.Log.For(this).Info("Start post controller for CreateRecord");
            try
            {
                r.worker = User.Identity.GetUserName();
                w_d.CreateRecord(r);
                return RedirectToAction("ViewRecords", new { id = r.id_table, close = false });
            }
            catch (Exception ex)
            {
                Log.Log.For(this).Error("Post controller for CreateRecord exeption " + ex);
                return View();
            }
        }

        [Authorize(Roles = "Manager")]
        public ActionResult CloseTable(int id_close, int id_bag)
        {
            Log.Log.For(this).Info("Start get controller for CloseTable");
            w_d.CloseTable(id_close);
            return RedirectToAction("ViewTables", new { id = id_bag });
        }

        [Authorize(Roles = "Manager")]
        public ActionResult OpenTable(int id)
        {
            Log.Log.For(this).Info("Start get controller for OpenTable");
            ViewBag.id = id;
            w_d.OpenTable(id);
            return RedirectToAction("ViewTables", new { id });

        }

        [Authorize(Roles = "Manager")]
        public ViewResult ViewRecordsNonCheck(int? page)
        {
            Log.Log.For(this).Info("Start get controller for ViewRecordsNonCheck");
            return View();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult SelectRecordsNonCheck(int? page)
        {
            Log.Log.For(this).Info("Start get controller for SelectRecordsNonCheck");
            int pageNumber = (page ?? 1);
            return View(w_d.GetRecordsNonCheck().ToPagedList(pageNumber, 10));

        }

        [Authorize(Roles = "Manager")]
        public ActionResult SetCheckRecord(int id, bool check)
        {
            Log.Log.For(this).Info("Start get controller for SetCheckRecord");
            w_d.SetCheck(id, check);
            return RedirectToAction("ViewRecordsNonCheck");
        }










    }
}
