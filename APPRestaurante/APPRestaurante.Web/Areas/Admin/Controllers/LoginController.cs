using APPRestaurante.UnitOfWork;
using APPRestaurante.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace APPRestaurante.Web.Areas.Admin.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Admin/Login
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        public LoginController(IUnitOfWork unit) : base(unit)
        {
        }

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
            }
            catch (Exception ex)
            {
                log.Error("Error Validar Usuario", ex);
                return Json(new { Error = true, Message = "Ocurrió un error al procesar solicitud."});
            }           

            return Json(new { Success = true});
        }
    }
}