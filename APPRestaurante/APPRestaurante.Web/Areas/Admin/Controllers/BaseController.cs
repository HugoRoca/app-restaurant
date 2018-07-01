using APPRestaurante.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APPRestaurante.Web.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _unit;

        public BaseController(IUnitOfWork unit)
        {
            _unit = unit;
        }
    }
}