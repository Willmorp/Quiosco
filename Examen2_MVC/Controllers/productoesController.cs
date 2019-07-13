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
        public ActionResult Index(string nombre)
        {
            @ViewBag.nomtienda = nombre;
            var productoes = db.productoes ;
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

        public ActionResult getDeta(int id)
        {
            producto pr = db.productoes.Find(id);
            //comprar el articulo seleccionado visualizar
            Compras cp = new Compras();
            cp.idproducto = id;
            cp.nombreproducto = pr.nombreproducto;
            cp.precioventa = pr.precioventa;
            //cp.Imagen = ar.Imagen;
            cp.stock = pr.stock;
            cp.cantidad = 0;
            Session["venta"] = cp;

            return View(cp);
        }
        [HttpPost]
        public ActionResult getDeta(Compras cp)
        {//envia la compra para ser colocado en una lista  generica
            List<Compras> lista;
            Compras comp = (Compras)Session["venta"];
            comp.cantidad = cp.cantidad;
            if (Session["canasta"] == null)
            {
                lista = new List<Compras>();

            }
            else
            {
                lista = (List<Compras>)Session["canasta"];
            }
            lista.Add(comp);
            Session["canasta"] = lista;//se actualiza la session
            return RedirectToAction("getCompra");

        }
        public ActionResult getCompra()
        {
            List<Compras> lis = (List<Compras>)Session["canasta"];
            return View(lis);
        }

        public ActionResult getDel(int nro)
        {
            List<Compras> lis = (List<Compras>)Session["canasta"];
            lis.RemoveAt(nro);
            Session["canasta"] = lis;
            return RedirectToAction("getCompra");
        }

        public ActionResult getLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult getLogin(string usr, string pas)
        {
            usuario usu = db.usuarios.Find(usr.Trim());
            string pagina = "";
            string men = "";
            if (usu == null)
            {
                men = "no existe usuario";
            }
            else
            {
                if (pas.Trim() == usu.clave.Trim())
                {
                    pagina = "getConfirma";
                    Session["cliente"] = usu;
                    return RedirectToAction(pagina);
                }
                else
                {
                    men = "Clave incorrecta";
                }
            }
            ViewBag.mensaje = men;
            return View();
        }
        public ActionResult getConfirma()
        {
            List<Compras> lis = (List<Compras>)Session["canasta"];
            usuario usu = (usuario)Session["cliente"];
            string nombre = usu.nombreusuario + "," + usu.sedes;
            //ViewBag.cl nom = nombre;
            return View(lis);

        }
        public ActionResult getGraba()
        {
            usuario usu = (usuario)Session["cliente"];
            List<Compras> lista = (List<Compras>)Session["canasta"];
            double sm = 0;
            foreach (var cp in lista)
            {
                sm = sm + cp.total;
            }
            string fac = "";
            //string fac = db.grabafac(usu.idusuario, (decimal)sm).FirstOrDefault();

            foreach (var dt in lista)
            {
                //db.grabadeta(fac, dt.idusuario, dt.cantidad);
            }
            string clinom = usu.nombreusuario + "," + usu.sedes;
            Session["canasta"] = null;
            Session["cliente"] = null;
            return RedirectToAction("getResumen", new { nro = fac, total = sm, nombre = clinom });
        }
        public ActionResult getResumen(string nro, double total, string nombre)
        {
            ViewBag.factura = nro;
            ViewBag.total = total;
            ViewBag.cliente = nombre;
            return View();
        }
    }
}
