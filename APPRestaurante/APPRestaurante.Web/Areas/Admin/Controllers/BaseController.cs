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
            var idUsuario = SessionHelper.GetUser();
            ViewBag.Permisos = _unit.Permiso.ObtenerPermisosDeAcceso(idUsuario);
            ViewBag.Usuario = _unit.Usuario.GetEntitybyId(idUsuario);

            base.OnActionExecuting(filterContext);
        }
    }
}