using APPRestaurante.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPRestaurante.Web.Areas.Admin.Controllers
{
    [RoutePrefix("Menu")]
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

        [Route("ListaMenu/{page:int}/{row:int}")]
        public PartialViewResult ListaMenu(int page, int row)
        {
            var start = ((page - 1) * row) + 1;
            var end = page * row;
            return PartialView(_unit.Menu.ListaMenuPaginacion(DateTime.Now, DateTime.Now, start, end));
        }
    }
}