using APPRestaurante.UnitOfWork;
using APPRestaurante.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPRestaurante.Web.Areas.Admin.Controllers
{
    public class InicioController : Controller
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        protected readonly IUnitOfWork _unit;
        public InicioController()
        {
            _unit = new AppRestauranteUnitOfWork();
        }

        // GET: Admin/Inicio
        public ActionResult Index()
        {
            try
            {
                return View(_unit.Clientes.GetEntitybyId(7));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult Error()
        {
            try
            {
                var zero = 0;
                var x = 5 / zero;

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
                return Json(new { succes = true });
            
        }
    }
}