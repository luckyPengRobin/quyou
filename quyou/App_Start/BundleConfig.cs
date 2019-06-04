using System.Web;
using System.Web.Optimization;

namespace quyou
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                   "~/vendor/jquery/jquery.js"));


            bundles.Add(new ScriptBundle("~/Content/Scripts/toojs").Include(
                 "~/vendor/bootstrap/js/bootstrap.js",
                 "~/vendor/metisMenu/metisMenu.js",
                 "~/vendor/raphael/raphael.js",
                 "~/Scripts/plugins/select2/select2.js",
                "~/Scripts/plugins/select2/select2.full.js",
                 "~/Scripts/cust/sb-admin-2.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/plugins/bootstrap-table/bootstrap-table.js",
                "~/Scripts/plugins/bootstrap-table/extensions/export/bootstrap-table-export.js",
                "~/Scripts/plugins/bootstrap-table/extensions/export/tableExport.js",
                "~/Scripts/plugins/datetimepicker/bootstrap-datetimepicker.js",
                "~/Scripts/plugins/validate/bootstrapValidator.js",
                "~/Scripts/layer/layer.js",
                "~/Scripts/fileinput/fileinput.js"
             ));






            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
               "~/vendor/bootstrap/css/bootstrap.css",
               "~/vendor/metisMenu/metisMenu.css",
               "~/Content/cust/sb-admin-2.css",
               "~/vendor/morrisjs/morris.css",
               "~/Scripts/plugins/bootstrap-table/bootstrap-table.css",
               "~/Scripts/plugins/datetimepicker/bootstrap-datetimepicker.css",
               "~/Scripts/plugins/validate/bootstrapValidator.css",
               "~/vendor/font-awesome/css/font-awesome.css",
               "~/Scripts/fileinput/fileinput.css",
               "~/Scripts/plugins/select2/select2.css"
            ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}