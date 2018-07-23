using APPRestaurante.UnitOfWork;
using APPRestaurante.Web.Areas.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPRestaurante.Web.Areas.Admin.Controllers
{
    [Autenticado]
    public class MenuController : BaseController
    {
        public MenuController(IUnitOfWork unit) : base(unit)
        {

        }

        // GET: Admin/Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Count(int rows)
        {
            var total = _unit.Menu.Count();
            var totalPagina = total / rows;
            var page = new
            {
                TotalPages = totalPagina
            };

            return Json(new { Page = page});
        }

        public JsonResult ListaMenu(DateTime desde, DateTime hasta, int pagina, int fila)
        {
            var start = ((pagina - 1) * fila) + 1;
            var end = pagina * fila;
            return Json(_unit.Menu.ListaMenuPaginacion(desde, hasta, start, end));
        }
    }
}