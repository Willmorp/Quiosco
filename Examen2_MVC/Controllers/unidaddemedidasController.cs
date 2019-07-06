using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Examen2_MVC.Models;

namespace Examen2_MVC.Controllers
{
    public class unidaddemedidasController : Controller
    {
        private GrupoNetEntities1 db = new GrupoNetEntities1();

        // GET: unidaddemedidas
        public ActionResult Index()
        {
            return View(db.unidaddemedidas.ToList());
        }

        // GET: unidaddemedidas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            unidaddemedida unidaddemedida = db.unidaddemedidas.Find(id);
            if (unidaddemedida == null)
            {
                return HttpNotFound();
            }
            return View(unidaddemedida);
        }

        // GET: unidaddemedidas/Create
        public ActionResult Create()
        {
            return PartialView("Create", new unidaddemedida());
        }

        // POST: unidaddemedidas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idunidaddemedida,nombreunidaddemedida")] unidaddemedida unidaddemedida)
        {
            if (ModelState.IsValid)
            {
                db.unidaddemedidas.Add(unidaddemedida);
                db.SaveChanges();
                return PartialView("List", db.unidaddemedidas.ToList());
            }

            return PartialView("List",db.unidaddemedidas.ToList());
        }

        // GET: unidaddemedidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            unidaddemedida unidaddemedida = db.unidaddemedidas.Find(id);
            if (unidaddemedida == null)
            {
                return HttpNotFound();
            }
            return View(unidaddemedida);
        }

        // POST: unidaddemedidas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idunidaddemedida,nombreunidaddemedida")] unidaddemedida unidaddemedida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unidaddemedida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(unidaddemedida);
        }

        // GET: unidaddemedidas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            unidaddemedida unidaddemedida = db.unidaddemedidas.Find(id);
            if (unidaddemedida == null)
            {
                return HttpNotFound();
            }
            return View(unidaddemedida);
        }

        // POST: unidaddemedidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            unidaddemedida unidaddemedida = db.unidaddemedidas.Find(id);
            db.unidaddemedidas.Remove(unidaddemedida);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
