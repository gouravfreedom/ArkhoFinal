using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Geolocator;
using Plugin.CurrentActivity;
//using Com.Cloudrail.SI;

using Xamarin.Facebook; using Xamarin.Forms; using DFS.Droid.Implementations; 

namespace DFS.Droid
{
    [Activity(Label = "Arkho", Icon = "@drawable/icon", Theme = "@style/MyTheme.Splash", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            FacebookSdk.ApplicationId = "484296309068142";
            FacebookSdk.SdkInitialize(this);
            //Rg.Plugins.Popup.Popup.Init(this, bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            CrossCurrentActivity.Current.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            //FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            Xamarin.Forms.DependencyService.Register<Platform_Implementation_Android>();
            Xamarin.Forms.DependencyService.Register<Camera_Implementation_Android>();


            //CloudRail.AppKey = "5c27501221b62e522887898e";

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)         {             base.OnActivityResult(requestCode, resultCode, data);             var manager = DependencyService.Get<IFacebookManager>();             if (manager != null)             {                 (manager as FacebookManager)._callbackManager.OnActivityResult(requestCode, (int)resultCode, data);             }         }




        // public override void OnRes

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        //{
        //	PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        //{
        //    Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
    }

}
