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
    public class CobranzaController : BaseController
    {
        public CobranzaController(IUnitOfWork unit) : base(unit)
        {
        }

        // GET: Admin/Cobranza
        public ActionResult Index()
        {
            return View();
        }
    }
}