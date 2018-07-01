using APPRestaurante.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPRestaurante.Web.Areas.Admin.Controllers
{
    [HandleError]
    public class InicioController : Controller
    {
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
                return View(_unit.Clientes.GetAll());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}