using System.Web.Optimization;

namespace APPRestaurante.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new StyleBundle("~/bundles/CSS-Usuario")
                .Include("~/Areas/Admin/Assets/css/bootstrap.css")
                .Include("~/Areas/Admin/Assets/css/font-awesome.css")
                .Include("~/Areas/Admin/Assets/css/ace-fonts.css")
                .Include("~/Areas/Admin/Assets/css/ace.css"));

            bundles.Add(
                new ScriptBundle("~/bundles/JS-Usuario")
                .Include("~/Areas/Admin/Assets/js/jquery.js")
                .Include("~/Areas/Admin/Assets/js/jquery.mobile.custom.js"));

            bundles.Add(new DynamicFolderBundle("js", "*.js", false, new JsMinify()));
            //bundles.Add(new DynamicFolderBundle("css","*.css", false, new CssMinify()));

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}