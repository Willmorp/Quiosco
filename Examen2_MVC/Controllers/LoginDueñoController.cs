using Examen2_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examen2_MVC.Controllers
{
    public class LoginDueñoController : Controller
    {
        private GrupoNetEntities1 db = new GrupoNetEntities1();
        // GET: LoginDueño
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Login login)
        {
            try
            {

            }
            catch (Exception)
            {


            }
            var valor = db.usuarios.Where(x => x.nombreusuario == login.usuario && x.clave == login.contraseña && x.idtipousuario == 2).FirstOrDefault();
            if (valor != null)
            {
                Session["idusuario"] = valor.idusuario;
                Session["nomusuario"] = valor.nombreusuario;
                Session["clave"] = valor.clave;

                return RedirectToAction("Index", "PrincipalDueño");
            }
            else
            {
                return RedirectToAction("Index");
            }


        }
        // GET: LoginDueño/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginDueño/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginDueño/Create
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

        // GET: LoginDueño/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginDueño/Edit/5
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

        // GET: LoginDueño/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginDueño/Delete/5
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
