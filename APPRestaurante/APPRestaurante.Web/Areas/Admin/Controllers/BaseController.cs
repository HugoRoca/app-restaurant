using APPRestaurante.Helper;
using APPRestaurante.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPRestaurante.Web.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _unit;

        public BaseController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Permisos = _unit.Permiso.ObtenerPermisosDeAcceso(SessionHelper.GetUser());

            base.OnActionExecuting(filterContext);
        }
    }
}