using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Course_Stock.DAO;
using Course_Stock.Models;

namespace Course_Stock.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        AdminDAO a_d = new AdminDAO();
        public ActionResult Index()
        {
            Log.Log.For(this).Info("Start get controller for Index");
            return View(a_d.GetAllUsers());
        }

        public ActionResult Edit(string id)
        {
            Log.Log.For(this).Info("Start get controller for Edit");
            AdminDAO a_d = new AdminDAO();
            ViewData["Roles"] = new SelectList(a_d.GetRoles());
            return View(a_d.getById(id));
        }

        [HttpPost]
        public ActionResult Edit(User record)
        {
            Log.Log.For(this).Info("Start post controller for Edit");
            if (ModelState.IsValid)
            {
                a_d.EditRecord(record);
                return RedirectToAction("Index");
            }
            else
            {
                Log.Log.For(this).Error("Post controller for Edit: Model not valid");
                return View("Edit");
            }
        }

        public ActionResult Delete(string id)
        {
            Log.Log.For(this).Info("Start get controller for Delete");
            return View(a_d.getById(id));
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            Log.Log.For(this).Info("Start post controller for Delete");
            try
            {
                AdminDAO a_d = new AdminDAO();
                a_d.DeleteRecord(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex )
            {
                Log.Log.For(this).Error("Post controller for Delete exeption " + ex);
                return View("Delite");
            }
        }
    }
}
