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
    public class UsuarioController : BaseController
    {
        public UsuarioController(IUnitOfWork unit) : base(unit)
        {

        }

        // GET: Admin/Usuario
        public ActionResult Index()
        {
            return View();
        }
    }
}