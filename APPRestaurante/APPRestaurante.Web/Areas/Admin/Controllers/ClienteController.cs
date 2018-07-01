using APPRestaurante.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPRestaurante.Web.Areas.Admin.Controllers
{
    public class ClienteController : BaseController
    {
        public ClienteController(IUnitOfWork unit) : base(unit)
        {

        }

        // GET: Admin/Cliente
        public ActionResult Index()
        {
            return View();
        }
    }
}