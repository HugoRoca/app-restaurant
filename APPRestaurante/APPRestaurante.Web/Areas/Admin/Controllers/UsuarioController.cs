using APPRestaurante.Models;
using APPRestaurante.UnitOfWork;
using APPRestaurante.Web.Areas.Admin.Filters;
using APPRestaurante.Web.Areas.Admin.Models;
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

        public ActionResult Registro(int id = 0)
        {
            var empleado = _unit.Empleado.GetAll();
            var model = new UsuarioModel();

            var list = new List<SelectListItem>();

            foreach (var item in empleado)
            {
                list.Add(new SelectListItem {
                    Text = item.nombres + " " + item.apellidos,
                    Value = item.id.ToString()
                });
            }

            model.empleados = list;

            if (id > 0)
            {

            }

            return View(model);
        }

        [HttpPost]
        public JsonResult Lista()
        {
            return Json(_unit.Usuario.ListaUsuario());
        }

        [HttpPost]
        public JsonResult Eliminar(int id)
        {
            return Json(new { });
        }
    }
}