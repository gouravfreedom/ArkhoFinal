using System;
using System.Collections.Generic;
using System.Linq;
//using CloudRailSI;
using FFImageLoading.Transformations;
using Foundation;
using UIKit;

namespace DFS.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            //global::Xamarin.FormsMaps.Init ();
            //Xamarin.FormsGoogleMaps.Init("AIzaSyDoXdTaPhf4w3EQpjciwMVsQJ4TPcMGXjY");
            //Xamarin.Forms.DependencyService.Register<Platform_Implemetation_IOS>();

            XamForms.Controls.iOS.Calendar.Init();

            //CRCloudRail.AppKey = "5c27501221b62e522887898e";
            //FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            //var ignore = new CircleTransformation();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
