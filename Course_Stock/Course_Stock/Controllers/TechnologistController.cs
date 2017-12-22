using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Course_Stock.DAO;
using Course_Stock.Models;
using Course_Stock.Logic.Technologist;
using PagedList;

namespace Course_Stock.Controllers
{
    [Authorize(Roles = "Technologist")]
    public class TechnologistController : Controller
    {
        Calculate Calc = new Calculate();
        TechnologistDAO t_d = new TechnologistDAO();

        public ViewResult ViewProduct()
        {
            Log.Log.For(this).Info("Start get controller for ViewProduct");
            return View();
        }

        public ActionResult SelectProduct(int? page)
        {
            Log.Log.For(this).Info("Start get controller for SelectProduct");
            int pageNumber = (page ?? 1);
            return View(t_d.GetAllProduct().ToPagedList(pageNumber, 10));
        }

        public ActionResult SelectComponentProduct(int id)
        {
            Log.Log.For(this).Info("Start get controller for SelectComponentProduct");
            ViewBag.id = id;
            return View(t_d.GetComponentProduct(id));
        }

        public ActionResult CreateCalculate(int id)
        {
            Log.Log.For(this).Info("Start get controller for CreateCalculate");
            DataCalculate model = new DataCalculate();
            model.id = id;
            model.valueBox = Calc.CalcMax(t_d.GetComponentForCalculate(id));
            ViewBag.id = id;
            Log.Log.For(this).Info(" " + model.valueBox);

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCalculate(DataCalculate model)
        {
            Log.Log.For(this).Info("Start post controller for CreateCalculate");
            try
            {
                Log.Log.For(this).Info("CreateCalculatePost " + model.id+" " +model.valueBox);

                return RedirectToAction("Result", model);
            }
            catch (Exception ex)
            {
                Log.Log.For(this).Error("Post controller for CreateCalculate exeption " + ex);
                return View();
            }
        }

        public ActionResult Result(DataCalculate model)
        {
            Log.Log.For(this).Info("Start get controller for Result");
            return View("Result", Calc.CalcResult(t_d.GetComponentForCalculate(model.id), model.valueBox));
        }

      



    }
}
