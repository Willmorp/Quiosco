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
    public class empresasController : Controller
    {
        private GrupoNetEntities1 db = new GrupoNetEntities1();

        // GET: empresas
        public ActionResult Index()
        {
            return View(db.empresas.ToList());
        }

        // GET: empresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresa empresa = db.empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: empresas/Create
        public ActionResult Create()
        {
            return PartialView("Create",new empresa());
        }

        // POST: empresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idempresa,nombreempresa,ruc,telefono,celular,direccion,estadotabla")] empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.empresas.Add(empresa);
                db.SaveChanges();
                return PartialView("List", db.empresas.ToList());
            }

            return PartialView("List", db.empresas.ToList());
        }

        // GET: empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresa empresa = db.empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return PartialView("Edit",empresa);
        }

        // POST: empresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idempresa,nombreempresa,ruc,telefono,celular,direccion,estadotabla")] empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("List", db.empresas.ToList());
            }
            return PartialView("List", db.empresas.ToList());
        }

        // GET: empresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresa empresa = db.empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete",empresa);
        }

        // POST: empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(empresa emp)
        {
            empresa empresa = db.empresas.Find(emp.idempresa);
            db.empresas.Remove(empresa);
            db.SaveChanges();
            return PartialView("List",db.empresas.ToList());
        }

        public ActionResult Sede(int id)
        {

            ViewBag.idempresa = new SelectList(db.empresas, "idempresa", "nombreempresa");
            ViewBag.idusuario = new SelectList(db.usuarios, "idusuario", "nombreusuario");
            return PartialView("Sede",new sede());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sede(sede sede)
        {
            if (ModelState.IsValid)
            {
                db.sedes.Add(sede);
                db.SaveChanges();
                return PartialView("List", db.empresas.ToList());
            }

            return PartialView("List", db.empresas.ToList());
        }


        public ActionResult ListarSede(int id)
        {
            var sedes = db.sedes.Include(u => u.usuario).Include(e => e.empresa).Where(x => x.idempresa == id);
            return PartialView("ListarSede",sedes.ToList());
        }


        public ActionResult DeleteSede(int id, int idempresa)
        {
            sede se = db.sedes.Find(id);
            db.sedes.Remove(se);
            db.SaveChanges();
            var sedes = db.sedes.Include(u => u.usuario).Include(e => e.empresa).Where(x => x.idempresa == idempresa);
            return PartialView("ListSede", sedes.ToList());
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
