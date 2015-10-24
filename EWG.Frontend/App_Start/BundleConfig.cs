using System;
using System.Web.Optimization;

namespace EWG.Frontend
{
    public class BundleConfig
    {
        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
                throw new ArgumentNullException("ignoreList");
            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }

        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Resources/bower_components/jquery/dist/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/sitecss")
                .Include("~/Content/css/site.css"));

            /*bundles.Add(new StyleBundle("~/bundles/bootstrapcss")
                .Include("~/Scripts/bower_components/bootstrap/dist/css/bootstrap.css"));*/

            bundles.Add(new StyleBundle("~/bundles/bootstrapcss")
                .Include("~/Content/css/bootstrap-theme-darkly.css"));

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

            bundles.Add(new ScriptBundle("~/bundles/angularjs")
                .Include("~/Resources/bower_components/angularjs/angular.js")
                .Include("~/Resources/bower_components/angular-route/angular-route.js")
                .Include("~/Resources/bower_components/ng-file-upload/angular-file-upload.js")
                .Include("~/Resources/bower_components/ng-file-upload/angular-file-upload-shim.js")
                .Include("~/Resources/bower_components/angular-bootstrap/ui-bootstrap-tpls.js")
                .Include("~/Resources/bower_components/angular-dialog-service/dist/dialogs.min.js")
                .Include("~/Resources/bower_components/angular-sanitize/angular-sanitize.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs")
                .Include("~/Resources/bower_components/bootstrap/dist/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/requirejs")
                .Include("~/Resources/bower_components/requirejs/require.js"));
        }
    }
}