using APPRestaurante.Helper;
using APPRestaurante.UnitOfWork;
using APPRestaurante.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPRestaurante.Web.Areas.Admin.Controllers
{
    public class CargaController : BaseController
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        public CargaController(IUnitOfWork unit) : base(unit)
        {
        }

        [HttpPost]
        public JsonResult CargaDatos()
        {
            try
            {
                var permisos = _unit.Permiso.ObtenerPermisosDeAcceso(SessionHelper.GetUser());
                var usuario = _unit.Usuario.GetEntitybyId(SessionHelper.GetUser());
                return Json(new {
                    Success = true,
                    Usuario = usuario,
                    Permisos = permisos
                });
            }
            catch (Exception ex)
            {
                log.Error("Carga", ex);
                return Json(new { Error = true });
            }
        }
    }
}