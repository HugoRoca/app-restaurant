using APPRestaurante.Helper;
using APPRestaurante.UnitOfWork;
using APPRestaurante.Web.Helper;
using System;
using System.Web.Mvc;
using APPRestaurante.Web.Areas.Admin.Filters;

namespace APPRestaurante.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        protected readonly IUnitOfWork _unit;

        public LoginController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ValidarUsuario(string usuario, string clave)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
                {
                    return Json(new { Success = false, Message = "Debe de completar todos los campos." });
                }

                var resultUsuario = _unit.Usuario.ValidarUsuario(usuario, clave);

                if (resultUsuario == null)
                {
                    return Json(new { Success = false, Message = "Usuario y/o contraseña incorrecto." });
                }

                SessionHelper.AddUserToSession(resultUsuario.id.ToString());
            }
            catch (Exception ex)
            {
                log.Error("Error Validar Usuario", ex);
                return Json(new { Error = true, Message = "Ocurrió un error al procesar solicitud." });
            }

            return Json(new { Success = true });
        }

        [HttpPost]
        public JsonResult CerrarSession()
        {
            SessionHelper.DestroyUserSession();
            return Json(new { Success = true});
        }
    }
}