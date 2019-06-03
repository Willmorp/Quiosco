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
    public class usuariosController : Controller
    {
        private GrupoNetEntities1 db = new GrupoNetEntities1();

        // GET: usuarios
        public ActionResult Index()
        {
            var usuarios = db.usuarios.Include(u => u.persona).Include(u => u.tipousuario);
            return View(usuarios.ToList());
        }

        // GET: usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: usuarios/Create
        public ActionResult Create()
        {
            ViewBag.idpersona = new SelectList(db.personas, "idpersona", "dni");
            ViewBag.idtipousuario = new SelectList(db.tipousuarios, "idtipousuario", "nombretipousuario");
            return PartialView("Create",new usuario());
        }

        // POST: usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idusuario,nombreusuario,clave,idpersona,idtipousuario")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.usuarios.Add(usuario);
                db.SaveChanges();
                var listusuarios = db.usuarios.Include(x => x.tipousuario).Include(y => y.persona);
                return PartialView("List", listusuarios.ToList());
            }

            ViewBag.idpersona = new SelectList(db.personas, "idpersona", "dni", usuario.idpersona);
            ViewBag.idtipousuario = new SelectList(db.tipousuarios, "idtipousuario", "nombretipousuario", usuario.idtipousuario);
            var listusuario = db.usuarios.Include(x => x.tipousuario).Include(y => y.persona);
            return PartialView("List", listusuario.ToList());
        }

        // GET: usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.idpersona = new SelectList(db.personas, "idpersona", "dni", usuario.idpersona);
            ViewBag.idtipousuario = new SelectList(db.tipousuarios, "idtipousuario", "nombretipousuario", usuario.idtipousuario);
            return PartialView("Edit",usuario);
        }

        // POST: usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idusuario,nombreusuario,clave,idpersona,idtipousuario")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                var listusuario = db.usuarios.Include(x => x.tipousuario).Include(y => y.persona);
                return PartialView("List", listusuario.ToList());
            }
            ViewBag.idpersona = new SelectList(db.personas, "idpersona", "dni", usuario.idpersona);
            ViewBag.idtipousuario = new SelectList(db.tipousuarios, "idtipousuario", "nombretipousuario", usuario.idtipousuario);
            var listusuarios = db.usuarios.Include(x => x.tipousuario).Include(y => y.persona);
            return PartialView("List", listusuarios.ToList());
        }

        // GET: usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete",usuario);
        }

        // POST: usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(usuario usu)
        {
            usuario usuario = db.usuarios.Find(usu.idusuario);
            db.usuarios.Remove(usuario);
            db.SaveChanges();
            var listusuario = db.usuarios.Include(x => x.tipousuario).Include(y => y.persona);
            return PartialView("List", listusuario.ToList());
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
