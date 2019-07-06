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
    public class subcategoriasController : Controller
    {
        private GrupoNetEntities1 db = new GrupoNetEntities1();

        // GET: subcategorias
        public ActionResult Index()
        {
            var subcategorias = db.subcategorias.Include(s => s.categoria);
            return View(subcategorias.ToList());
        }

        // GET: subcategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subcategoria subcategoria = db.subcategorias.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }

        // GET: subcategorias/Create
        public ActionResult Create()
        {
            ViewBag.idcategoria = new SelectList(db.categorias, "idcategoria", "nombrecategoria");
            return PartialView("Create", new subcategoria());
        }

        // POST: subcategorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idsubcategoria,nombresubcategoria,idcategoria")] subcategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                db.subcategorias.Add(subcategoria);
                db.SaveChanges();
                var subcategoriass = db.subcategorias.Include(s => s.categoria);
                return PartialView("List", subcategoriass.ToList());
            }

            ViewBag.idcategoria = new SelectList(db.categorias, "idcategoria", "nombrecategoria", subcategoria.idcategoria);
            var subcategorias = db.subcategorias.Include(s => s.categoria);
            return PartialView("List", subcategorias.ToList());

        }

        // GET: subcategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subcategoria subcategoria = db.subcategorias.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcategoria = new SelectList(db.categorias, "idcategoria", "nombrecategoria", subcategoria.idcategoria);
            return View(subcategoria);
        }

        // POST: subcategorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idsubcategoria,nombresubcategoria,idcategoria")] subcategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subcategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idcategoria = new SelectList(db.categorias, "idcategoria", "nombrecategoria", subcategoria.idcategoria);
            return View(subcategoria);
        }

        // GET: subcategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subcategoria subcategoria = db.subcategorias.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }

        // POST: subcategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            subcategoria subcategoria = db.subcategorias.Find(id);
            db.subcategorias.Remove(subcategoria);
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
