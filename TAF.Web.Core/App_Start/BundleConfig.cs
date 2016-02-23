namespace TAF.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new StyleBundle("~/assets/basiccss").Include(
                    "~/assets/css/bootstrap.min.css",
                    "~/assets/css/font-awesome.min.css"));

            bundles.Add(
                new StyleBundle("~/assets/beyondcss").Include(
                    "~/assets/css/beyond.min.css",
                    "~/assets/css/demo.min.css",
                    "~/assets/css/typicons.min.css",
                    "~/assets/css/animate.min.css",
                    "~/assets/css/dataTables.bootstrap.css",
                    "~/assets/js/ztree/zTreeStyle.css",
                    "~/assets/js/ztree/metro.css"));

            bundles.Add(
                new ScriptBundle("~/assets/basicjs").Include(
                    "~/assets/js/jquery.min.js",
                    "~/assets/js/bootstrap.min.js",
                    "~/assets/js/validation/bootstrapValidator.js",
                    "~/assets/js/vue.min.js"));
            bundles.Add(
                new ScriptBundle("~/assets/js").Include(
                    "~/assets/js/skins.min.js",
                    "~/assets/js/jquery.min.js",
                    "~/assets/js/bootstrap.min.js",
                    "~/assets/js/modal.js",
                    "~/assets/js/slimscroll/jquery.slimscroll.min.js",
                    "~/assets/js/lodash.min.js",
                    "~/assets/js/underscore.string.min.js",
                    "~/assets/js/simple-inheritance",
                    "~/assets/js/ztree/jquery.ztree.core-3.5.js",
                    "~/assets/js/toastr/toastr.js",
                    "~/assets/js/fuelux/spinbox/fuelux.spinbox.min.js",
                    "~/assets/js/validation/bootstrapValidator.js",
                    "~/assets/js/vue.js",
                    "~/assets/js/select2/select2.js",
                    "~/assets/js/beyond.js"));

            bundles.Add(
                new ScriptBundle("~/assets/app").Include(
                    "~/scripts/index.js",
                    "~/scripts/base.js",
                    "~/scripts/utility.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}