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
    public class personasController : Controller
    {
        public static int idpersona=0;
        private GrupoNetEntities1 db = new GrupoNetEntities1();

        // GET: personas
        public ActionResult Index()
        {
            List<comunicacion> lis = new List<comunicacion>();
            Session["comunicacion"] = lis;
           
            var personas = db.personas.Include(p => p.tipodocumento);
            return View(personas.ToList());
        }

        // GET: personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            persona persona = db.personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: personas/Create
        public ActionResult Create()
        {
            ViewBag.idtipidocumento = new SelectList(db.tipodocumentoes, "idtipidocumento", "nombretipodocumento");
            return PartialView("Create",new persona());
        }

        // POST: personas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idpersona,dni,nombre,apellido,fechanacimiento,direccion,idtipidocumento")] persona persona)
        {
            if (ModelState.IsValid)
            {
                db.personas.Add(persona);
                db.SaveChanges();
                var Listpersona = db.personas.Include(p => p.tipodocumento);
                return PartialView("List",Listpersona.ToList());
            }

            ViewBag.idtipidocumento = new SelectList(db.tipodocumentoes, "idtipidocumento", "nombretipodocumento", persona.idtipidocumento);
            return View(persona);
        }

        // GET: personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            persona persona = db.personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.idtipidocumento = new SelectList(db.tipodocumentoes, "idtipidocumento", "nombretipodocumento", persona.idtipidocumento);
            return PartialView("Edit",persona);
        }

        // POST: personas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idpersona,dni,nombre,apellido,fechanacimiento,direccion,idtipidocumento")] persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                var listPersonas = db.personas.Include(p => p.tipodocumento);
                return PartialView("List", listPersonas.ToList());
            }
            ViewBag.idtipidocumento = new SelectList(db.tipodocumentoes, "idtipidocumento", "nombretipodocumento", persona.idtipidocumento);

            var listPersona = db.personas.Include(p => p.tipodocumento);
            return PartialView("List",listPersona.ToList());
        }

        // GET: personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            persona persona = db.personas.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete",persona);
        }

        // POST: personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(persona pe)
        {
            persona persona = db.personas.Find(pe.idpersona);
            db.personas.Remove(persona);
            db.SaveChanges();
            var listPersona = db.personas.Include(p => p.tipodocumento);
            return PartialView("List",listPersona.ToList());
        }

        public ActionResult CrearComunicacion(int id)
        {
            idpersona = id;
            List<comunicacion> lis = new List<comunicacion>();
            Session["comunicacion"] = lis;
            ViewBag.idtipocomunicacion = new SelectList(db.tipodecomunicacions, "idtipocomunicacion", "nombretipocomunicacion");

            return PartialView("CrearComunicacion",new comunicacion());
        }

        public ActionResult llenarcomunicacion(string nom, int idtipo) {
            List<comunicacion> lis =(List<comunicacion>)Session["comunicacion"];
            comunicacion c = new comunicacion();
            c.nombrecomunicacion = nom;
            c.idtipocomunicacion = idtipo;
            var nomtipo = db.tipodecomunicacions.Where(x => x.idtipocomunicacion == idtipo).FirstOrDefault();
            c.nombre = nomtipo.nombretipocomunicacion;
            lis.Add(c);
            Session["comunicacion"] = lis;
           return PartialView("llenarcomunicacion", lis);
        }

        public ActionResult EliminarComunicacion(int id, string nom) {
            List<comunicacion> lis = (List<comunicacion>)Session["comunicacion"];
           
            var eliminar = lis.Where(x => x.idtipocomunicacion == id && x.nombrecomunicacion == nom).FirstOrDefault();
            lis.Remove(eliminar);
            Session["comunicacion"] = lis;
            return PartialView("llenarcomunicacion", lis);
        }
        [HttpPost]
        public ActionResult CrearComunicacion(comunicacion com)
        {

            List<comunicacion> lis = (List<comunicacion>)Session["comunicacion"];

            foreach (var item in lis)
            {
                comunicacion c = new comunicacion();
                c.nombrecomunicacion = item.nombrecomunicacion;
                c.idtipocomunicacion = item.idtipocomunicacion;
                db.comunicacions.Add(c);
                db.SaveChanges();
                var idcomuni = db.comunicacions.Max(x => x.idcomunicacion);

                Comunicacion_PersonaDAO.Insert(idpersona,idcomuni);
            }

           
       
            var listPersona = db.personas.Include(p => p.tipodocumento);
            return PartialView("List", listPersona.ToList());
        }

        public ActionResult ListarComunicacion(int id)
        {
            var lis = Comunicacion_PersonaDAO.Listar(id);
            return PartialView("ListarComunicacion",lis);
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
