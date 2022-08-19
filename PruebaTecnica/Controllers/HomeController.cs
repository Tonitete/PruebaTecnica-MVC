using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            MantenimientoArticulo ma = new MantenimientoArticulo();
            return View(ma.RecuperarTodos());
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            MantenimientoArticulo ma = new MantenimientoArticulo();
            List<Articulo> art = ma.Buscar(collection["Buscar"].ToString());
            if (art.Count != 0)
            {
                return View("Search", art);
            }
            else
            {
                return View("ArticuloNoExiste");
            }
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            MantenimientoArticulo ma = new MantenimientoArticulo();
            Articulo art = ma.Recuperar(id);
            return View(art);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            MantenimientoArticulo ma = new MantenimientoArticulo();
            Articulo art = new Articulo
            {
                Codigo = int.Parse(collection["codigo"].ToString()),
                Nombre = collection["nombre"].ToString(),
                Precio = float.Parse(collection["precio"].ToString()),
                Descripcion = collection["descripcion"].ToString(),
                Familia = collection["familia"].ToString()
            };
            ma.Alta(art);
            return RedirectToAction("Index");
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            MantenimientoArticulo ma = new MantenimientoArticulo();
            Articulo art = ma.Recuperar(id);
            return View(art);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            MantenimientoArticulo ma = new MantenimientoArticulo();
            Articulo art = new Articulo()
            {
                Codigo = id,
                Nombre = collection["nombre"].ToString(),
                Precio = float.Parse(collection["precio"].ToString()),
                Descripcion = collection["descripcion"].ToString(),
                Familia = collection["familia"].ToString()
            };
            ma.Modificar(art);
            return RedirectToAction("Index");
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            MantenimientoArticulo ma = new MantenimientoArticulo();
            Articulo art = ma.Recuperar(id);
            return View(art);

        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            MantenimientoArticulo ma = new MantenimientoArticulo();
            ma.Borrar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Search(List<Articulo> art)
        {
            
            return View(art);
        }
        [HttpPost]
        public ActionResult Search(FormCollection collection)
        {
            MantenimientoArticulo ma = new MantenimientoArticulo();
            List<Articulo> art = ma.Buscar(collection["Buscar"].ToString());
            if (art.Count != 0)
            {
                return View("Search", art);
            }
            else
            {
                return View("ArticuloNoExiste");
            }
        }

        public ActionResult ArticuloNoExiste()
        {
            return View();
        }
    }
}
