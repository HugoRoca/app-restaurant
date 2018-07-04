using System.Web.Optimization;

namespace APPRestaurante.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new StyleBundle("~/bundles/cssLogin")
                .Include(""));

            bundles.Add(new DynamicFolderBundle("js", "*.js", false, new JsMinify()));
            bundles.Add(new DynamicFolderBundle("css","*.css", false, new CssMinify()));

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}