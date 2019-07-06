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
    public class productoesController : Controller
    {
        private GrupoNetEntities1 db = new GrupoNetEntities1();

        // GET: productoes
        public ActionResult Index()
        {
            var productoes = db.productoes.Include(p => p.marca).Include(p => p.subcategoria).Include(p => p.unidaddemedida).Include(p => p.usuario);
            return View(productoes.ToList());
        }

        // GET: productoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producto producto = db.productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: productoes/Create
        public ActionResult Create()
        {
            ViewBag.idmarca = new SelectList(db.marcas, "idmarca", "nombremarca");
            ViewBag.idsubcategoria = new SelectList(db.subcategorias, "idsubcategoria", "nombresubcategoria");
            ViewBag.idunidadmedida = new SelectList(db.unidaddemedidas, "idunidaddemedida", "nombreunidaddemedida");
            ViewBag.idusuario = new SelectList(db.usuarios, "idusuario", "nombreusuario");
            return PartialView("Create",new producto());
        }

        // POST: productoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idproducto,nombreproducto,precioorigen,precioventa,stock,idsubcategoria,idmarca,idunidadmedida,idusuario")] producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.idusuario =(int)(Session["idusuario"]);
                db.productoes.Add(producto);
                db.SaveChanges();
                var productoess = db.productoes.Include(p => p.marca).Include(p => p.subcategoria).Include(p => p.unidaddemedida).Include(p => p.usuario);
                return PartialView("List",productoess.ToList());
            }

            ViewBag.idmarca = new SelectList(db.marcas, "idmarca", "nombremarca", producto.idmarca);
            ViewBag.idsubcategoria = new SelectList(db.subcategorias, "idsubcategoria", "nombresubcategoria", producto.idsubcategoria);
            ViewBag.idunidadmedida = new SelectList(db.unidaddemedidas, "idunidaddemedida", "nombreunidaddemedida", producto.idunidadmedida);
            ViewBag.idusuario = new SelectList(db.usuarios, "idusuario", "nombreusuario", producto.idusuario);
            var productoes = db.productoes.Include(p => p.marca).Include(p => p.subcategoria).Include(p => p.unidaddemedida).Include(p => p.usuario);
            return PartialView("List", productoes.ToList());
        }

        // GET: productoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producto producto = db.productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.idmarca = new SelectList(db.marcas, "idmarca", "nombremarca", producto.idmarca);
            ViewBag.idsubcategoria = new SelectList(db.subcategorias, "idsubcategoria", "nombresubcategoria", producto.idsubcategoria);
            ViewBag.idunidadmedida = new SelectList(db.unidaddemedidas, "idunidaddemedida", "nombreunidaddemedida", producto.idunidadmedida);
            ViewBag.idusuario = new SelectList(db.usuarios, "idusuario", "nombreusuario", producto.idusuario);
            return View(producto);
        }

        // POST: productoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idproducto,nombreproducto,precioorigen,precioventa,stock,idsubcategoria,idmarca,idunidadmedida,idusuario")] producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idmarca = new SelectList(db.marcas, "idmarca", "nombremarca", producto.idmarca);
            ViewBag.idsubcategoria = new SelectList(db.subcategorias, "idsubcategoria", "nombresubcategoria", producto.idsubcategoria);
            ViewBag.idunidadmedida = new SelectList(db.unidaddemedidas, "idunidaddemedida", "nombreunidaddemedida", producto.idunidadmedida);
            ViewBag.idusuario = new SelectList(db.usuarios, "idusuario", "nombreusuario", producto.idusuario);
            return View(producto);
        }

        // GET: productoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producto producto = db.productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            producto producto = db.productoes.Find(id);
            db.productoes.Remove(producto);
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
