using APPRestaurante.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPRestaurante.Web.Areas.Admin.Controllers
{
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

        [Route("Count/{rows:int}")]
        public JsonResult Count(int rows)
        {
            var total = _unit.Menu.Count();
            var totalPagina = total / rows;
            var page = new
            {
                TotalPages = totalPagina
            };

            return Json(page);
        }
    }
}