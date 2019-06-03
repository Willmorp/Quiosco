using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Examen2_MVC.Models;
using Examen2_MVC.Servicio;

namespace Examen2_MVC.Controllers
{
    public class PrincipalController : Controller
    {
        private GrupoNetEntities1 db = new GrupoNetEntities1();
        // GET: Principal
        public ActionResult Index()
        {
           
           var listar= db.sedes.Include(p => p.empresa).Include(u => u.usuario);
            foreach (var item in listar)
            {
                string nom = item.nombresede;
            }
            return View(listar);
        }

        // GET: Principal/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Principal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Principal/Create
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

        // GET: Principal/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Principal/Edit/5
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

        // GET: Principal/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Principal/Delete/5
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
