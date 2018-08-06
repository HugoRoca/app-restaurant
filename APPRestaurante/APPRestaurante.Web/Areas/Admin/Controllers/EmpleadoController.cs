using APPRestaurante.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPRestaurante.Web.Areas.Admin.Controllers
{
    public class EmpleadoController : BaseController
    {
        public EmpleadoController(IUnitOfWork unit) : base(unit)
        {

        }

        // GET: Admin/Empleado
        public ActionResult Index()
        {
            return View();
        }
    }
}