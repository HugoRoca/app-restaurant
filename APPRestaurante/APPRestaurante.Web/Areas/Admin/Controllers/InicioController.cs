using APPRestaurante.UnitOfWork;
using APPRestaurante.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APPRestaurante.Web.Areas.Admin.Filters;
using APPRestaurante.Helper;

namespace APPRestaurante.Web.Areas.Admin.Controllers
{
    [Autenticado]
    public class InicioController : BaseController
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        public InicioController(IUnitOfWork unit) : base(unit)
        {
        }

        // GET: Admin/Inicio
        public ActionResult Index()
        {
            try
            {
                ViewBag.Permisos = _unit.Permiso.ObtenerPermisosDeAcceso(SessionHelper.GetUser());
                return View(_unit.Usuario.GetEntitybyId(SessionHelper.GetUser()));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}