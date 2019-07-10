using System.Web;
using System.Web.Optimization;

namespace OurRoots.WebUI
{
    public class BundleConfig
    {
      
        public static void RegisterBundles(BundleCollection bundles)
        {
           
            bundles.Add(new StyleBundle("~/Content/styles/login").Include(
                     "~/Content/loginAssets/css/font-awesome.min.css",
                     "~/Content/loginAssets/css/style.css"));


            bundles.Add(new StyleBundle("~/Content/styles/core").Include(
                      "~/Content/assets/css/lazy-load-images.css",
                      "~/Content/assets/css/bootstrap.min.css",
                      "~/Content/assets/css/jquery-ui.min.css",
                      "~/Content/assets/css/animate.css",
                      "~/Content/assets/css/css-plugin-collections.css",
                      "~/Content/assets/css/menuzord-skins/menuzord-rounded-boxed.css",
                      "~/Content/assets/css/style-main.css",
                      "~/Content/assets/css/preloader.css",
                      "~/Content/assets/css/custom-bootstrap-margin-padding.css",
                      "~/Content/assets/css/responsive.css",
                      "~/Content/assets/css/utility-classes.css",
                      "~/Content/assets/css/settings.css",
                      "~/Content/assets/css/layers.css",
                      "~/Content/assets/css/navigation.css",
                      "~/Content/assets/css/colors/theme-skin-blue.css"));
         

            bundles.Add(new ScriptBundle("~/bundles/headscripts").Include(
                       "~/Content/assets/js/jquery-2.2.4.min.js",
                       "~/Content/assets/js/jquery-ui.min.js",
                       "~/Content/assets/js/bootstrap.min.js",
                        "~/Content/assets/js/jquery-plugin-collection.js",                       
                       "~/Content/assets/js/revolution-slider/js/jquery.themepunch.tools.min.js",
                       "~/Content/assets/js/revolution-slider/js/jquery.themepunch.revolution.min.js",
                        "~/Content/assets/js/jquery.imagesGrid.js"));


            bundles.Add(new ScriptBundle("~/bundles/sitescripts").Include(
                        "~/Content/assets/js/custom.js",
                        "~/Content/assets/js/revolution-slider/js/extensions/revolution.extension.actions.min.js",
                        "~/Content/assets/js/revolution-slider/js/extensions/revolution.extension.carousel.min.js",
                        "~/Content/assets/js/revolution-slider/js/extensions/revolution.extension.kenburn.min.js",
                        "~/Content/assets/js/revolution-slider/js/extensions/revolution.extension.layeranimation.min.js",
                        "~/Content/assets/js/revolution-slider/js/extensions/revolution.extension.migration.min.js",
                        "~/Content/assets/js/revolution-slider/js/extensions/revolution.extension.navigation.min.js",
                        "~/Content/assets/js/revolution-slider/js/extensions/revolution.extension.parallax.min.js",
                        "~/Content/assets/js/revolution-slider/js/extensions/revolution.extension.slideanims.min.js",
                        "~/Content/assets/js/revolution-slider/js/extensions/revolution.extension.video.min.js",                     
                        "~/Scripts/TwitterBootstrapMvcJs.js"));

          
        }
    }
}
