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

        public ActionResult Registro(int id = 0)
        {
            var model = new Menu();

            if(id > 0)
            {
                model = _unit.Menu.ObtenerMenu(id);
            }

            return View(model);
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
        public JsonResult InsertarMenu()
        {
            var menu = new Menu();
            var insert = false;
            string archivo = "";
            var ruta = Server.MapPath("~/Uploads/Menu/");

            try
            {
                menu.idDetalle = Convert.ToInt32(Request.Form["idDetalle"]);
                menu.fecha = Request.Form["Fecha"];
                menu.titulo = Request.Form["Titulo"];
                menu.descripcion = Request.Form["Descripcion"];
                menu.tipo = Request.Form["Tipo"];
                menu.precio = Request.Form["Precio"] == "" ? 0 : Convert.ToDouble(Request.Form["Precio"]);

                HttpPostedFileBase foto = Request.Files["Foto"];

                if (string.IsNullOrWhiteSpace(menu.fecha)) return Json(new { Success = false, Message = "Falta completar la fecha." });
                if (string.IsNullOrWhiteSpace(menu.titulo)) return Json(new { Success = false, Message = "Falta completar el título." });
                if (string.IsNullOrWhiteSpace(menu.descripcion)) return Json(new { Success = false, Message = "Falta completar la descripción." });
                if (string.IsNullOrWhiteSpace(menu.tipo)) return Json(new { Success = false, Message = "Falta completar el tipo." });
                if (menu.precio <= 0) return Json(new { Success = false, Message = "Falta completar el precio." });
                if (menu.idDetalle > 0 && string.IsNullOrWhiteSpace(foto.FileName)) return Json(new { Success = false, Message = "Falta completar la foto." });

                menu.fechaMenu = Convert.ToDateTime(menu.fecha);
                menu.idUsuario = SessionHelper.GetUser();

                if (menu.idDetalle > 0)
                {
                    var obtenerMenu = _unit.Menu.ObtenerMenu(menu.idDetalle);

                    if (string.IsNullOrWhiteSpace(obtenerMenu.foto))
                    {
                        if (foto.ContentLength == 0) return Json(new { Success = false, Message = "Falta completar la foto." });
                        archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + foto.FileName).ToLower();
                        menu.foto = archivo;
                        foto.SaveAs(ruta + archivo);
                    }
                    else
                    {
                        if (foto.ContentLength > 0)
                        {
                            archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + foto.FileName).ToLower();
                            menu.foto = archivo;
                            foto.SaveAs(ruta + archivo);
                            if (System.IO.File.Exists(ruta + obtenerMenu.foto)) System.IO.File.Delete(ruta + obtenerMenu.foto);
                        }
                    }

                    insert = Convert.ToBoolean(_unit.Menu.EditarMenu(menu));
                }
                else
                {
                    insert = Convert.ToBoolean(_unit.Menu.InsertarMenu(menu));

                    foto.SaveAs(Server.MapPath("~/Uploads/Menu/" + archivo));
                }

                return Json(new { Success = true, Message = "Registro Exitoso" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}