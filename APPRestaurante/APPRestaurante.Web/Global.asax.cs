using APPRestaurante.Web.App_Start;
using LightInject;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace APPRestaurante.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
            ConfigureInjector();
        }

        private void ConfigureInjector()
        {
            var container = new ServiceContainer();
            container.RegisterAssembly(Assembly.GetExecutingAssembly());
            container.RegisterAssembly("APPRestaurante.Repository*.dll");
            container.RegisterAssembly("APPRestaurante.UnitOfWork*.dll");
            container.RegisterControllers();
            container.EnableMvc();
        }
    }
}
