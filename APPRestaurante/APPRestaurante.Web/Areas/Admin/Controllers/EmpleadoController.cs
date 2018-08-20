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
    public class EmpleadoController : BaseController
    {
        public EmpleadoController(IUnitOfWork unit) : base(unit)
        {

        }

        // GET: Admin/Empleado
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registro(int id = 0)
        {
            var model = new Empleado();
            if(id > 0)
            {
                model = _unit.Empleado.GetEntitybyId(id);
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult ListaEmpleado()
        {
            return Json(_unit.Empleado.GetAll());
        }

        [HttpPost]
        public JsonResult InsertarEmpleado()
        {
            var empleado = new Empleado();
            var insert = false;
            string archivo = "";
            var ruta = Server.MapPath("~/Uploads/Empleado/");

            try
            {
                empleado.id = Convert.ToInt32(Request.Form["Id"]);
                empleado.nombres = Request.Form["Nombre"];
                empleado.apellidos = Request.Form["Apellido"];
                empleado.direccion = Request.Form["Direccion"];
                empleado.celular = Request.Form["Celular"];
                empleado.tipoDocumento = Request.Form["TipoDocumento"];
                empleado.documento = Request.Form["Documento"];
                HttpPostedFileBase foto = Request.Files["Foto"];
               
                if (string.IsNullOrWhiteSpace(empleado.nombres)) return Json(new { Success = false, Message = "Falta completar el nombre." });
                if (string.IsNullOrWhiteSpace(empleado.apellidos)) return Json(new { Success = false, Message = "Falta completar los apellidos." });
                if(string.IsNullOrWhiteSpace(empleado.celular)) return Json(new { Success = false, Message = "Falta completar el numero de celular." });

                if (empleado.id > 0)
                {
                    var obtenerEmpleado = _unit.Empleado.GetEntitybyId(empleado.id);
                    empleado.foto = obtenerEmpleado.foto;

                    if (string.IsNullOrEmpty(obtenerEmpleado.foto))
                    {
                        if(foto == null) return Json(new { Success = false, Message = "Falta completar la foto." });
                        archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + foto.FileName).ToLower();
                        empleado.foto = archivo;
                        foto.SaveAs(ruta + archivo);
                    }
                    else
                    {
                        if(foto != null)
                        {
                            archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + foto.FileName).ToLower();
                            empleado.foto = archivo;
                            foto.SaveAs(ruta + archivo);
                            if (System.IO.File.Exists(ruta + obtenerEmpleado.foto)) System.IO.File.Delete(ruta + obtenerEmpleado.foto);
                        }
                    }

                    var result = _unit.Empleado.Update(empleado);
                }
                else
                {
                    archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + foto.FileName).ToLower();
                    empleado.foto = archivo;
                    insert = Convert.ToBoolean(_unit.Empleado.Insert(empleado));
                    foto.SaveAs(ruta + archivo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return Json(new { Success = true, Message = "Registro Exitoso" });
        }

        [HttpPost]
        public JsonResult EliminarEmpleado(int id)
        {
            var empleado = new Empleado();
            empleado.id = id;

            return Json(_unit.Empleado.Delete(empleado));
        }
    }
}