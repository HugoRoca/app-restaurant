using APPRestaurante.Helper;
using APPRestaurante.Models;
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
    public class MenuController : BaseController
    {
        public MenuController(IUnitOfWork unit) : base(unit)
        {

        }

        // GET: Admin/Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ContarLista(int rows)
        {
            var total = _unit.Menu.Count();
            var totalPagina = total % rows != 0 ? (total / rows) + 1 : total / rows;
            var page = new
            {
                TotalPages = totalPagina
            };

            return Json(new { Page = page });
        }

        public JsonResult ListaMenu(DateTime desde, DateTime hasta, int pagina, int fila)
        {
            var start = ((pagina - 1) * fila) + 1;
            var end = pagina * fila;
            var resultado = _unit.Menu.ListaMenuPaginacion(desde, hasta, start, end);
            return Json(resultado);
        }

        [HttpPost]
        public ActionResult InsertarMenu(Menu menu, HttpPostedFileBase foto)
        {
            try
            {
                var insert = false;

                string archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + foto.FileName).ToLower();

                menu.fechaMenu = Convert.ToDateTime(menu.fecha);
                menu.foto = archivo;
                menu.idUsuario = SessionHelper.GetUser();
                insert = Convert.ToBoolean(_unit.Menu.InsertarMenu(menu));

                foto.SaveAs(Server.MapPath("~/Uploads/Menu/" + archivo));

                return RedirectToAction("Index", "Menu");

                //if (string.IsNullOrWhiteSpace(menu.fecha)) return Json(new { Success = false, Message = "Falta completar la fecha." });
                //if (string.IsNullOrWhiteSpace(menu.titulo)) return Json(new { Success = false, Message = "Falta completar el título." });
                //if (string.IsNullOrWhiteSpace(menu.descripcion)) return Json(new { Success = false, Message = "Falta completar la descripción." });
                //if (string.IsNullOrWhiteSpace(menu.tipo)) return Json(new { Success = false, Message = "Falta completar el tipo." });
                //if (menu.precio <= 0) return Json(new { Success = false, Message = "Falta completar el precio." });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}