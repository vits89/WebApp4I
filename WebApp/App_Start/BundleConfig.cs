using System.Web.Optimization;

namespace WebApp4I.WebApp
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include("~/Content/bootstrap.css", "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/Scripts/jquery-{version}.slim.js",
                "~/Scripts/bootstrap.bundle.js", "~/Scripts/vue.js", "~/Scripts/vee-validate.full.js"));
            bundles.Add(new ScriptBundle("~/bundles/vm").Include("~/Scripts/vueModel.js"));
        }
    }
}
