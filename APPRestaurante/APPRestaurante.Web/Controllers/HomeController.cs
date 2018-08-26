using APPRestaurante.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPRestaurante.Web.Controllers
{
    public class HomeController : Controller
    {
        protected readonly IUnitOfWork _unit;

        public HomeController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        // GET: Home
        public ActionResult Index()
        {
            var lista = _unit.Menu.ListaMenuHome(DateTime.Now);
            ViewBag.Bebida = lista.Where(x => x.tipo == "Bebida").ToList();
            ViewBag.Entrada = lista.Where(x => x.tipo == "Entrada").ToList();
            ViewBag.Fondo = lista.Where(x => x.tipo == "Fondo").ToList();
            ViewBag.Postre = lista.Where(x => x.tipo == "Postre").ToList();

            ViewBag.Fecha = String.Format("Hoy es {0:D}", DateTime.Now);
            return View();
        }
    }
}