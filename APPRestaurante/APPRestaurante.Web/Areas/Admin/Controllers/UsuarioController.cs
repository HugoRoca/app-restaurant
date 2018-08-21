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
            var model = new UsuarioModel();

            try
            {
                var empleado = _unit.Empleado.GetAll();
                var rol = _unit.Rol.GetAll();

                var list_empleado = new List<SelectListItem>();
                var list_rol = new List<SelectListItem>();

                foreach (var item in empleado)
                {
                    list_empleado.Add(new SelectListItem
                    {
                        Text = item.nombres + " " + item.apellidos,
                        Value = item.id.ToString()
                    });
                }

                foreach (var item in rol)
                {
                    list_rol.Add(new SelectListItem
                    {
                        Text = item.descripcion,
                        Value = item.id.ToString()
                    });
                }

                model.empleados = list_empleado;
                model.roles = list_rol;

                if (id > 0)
                {
                    var data = _unit.Usuario.ObtenerUsuario(id);
                    model.id = data.id;
                    model.usuario = data.usuario;
                    model.idEmpleado = data.idEmpleado;
                    model.idRol = data.idRol;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        [HttpPost]
        public JsonResult Insertar(UsuarioModel usuarioModel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuarioModel.usuario)) return Json(new { Success = false, Message = "Falta completar el nombre de usuario" });
                if (string.IsNullOrWhiteSpace(usuarioModel.clave)) return Json(new { Success = false, Message = "Falta completar la clave" });

                var usuario = new Usuario();
                usuario.idEmpleado = usuarioModel.idEmpleado;
                usuario.clave = usuarioModel.clave;
                usuario.idRol = usuarioModel.idRol;
                usuario.usuario = usuarioModel.usuario;

                if (usuarioModel.id == 0)
                {
                    var lista = _unit.Usuario.ListaUsuario();

                    foreach (var item in lista)
                    {
                        if (item.idEmpleado == usuario.idEmpleado) return Json(new { Success = false, Message = "El empleado ya tiene un usuario registrado" });
                        if (item.usuario == usuario.usuario) return Json(new { Success = false, Message = "El nombre de usuario ya se encuentra en uso." });
                    }

                }
                _unit.Usuario.RegistrarUsuario(usuario);

                return Json(new { Success = true, Message = "Registro exitoso" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}