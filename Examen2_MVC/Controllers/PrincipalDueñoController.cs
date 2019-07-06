using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examen2_MVC.Controllers
{
    public class PrincipalDueñoController : Controller
    {
        // GET: PrincipalDueño
        public ActionResult Index()
        {
            return PartialView();
        }

        // GET: PrincipalDueño/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PrincipalDueño/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrincipalDueño/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PrincipalDueño/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PrincipalDueño/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PrincipalDueño/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrincipalDueño/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
