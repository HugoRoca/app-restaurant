using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPRestaurante.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Fecha = String.Format("Hoy es {0:D}", DateTime.Now);
            return View();
        }
    }
}